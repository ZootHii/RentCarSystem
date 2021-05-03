using System.Threading;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("get/by/id")]
        public IActionResult GetCarById(int id)
        {
            var result = _carService.GetCarById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all")]
        public IActionResult GetAllCars()
        {
            //Thread.Sleep(7000);
            var result = _carService.GetAllCars();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all/by/brand/id")]
        public IActionResult GetCarsByBrandId(int brandId)
        {
            var result = _carService.GetCarsByBrandId(brandId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all/by/color/id")]
        public IActionResult GetCarsByColorId(int colorId)
        {
            var result = _carService.GetCarsByColorId(colorId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("get/all/details/by/brand/id")]
        public IActionResult GetCarsDetailsByBrandId(int brandId)
        {
            var result = _carService.GetCarsDetailsByBrandId(brandId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all/details/by/color/id")]
        public IActionResult GetCarsDetailsByColorId(int colorId)
        {
            var result = _carService.GetCarsDetailsByColorId(colorId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/details/by/car/id")]
        public IActionResult GetCarDetailsByCarId(int carId)
        {
            var result = _carService.GetCarDetailsByCarId(carId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("get/all/details")]
        public IActionResult GetCarsDetails()
        {
            var result = _carService.GetCarsDetails();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all/details/with/preview/first/image")]
        public IActionResult GetCarsDetailsWithPreviewFirstImage()
        {
            Thread.Sleep(1000);
            var result = _carService.GetCarsDetailsWithPreviewFirstImage();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}