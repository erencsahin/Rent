using Business.Abstract;
using Business.Constants;
using Core.Utilities.BusinessRules;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carimageDal;
        IFileHelper _fileHelper;
        public CarImageManager(ICarImageDal carImageDal,IFileHelper fileHelper)
        {
            _carimageDal = carImageDal;
            _fileHelper = fileHelper;
        }

        public IResult Add(IFormFile file,CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckForImageLimit(carImage.CarId));
            if (result != null) 
            {
                return result;
            }
            carImage.ImagePath = _fileHelper.Upload(file,PathConstants.ImagesPath);
            carImage.Date = DateTime.Now;
            _carimageDal.Add(carImage);
            return new SuccessResult("Picture has been added.");

        }

        public IResult Delete(CarImage carImage)
        {
            _fileHelper.Delete(PathConstants.ImagesPath + carImage.ImagePath);
            _carimageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carimageDal.GetAll());
        }

        public IDataResult<CarImage> GetByCarId(int carId)
        {
            return new SuccessDataResult<CarImage>(_carimageDal.Get(c=>c.CarId==carId));
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = _fileHelper.Update(file,PathConstants.ImagesPath+carImage.ImagePath,PathConstants.ImagesPath);
            _carimageDal.Update(carImage);
            return new SuccessResult();

        }

        public IResult CheckForImageLimit(int carId)
        {
            var result = _carimageDal.GetAll(c=>c.CarId==carId).Count;
            if (result>=5)
            {
                return new ErrorResult(Messages.CarImageLimit);
            }
            return new SuccessResult();
        }

        public IResult CheckImageExist(int carId)
        {
            var result = _carimageDal.GetAll(c=>c.CarId == carId).Count;
            if (result>0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        public IDataResult<List<CarImage>> GetDefaultImage(int carId) 
        {
            List<CarImage> carImage = new List<CarImage>();
            carImage.Add(new CarImage() {  CarId = carId,Date=DateTime.Now,ImagePath = "Defaultimage.jpg" });
            return new SuccessDataResult<List<CarImage>>(carImage);
        }

        public IDataResult<CarImage> GetByImageId(int imageId)
        {
            return new SuccessDataResult<CarImage>(_carimageDal.Get(c => c.Id == imageId));
        }
    }
}
