using System;
using Business.Abstract;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Business.Concrete
{
    public class FakePaymentManager : IFakePaymentService
    {
        [TransactionScopeAspect]
        public IResult Add(int totalPrice)
        {
            /*var random = new Random();
            int randomNumber = random.Next(0,5);*/
            if (totalPrice < 7000)
            {
                return new SuccessResult("Paid: " + totalPrice);
            }

            return new ErrorResult("Not Paid: " + totalPrice);
        }
    }
}