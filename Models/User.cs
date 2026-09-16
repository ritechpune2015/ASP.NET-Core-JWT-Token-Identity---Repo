using Microsoft.AspNetCore.Identity;

namespace CURDUSingAPIEFCore.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
    }
}
