using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.EfEntityRepositoryBase;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, DbCarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (DbCarContext context = new DbCarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join k in context.Colors
                             on c.ColorId equals k.ColorId
                             select new CarDetailDto

                             {
                                 CarId = c.CarId,
                                 Description = c.Description,
                                 DailyPrice = c.DailyPrice,
                                 BrandName = b.BrandName,
                                 ColorName = k.ColorName
                             };
                return result.ToList();
            }
        }
    }
}
