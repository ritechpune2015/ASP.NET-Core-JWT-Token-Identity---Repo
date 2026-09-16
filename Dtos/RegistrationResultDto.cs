namespace CURDUSingAPIEFCore.Dtos
{
    public class RegistrationResultDto
    {
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsSuccess { get; set; }
    }
}
