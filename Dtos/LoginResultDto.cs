namespace CURDUSingAPIEFCore.Dtos
{
    public class LoginResultDto
    {
        public bool IsSuccess { get; set; }
        public string LoggedInName { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
