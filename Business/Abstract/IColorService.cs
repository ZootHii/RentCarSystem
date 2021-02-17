using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IColorService
    {
        public IResult Add(Color color);
        public IResult Update(Color color);
        public IResult Delete(Color color);
        public IDataResult<Color> GetColorById(int colorId);
        public IDataResult<List<Color>> GetAllColors();
    }
}