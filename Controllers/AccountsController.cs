using CURDUSingAPIEFCore.Dtos;
using CURDUSingAPIEFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CURDUSingAPIEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        IManageUsers _users;
        public AccountsController(IManageUsers user)
        {
            this._users = user;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto rec)
        {

            if (ModelState.IsValid)
            {
                var res=await this._users.RegisterNewUser(rec);
                if (res.IsSuccess)
                {
                    return Ok(res.Message);
                }
                else
                {
                    return BadRequest(res.Errors);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto rec)
        {

            if (ModelState.IsValid)
            {
                var res = await this._users.Login(rec);
                if (res.IsSuccess)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
