using meu.lms.dto.Interfaces;

namespace meu.lms.dto.DTOs.AppUserDTOs
{
    public class AppUserLoginDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}