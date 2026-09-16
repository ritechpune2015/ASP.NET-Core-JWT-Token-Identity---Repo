using CURDUSingAPIEFCore.Dtos;

namespace CURDUSingAPIEFCore.Repositories
{
    public interface IManageUsers
    {
        Task<RegistrationResultDto> RegisterNewUser(RegisterDto rec);
        Task<LoginResultDto> Login(LoginDto login);
    }
}
