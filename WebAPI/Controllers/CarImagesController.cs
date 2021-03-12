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
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carImageService.GetCarImageById(id);

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