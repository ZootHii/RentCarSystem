using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    // TODO define necessary messages
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            _brandDal.Add(brand);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            _brandDal.Update(brand);
            return new SuccessResult();
        }

        public IResult Delete(Brand brand)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            _brandDal.Delete(brand);
            return new SuccessResult();
        }

        public IDataResult<Brand> GetBrandById(int brandId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<Brand>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId));
        }

        public IDataResult<List<Brand>> GetAllBrands()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Brand>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }
    }
}