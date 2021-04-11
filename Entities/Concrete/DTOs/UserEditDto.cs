using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public class UserEditDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string CompanyName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}