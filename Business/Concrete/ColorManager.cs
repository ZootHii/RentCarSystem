using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    // TODO define necessary messages
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            /*if (string.IsNullOrEmpty(color.ColorName) || color.ColorName == " ")
            {
                return new ErrorResult(Messages.InvalidName);
            }*/
            
            //ValidationTool.Validate(new ColorValidator(), color);
            
            _colorDal.Add(color);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            /*if (string.IsNullOrEmpty(color.ColorName) || color.ColorName == " ")
            {
                return new ErrorResult(Messages.InvalidName);
            }*/
            
            //ValidationTool.Validate(new ColorValidator(), color);
            
            _colorDal.Update(color);
            return new SuccessResult();
        }

        public IResult Delete(Color color)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            _colorDal.Delete(color);
            return new SuccessResult();
        }

        public IDataResult<Color> GetColorById(int colorId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<Color>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == colorId));

        }

        public IDataResult<List<Color>> GetAllColors()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Color>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());

        }
    }
}