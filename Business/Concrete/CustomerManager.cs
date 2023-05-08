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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager:ICustomerService
    {
        ICustomerDal _customerdal;
        public CustomerManager(ICustomerDal customerDal)
        {
           _customerdal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerdal.Add(customer);
            return new Result(true, Messages.CustomerAdded);
        }

        public IResult Delete(Customer customer)
        {
            _customerdal.Delete(customer);
            return new Result(true, Messages.CustomerDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerdal.GetAll(),Messages.CustomerListed);
        }

        public IDataResult<Customer> GetCustomerById(int id)
        {
            return new SuccessDataResult<Customer>(_customerdal.Get(c => c.UserId == id), Messages.CustomerIdListed);
        }

        public IResult Update(Customer customer)
        {
            return new Result(true, Messages.CustomerUpdated);
        }
    }
}
