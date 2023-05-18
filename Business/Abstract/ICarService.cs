using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetByDailyPrice(int min,int max);
        IDataResult<List<Car>> GetByColorId(int colorId);
        IDataResult<List<Car>> GetByModelYear(int min, int max);
        IDataResult<List<Car>> GetByBrand(int brandId);
        IResult Add(Car car);
        IResult Delete(Car car);
        IResult Update(Car car);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<Car> GetById(int id);
        IResult TransactionalOperation(Car car);

    }
}
