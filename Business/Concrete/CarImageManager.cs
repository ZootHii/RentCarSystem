using System;
using System.Collections.Generic;
using System.IO;
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
            var result = BusinessRules.Run(CheckIfFormFileIsImageOrNull(imageFile));
            if (result != null)
            {
                return result;
            }
            
            if (imageFile != null)
            {
                carImage.ImageName = FileHelper.CreateImageNameWithExtension(imageFile);
                carImage.UploadDate = DateTime.Now;
                carImage.ImagePath = await FileHelper.ConvertFormFileToByteArray(imageFile);
                _carImageDal.Add(carImage);
                await FileHelper.WriteFormFileToImagesPost(imageFile, carImage.ImageName);
            }
            else
            {
                carImage.ImageName = FileHelper.defaultImageName;
                carImage.UploadDate = DateTime.Now;
                carImage.ImagePath = FileHelper.GetDefaultImage();
                _carImageDal.Add(carImage);
            }

            return new SuccessResult();
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public async Task<IResult> Update(CarImage carImage, IFormFile imageFile)
        {
            // TODO update ederken eski fotoğrafı mı kullanacak, yeni fotoğrafı mı, yoksa hiç bir fotoğraf mı.
            // TODO sanırım update te eskisini koymak daha mantıklı logo koymak yerine
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
            throw new System.NotImplementedException();
        }

        public async Task<IDataResult<CarImage>> GetCarImageById(int carImageId)
        {
            var dataResult = new SuccessDataResult<CarImage>(_carImageDal.Get(image => image.Id == carImageId));

            if (dataResult.Data.ImageName != "logo.jpeg")
            {
                await FileHelper.WriteImageBytesToImagesGet(dataResult.Data.ImagePath, dataResult.Data.ImageName);

            }

            return dataResult;
        }

        public IDataResult<List<CarImage>> GetAllCarImages()
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            throw new System.NotImplementedException();
        }

        #region Rules

        private IResult CheckIfFormFileIsImageOrNull(IFormFile formFile)
        {
            if (formFile == null)
            {
                return new SuccessResult();
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