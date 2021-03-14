using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
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
            var result = BusinessRules.Run(CheckIfFormFileIsImageOrNull(imageFile),
                CheckIfCarReachedMaxImageCount(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            carImage.ImageName = FileHelper.CreateImageNameWithExtension(imageFile);
            carImage.UploadDate = DateTime.Now;
            carImage.ImagePath = await FileHelper.ConvertFormFileToByteArray(imageFile);
            _carImageDal.Add(carImage);
            await FileHelper.WriteFormFileToImagesPost(imageFile, carImage.ImageName);

            return new SuccessResult();
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public async Task<IResult> Update(CarImage carImage, IFormFile imageFile)
        {
            // TODO update ederken eski fotoğrafı mı kullanacak, yeni fotoğrafı mı, yoksa hiç bir fotoğraf mı.
            // TODO sanırım update te eskisini koymak daha mantıklı logo koymak yerine
            // TODO eski fotoğrafı post ve get kısmından sil
            var toUpdateCarImageResult = _carImageDal.Get(image => image.Id == carImage.Id);
            carImage.UploadDate = toUpdateCarImageResult.UploadDate;
            if (imageFile != null)
            {
                carImage.ImageName =
                    FileHelper.CreateImageNameWithExtension(imageFile); // eski ismiyle kaydetme uzantı uyuşmayabilir
                carImage.ImagePath = await FileHelper.ConvertFormFileToByteArray(imageFile);
            }
            else
            {
                carImage.ImageName = toUpdateCarImageResult.ImageName;
                carImage.ImagePath = toUpdateCarImageResult.ImagePath;
            }

            _carImageDal.Update(carImage);
            if (imageFile != null)
            {
                await FileHelper.WriteFormFileToImagesPost(imageFile, carImage.ImageName);
            }

            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            // TODO
            throw new System.NotImplementedException();
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
            var result = BusinessRules.Run(CheckIfCarHasImages(carId));
            return result;
        }

        #region Rules

        private IResult CheckIfCarReachedMaxImageCount(int carId)
        {
            var result = _carImageDal.GetAll(image => image.CarId == carId).Count;
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

        private IDataResult<List<CarImage>> CheckIfCarHasImages(int carId)
        {
            var result = _carImageDal.GetAll(image => image.CarId == carId);
            int count = result.Count;
            if (count != 0)
            {
                return new SuccessDataResult<List<CarImage>>($"Car has/have {count} image(s)", result);
            }

            var defaultCarImage = new CarImage
            {
                CarId = carId,
                ImageName = FileHelper.defaultImageName,
                ImagePath = FileHelper.GetDefaultImage(),
            };
            var defaultCarImages = new List<CarImage> {defaultCarImage};
            return new SuccessDataResult<List<CarImage>>(Messages.CarHasNoImage, defaultCarImages);
        }

        #endregion
    }
}