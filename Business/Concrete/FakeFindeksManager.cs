using System;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;

namespace Business.Concrete
{
    public class FakeFindeksService : IFindeksService
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;

        public FakeFindeksService(ICarService carService, IUserService userService)
        {
            _carService = carService;
            _userService = userService;
        }

        public IResult CheckUserFindeksScoreEnoughForCar(int carId, int userId)
        {
            /*Console.WriteLine("SCORE: "+CalculateUserFindeksScore(userId));
            Console.WriteLine(CalculateCarMinFindeksScore(carId));*/
            if (CalculateUserFindeksScore(userId) < CalculateCarMinFindeksScore(carId))
            {
                return new ErrorResult(Messages.CustomerFindeksScoreIsNotEnough);
            }

            return new SuccessResult();
        }
        
        public double CalculateCarMinFindeksScore(int carId)
        {
            var result = _carService.GetCarById(carId);
            if (result.Success)
            {
                double carMinFindeksScore = (double) result.Data.DailyPrice * 0.1 * 14;
                if (carMinFindeksScore > 1900)
                {
                    carMinFindeksScore = 1900;
                }

                return carMinFindeksScore;
            }

            return 0;
        }

        public double CalculateUserFindeksScore(int userId)
        {
            var result = _userService.GetUserById(userId);
            if (result.Success)
            {
                double userFindeksScore = (double) (result.Data.FirstName.Length + result.Data.LastName.Length) * 75;
                if (userFindeksScore > 1900)
                    userFindeksScore = 150;
                else
                    userFindeksScore = 1900;
                return userFindeksScore;
            }

            return 0;
        }
    }
}