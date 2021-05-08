using Core.Entities;
using Core.Utilities.Security.TokenCreation;

namespace Entities.Concrete.DTOs
{
    public class CustomerResponseDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string CompanyName { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}