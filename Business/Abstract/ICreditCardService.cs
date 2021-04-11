using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    // TODO security operations HASHING will be added
    public interface ICreditCardService
    {
        public IResult Add(CreditCard creditCard);
        public IDataResult<CreditCard> GetCreditCardByCustomerId(int customerId);
    }
}