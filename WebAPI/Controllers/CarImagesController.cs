using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarImagesController : Controller
    {
        private readonly ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("get/by/id")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carImageService.GetCarImageById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/by/car/id")]
        public IActionResult GetByCarId(int carId)
        {
            var result = _carImageService.GetCarImagesByCarId(carId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /*
         Send nothing for id
         Send empty for others
         */
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CarImage carImage, IFormFile imageFile)
        {
            var result = await _carImageService.Add(carImage, imageFile);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] CarImage carImage, IFormFile imageFile)
        {
            var result = await _carImageService.Update(carImage, imageFile);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}