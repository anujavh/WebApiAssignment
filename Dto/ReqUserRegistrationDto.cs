using System.ComponentModel.DataAnnotations;

namespace WebApiAssignemnt.Dto
{
    public class ReqUserRegistrationDto
    {
        [Required] public string UserName { get; set; } = string.Empty;
        [Required] public string PasswordHash { get; set; } = string.Empty;
        [Required] public string Email { get; set; } = string.Empty;
    }
}
