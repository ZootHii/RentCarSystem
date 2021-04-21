using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Aspects.Autofac.SecuredOperation.Jwt;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        
        //[SecuredOperationAspect("admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("Get")]
        public IResult Add(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName));
            if (result != null)
            {
                return result;
            }
            
            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("Get")]
        public IResult Update(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName));
            if (result != null)
            {
                return result;
            }
            
            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        [CacheRemoveAspect("Get")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        [SecuredOperationAspect("admin")]
        [CacheAspect]
        public IDataResult<Brand> GetBrandById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId));
        }

        //[SecuredOperationAspect("admin")]
        [CacheAspect]
        public IDataResult<List<Brand>> GetAllBrands()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }
        
        private IResult CheckIfBrandNameExists(string brandName)
        {
            bool result = _brandDal.GetAll(brand => brand.BrandName == brandName).Any();

            if (result)
            {
                return new ErrorResult("Brand Name Exists");
            }

            return new SuccessResult();
        }
        
    }
}