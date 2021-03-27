using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Files;
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

        [ValidationAspect(typeof(CarImageValidator))]
        public async Task<IResult> Add(CarImage carImage, IFormFile imageFile)
        {
            /*var result = BusinessRules.Run(CheckIfFormFileIsImageOrNull(imageFile),
                CheckIfCarReachedMaxImageCount(carImage.CarId));
            if (result != null)
            {
                return result;
            }*/

            carImage.GUID = Guid.NewGuid();
            carImage.ImageName = FileHelper.CreateImageNameWithExtension(imageFile).imageName;
            carImage.UploadDate = DateTime.Now;
            carImage.ImagePath = await FileHelper.ConvertFormFileToByteArray(imageFile);
            _carImageDal.Add(carImage);
            await FileHelper.WriteFormFileToImagesPost(imageFile, carImage.ImageName);

            return new SuccessResult();
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public async Task<IResult> Update(CarImage carImage, IFormFile imageFile)
        {
            var result = BusinessRules.Run(CheckIfFormFileIsImageOrNull(imageFile));
            if (result != null)
            {
                return result;
            }
            
            var toUpdateCarImageResult = _carImageDal.Get(image => image.Id == carImage.Id);
            carImage.UploadDate = toUpdateCarImageResult.UploadDate;
            
            if (imageFile != null)
            {
                // change old name because extension may differ
                carImage.GUID = toUpdateCarImageResult.GUID;
                carImage.ImageName = FileHelper.CreateImageNameWithExtension(imageFile).imageName;
                carImage.ImagePath = await FileHelper.ConvertFormFileToByteArray(imageFile);
            }
            else
            {
                carImage.GUID = toUpdateCarImageResult.GUID;
                carImage.ImageName = toUpdateCarImageResult.ImageName;
                carImage.ImagePath = toUpdateCarImageResult.ImagePath;
            }

            _carImageDal.Update(carImage);
            if (imageFile != null)
            {
                FileHelper.DeleteImage(toUpdateCarImageResult.ImageName);
                await FileHelper.WriteFormFileToImagesPost(imageFile, carImage.ImageName);
            }

            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            var imageToDelete = _carImageDal.Get(image => image.Id == carImage.Id);
            _carImageDal.Delete(carImage);
            FileHelper.DeleteImage(imageToDelete.ImageName);
            return new SuccessResult();
        }

        public async Task<IDataResult<CarImage>> GetCarImageById(int carImageId)
        {
            //TODO CheckIfCarImageExists
            var dataResult = new SuccessDataResult<CarImage>(_carImageDal.Get(image => image.Id == carImageId));

            
            
            
            await FileHelper.WriteImageBytesToImagesGet(dataResult.Data.ImagePath, dataResult.Data.ImageName);

            return dataResult;
        }

        public IDataResult<List<CarImage>> GetAllCarImages()
        {
            // todo çok kasacak pc yi büyük boyutlu dosyalarda o yüzden ilerde küçültme işlemi yap max boyut belirle
            throw new System.NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            // we must always return Image/Images so always success
            var result = _carImageDal.GetAll(image => image.CarId == carId);
            return result.Count == 0 
                ? new SuccessDataResult<List<CarImage>>(Messages.CarHasNoImage, GetDefaultCarImagesForTheCar(carId)) 
                : new SuccessDataResult<List<CarImage>>(Messages.CarImagesListedByCarId, result);
        }

        private List<CarImage> GetDefaultCarImagesForTheCar(int carId)
        {
            var defaultCarImage = new CarImage
            {
                CarId = carId,
                ImageName = FileHelper.defaultImageName,
                ImagePath = FileHelper.GetDefaultImage(),
            };
            return new List<CarImage> {defaultCarImage};
        }
        
        #region Rules

        private IResult CheckIfCarReachedMaxImageCount(int carId)
        {
            int result = _carImageDal.GetAll(image => image.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarReachedMaxImageCount);
            }

            return new SuccessResult();
        }

        private IResult CheckIfFormFileIsImageOrNull(IFormFile formFile)
        {
            if (formFile == null)
            {
                return new ErrorResult(Messages.NullFileError);
            }

            string extension = FileHelper.GetFormFileExtension(formFile);
            string extensionUpper = extension.ToUpper();
            if (extensionUpper.Equals(".JPEG") || extensionUpper.Equals(".JPG") || extensionUpper.Equals(".PNG"))
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.InvalidFileExtension);
        }
        
        #endregion
    }
}