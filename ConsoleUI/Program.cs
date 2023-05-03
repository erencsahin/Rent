// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;

//GetCars();

//GetColors();

//GetBrands();

//GetCarsByPriceFilter();


static void GetCars()
{
    CarManager carManager = new CarManager(new EfCarDal());
    foreach (var car in carManager.GetCarDetails().Data)
    {
        Console.WriteLine(car.BrandName +"---"+  car.Description + " / "+ car.ColorName+" / " +car.DailyPrice+" TL");
    }
}

static void GetColors()
{
    ColorManager colorManager = new ColorManager(new EfColorDal());
    foreach (var color in colorManager.GetAll().Data)
    {
        Console.WriteLine(color.ColorName + " / " + color.ColorId);
    }
}

static void GetBrands()
{
    BrandManager brandManager = new BrandManager(new EfBrandDal());
    foreach (var brand in brandManager.GetAll().Data)
    {
        Console.WriteLine(brand.BrandName +" / "+ brand.BrandId);
    }
}

static void GetCarsByPriceFilter()
{
    CarManager carManager = new CarManager(new EfCarDal());
    foreach (var car in carManager.GetByDailyPrice(900, 2500).Data)
    {
        Console.WriteLine(car.Description + car.DailyPrice);
    }
}