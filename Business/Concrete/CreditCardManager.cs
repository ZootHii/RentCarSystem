using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    // TODO security operations HASHING will be added
    public class CreditCardManager : ICreditCardService
    {
        private readonly ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult Add(CreditCard creditCard)
        {
            _creditCardDal.Add(creditCard);
            return new SuccessResult("credit card added");
        }

        public IDataResult<CreditCard> GetCreditCardByCustomerId(int customerId)
        {
            var result = _creditCardDal.Get(card => card.CustomerId == customerId);
            if (result == null)
            {
                return new ErrorDataResult<CreditCard>("no saved card found");
            }
            return new SuccessDataResult<CreditCard>("get credit card", _creditCardDal.Get(card => card.CustomerId == customerId));
        }
    }
}