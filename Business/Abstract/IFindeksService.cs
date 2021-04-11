using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFindeksService
    {
        public double CalculateCarMinFindeksScore(int carId);
        public double CalculateUserFindeksScore(int userId);
        public IResult CheckUserFindeksScoreEnoughForCar(int carId, int userId);
    }
}