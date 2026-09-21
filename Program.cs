using CURDUSingAPIEFCore.Models;
using CURDUSingAPIEFCore.Repositories;
using CURDUSingAPIEFCore.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddIdentity<User, IdentityRole>(opt => { 
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequiredLength = 5;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<CompanyContext>();

builder.Services.AddScoped<IManageUsers, ManageUsersRepo>();
builder.Services.AddScoped<ITokenService, TokenService>();

var issuer = builder.Configuration["JWT:issuer"];
var audience = builder.Configuration["JWT:audience"];
var key = builder.Configuration["JWT:key"];
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme =
    option.DefaultChallengeScheme =
    option.DefaultForbidScheme =
    option.DefaultScheme =
    option.DefaultSignInScheme =
    option.DefaultSignOutScheme =
    JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
options.TokenValidationParameters = new
TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = issuer,
    ValidAudience =audience,
    IssuerSigningKey = new
SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) }; });;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                   Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
            },
        Array.Empty<string>()
    }
});

});
builder.Services.AddDbContextPool<CompanyContext>(
     opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("scon"))
    );
builder.Services.AddScoped<IProduct, ProductRepo>();
builder.Services.AddCors(opt => {
    opt.AddDefaultPolicy(policy => {
        //policy.WithHeaders("Accept", "Content-Type");
        policy.AllowAnyHeader();
        //policy.WithMethods("Get","Post");
        policy.AllowAnyMethod();
        //policy.WithOrigins("https://www.ritechpune.com", "https://www.revolutioninfosystems.com");
        policy.AllowAnyOrigin();
    });
});
var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
