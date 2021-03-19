using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.TokenCreation
{
    public interface ITokenHelper
    {
        public AccessToken CreateAccessToken(User user, List<OperationClaim> operationClaims);
    }
}