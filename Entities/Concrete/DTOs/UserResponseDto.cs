using Core.Entities;
using Core.Utilities.Security.TokenCreation;

namespace Entities.Concrete.DTOs
{
    public class UserResponseDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}