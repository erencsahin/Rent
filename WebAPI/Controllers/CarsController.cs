using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll() 
        {
            var result = _carService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbybrand")]
        public IActionResult GetByBrand(int brandId) 
        {
            var result= _carService.GetByBrand(brandId);
            if (result.Success) 
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyprice")]
        public IActionResult GetByDailyPrice(int min,int max)
        {
            var result = _carService.GetByDailyPrice(min,max);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbycolor")]
        public IActionResult GetByColorId(int colorid)
        {
            var result= _carService.GetByColorId(colorid);
            if (result.Success) 
            { 
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbymodel")]
        public IActionResult GetByModelYear(int min,int max)
        {
            var result =_carService.GetByModelYear(min,max);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id) 
        {
            var result = _carService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        //GETS OVER.

        [HttpPost("add")]
        public IActionResult Add(Car car) 
        {
            var result=_carService.Add(car);
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Car car) 
        {
            var result = _carService.Delete(car);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message); //bir kişiye sordum
        }
        [HttpPost("update")]
        public IActionResult Update(Car car) 
        {
            var result=_carService.Update(car);
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
