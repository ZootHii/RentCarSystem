using System;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        public IActionResult GetById(int id)
        {
            var result = _carImageService.GetCarImageById(id);
            string filePath = $@"{"Images"}\{"Get"}\{result.Data.ImageName}";
            System.IO.File.WriteAllBytes(filePath, result.Data.ImagePath);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest();
        }
        
        [HttpPost("add")]
        public IActionResult Add([FromForm] CarImage carImage, IFormFile imageFile)
        {
            var result = _carImageService.Add(carImage, imageFile);
            
            if (result.Result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}