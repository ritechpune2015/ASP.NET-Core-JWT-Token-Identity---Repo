using CURDUSingAPIEFCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CURDUSingAPIEFCore.services
{
    public class TokenService : ITokenService
    {
        IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            this._config = config;
        }
        public string GetToken(User rec)
        {
            var claims = new List<Claim>(){
            new Claim(JwtRegisteredClaimNames.Name,rec.FirstName),
            new Claim(JwtRegisteredClaimNames.Email,rec.Email) };


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(this._config["JWT:key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: this._config["JWT:issuer"],
                audience: this._config["JWT:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
