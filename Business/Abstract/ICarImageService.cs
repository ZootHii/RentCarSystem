using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        public Task<IResult> Add(CarImage carImage, IFormFile imageFile);
        public Task<IResult> Update(CarImage carImage, IFormFile imageFile);
        public IResult Delete(CarImage carImage);
        public Task<IDataResult<CarImage>> GetCarImageById(int carImageId);
        public IDataResult<List<CarImage>> GetAllCarImages();
        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId);
        public IDataResult<CarImage> GetCarPreviewFirstImageByCarId(int carId);
    }
}