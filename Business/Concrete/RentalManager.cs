using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentDal;
        ICarService _carService;
        public RentalManager(IRentalDal rentdal,ICarService carService)
        {
            _rentDal = rentdal;
            _carService = carService;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
                _rentDal.Add(rental);
                return new Result(true,Messages.CarRented);
        }

        public IResult Delete(Rental rental)
        {
            _rentDal.Delete(rental);
            return new Result(true,Messages.CarRentDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentDal.GetAll(),Messages.CarsRentListed);
        }

        public IDataResult<Rental> GetByRentalId(int id)
        {
            return new SuccessDataResult<Rental>(_rentDal.Get(r => r.RentId == id));
        }

        public IResult Update(Rental rental)
        {
            _rentDal.Update(rental);
            return new Result(true, Messages.CarUpdated);
        }
    }
}
