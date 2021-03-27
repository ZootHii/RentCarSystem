using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public class CustomerDetailDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string CompanyName { get; set; }
    }
}