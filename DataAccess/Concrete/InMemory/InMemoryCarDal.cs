using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars= new List<Car>
            {
                new Car{CarId=1,BrandId=1,ColorId=1,DailyPrice=550,ModelYear=2021,Description="Ekonomik Renault Clio."},
                new Car{CarId=2,BrandId=1,ColorId=1,DailyPrice=799,ModelYear=2020,Description="Konfor Renault Megane."},
                new Car{CarId=3,BrandId=2,ColorId=1,DailyPrice=1017,ModelYear=2022,Description="Prestij Ford Kuga."},
                new Car{CarId=4,BrandId=3,ColorId=1,DailyPrice=1460,ModelYear=2021,Description="Premium Peugeot 5008."},
                new Car{CarId=5,BrandId=4,ColorId=1,DailyPrice=4951,ModelYear=2022,Description="Lüks Volvo XC90."}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            //Referans numarası farklı oldugundan foreach ile bir referans değer oluşturup eşleştiği
            //zaman silmesi lazım bunun için de LINQ kullanmamız gerekiyor.
            var result = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(result);
        }

        public Car Get(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return _cars.ToList();
        }

        public List<Car> GetAllByBrandId(int brandId)
        {
            return _cars.Where(c => c.BrandId == brandId).ToList();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.Description = car.Description;
        }
    }
}
