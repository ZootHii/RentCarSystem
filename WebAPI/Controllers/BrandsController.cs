using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        
        [HttpGet("get/by/id")]
        public IActionResult GetById(int id)
        {
            var result = _brandService.GetBrandById(id);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("get/all")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAllBrands();
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("add")]
        public IActionResult Add(Brand brand)
        {
            var result = _brandService.Add(brand);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("delete")]
        public IActionResult Delete(Brand brand)
        {
            var result = _brandService.Delete(brand);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Brand brand)
        {
            var result = _brandService.Update(brand);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}