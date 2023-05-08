using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        public IResult Add(Customer customer);
        public IResult Update(Customer customer);
        public IResult Delete(Customer customer);
        public IDataResult<List<Customer>> GetAll();
        public IDataResult<Customer> GetCustomerById(int id);
    }
}
