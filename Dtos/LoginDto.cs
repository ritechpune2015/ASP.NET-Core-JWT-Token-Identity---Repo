using System.ComponentModel.DataAnnotations;

namespace CURDUSingAPIEFCore.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string EmailID { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
