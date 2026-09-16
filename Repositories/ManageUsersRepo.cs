using CURDUSingAPIEFCore.Dtos;
using CURDUSingAPIEFCore.Models;
using CURDUSingAPIEFCore.services;
using Microsoft.AspNetCore.Identity;

namespace CURDUSingAPIEFCore.Repositories
{
    public class ManageUsersRepo : IManageUsers
    {
        UserManager<User> _usermanager;
        SignInManager<User> _signinmanager; 
        ITokenService _tokenService;
        public ManageUsersRepo(UserManager<User> usermanager,SignInManager<User> signinmanager,ITokenService tokensvr)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _tokenService = tokensvr;
        }

        public async Task<LoginResultDto> Login(LoginDto login)
        {
            LoginResultDto res = new LoginResultDto();
            var urec = this._usermanager.Users.FirstOrDefault(p => p.UserName == login.EmailID);
           
            if (urec == null)
            {
                res.IsSuccess = false;
                res.Message = "Email ID is not Registered!";
                return res;
            }

            var sres = await this._signinmanager.CheckPasswordSignInAsync(urec, login.Password,false);

            if (sres.Succeeded)
            {  
                res.Token= this._tokenService.GetToken(urec);
                res.Message = "Logged in SuccessFull!";
                res.IsSuccess = true;
                res.LoggedInName = urec.FirstName;
            }
            else
            {
                res.IsSuccess = false;
                res.Message = "Invalid Email Id or Password!";
            }

            return res;
        }

        public async Task<RegistrationResultDto> RegisterNewUser(RegisterDto rec)
        {
            RegistrationResultDto res = new RegistrationResultDto();
            try
            {
                var urec = new User()
                {
                    Address = rec.Address,
                    FirstName = rec.FirstName,
                    LastName = rec.LastName,
                    Email = rec.EmailID,
                    MobileNo = rec.MobileNo,
                    UserName=rec.EmailID
                };

                var cres=await this._usermanager.CreateAsync(urec, rec.Password);

                if (cres.Succeeded)
                {
                    res.Message = "Registered Success Fully!";
                    res.IsSuccess = true;
                }
                else
                {
                    res.IsSuccess = false;
                    foreach (var temp in cres.Errors)
                    {
                        res.Errors.Add(temp.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Errors.Add(ex.Message);
            }

            return res;
        }
    }
}
