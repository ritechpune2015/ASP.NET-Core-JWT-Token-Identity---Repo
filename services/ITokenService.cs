using CURDUSingAPIEFCore.Models;

namespace CURDUSingAPIEFCore.services
{
    public interface ITokenService
    {
        string GetToken(User rec);
    }
}
