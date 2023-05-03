using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    //Car ile ilgili db de yapacağım operasyonları kodlayacağım class ın interface'i.

    public interface ICarDal:IEntitiyRepository<Car>
    {
        List<CarDetailDto>GetCarDetails();
    }
}
