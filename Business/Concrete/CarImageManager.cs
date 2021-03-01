using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        
        public async Task<IResult> Add(CarImage carImage, IFormFile imageFile)
        {
            var fileInfo = new FileInfo(imageFile.FileName);
            string fileName = Guid.NewGuid().ToString();
            string fileExtension = fileInfo.Extension;
            string filePath = $@"{"Images"}\{"Post"}\{fileName + fileExtension}";
            if (imageFile.Length > 0) {
                using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
                    await imageFile.CopyToAsync(fileStream);
                }
            }

            byte[] temp;
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                temp = memoryStream.ToArray();
            }

            carImage.ImageName = fileName + fileExtension;
            carImage.UploadDate = DateTime.Now;
            carImage.ImagePath = temp;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Update(CarImage carImage)
        {
            throw new System.NotImplementedException();
        }

        public IResult Delete(CarImage carImage)
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<CarImage> GetCarImageById(int carImageId)
        {
            return new SuccessDataResult<CarImage>("ss", _carImageDal.Get(image => image.Id == carImageId));
        }

        public IDataResult<List<CarImage>> GetAllCarImages()
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            throw new System.NotImplementedException();
        }
    }
}