using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public class OperationClaimDto : IDto
    {
        public int OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}