using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("admin,moderator")]
        [ValidationAspect(typeof(CarValidator))]
        //33.satırın anlamı şu; Add methodu CarValidator kullanarak doğrula.
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckCarDescription(car.Description));
            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);
            return new Result(true, Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new Result(true, Messages.CarDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            //iş kodları.
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);

        }

        public IDataResult<List<Car>> GetByDailyPrice(int min, int max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.DailyPrice >= min && p.DailyPrice <= max));
        }
        public IDataResult<List<Car>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new Result(true, Messages.CarUpdated);
        }
        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            var car = _carDal.Get(i => i.ID == id);
            return new SuccessDataResult<Car>(car);
        }

        public IDataResult<List<Car>> GetByModelYear(int min, int max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(y => y.ModelYear >= min && y.ModelYear <= max));

        }

        public IDataResult<List<Car>> GetByBrand(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(b => b.BrandId == brandId));
        }

        private IResult CheckCarDescription(string description)
        {
            var result = _carDal.GetAll(d=>d.Description == description).Any();
            if (result)
            {
                return new ErrorResult(Messages.CarAlreadyExists);
            }

            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(Messages.CarUpdated);

        }
    }
}
