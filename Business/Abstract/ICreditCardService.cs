using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        public IResult Add(CreditCard creditCard);
    }
}