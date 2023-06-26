namespace WebApiAssignemnt.Dto
{
    public class ResLoginUserDto
    {
        public string token { get; set; } = string.Empty;
        public ResUserRegistrationDto userProfile { get; set; }
    }
}
