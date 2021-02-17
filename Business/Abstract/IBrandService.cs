using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBrandService
    {
        public IResult Add(Brand brand);
        public IResult Update(Brand brand);
        public IResult Delete(Brand brand);
        public IDataResult<Brand> GetBrandById(int brandId);
        public IDataResult<List<Brand>> GetAllBrands();
    }
}