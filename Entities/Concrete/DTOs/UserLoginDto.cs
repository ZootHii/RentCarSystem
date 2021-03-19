using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public class UserLoginDto : IDto
    {
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}