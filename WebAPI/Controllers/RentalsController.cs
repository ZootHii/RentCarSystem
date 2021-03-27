using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("get/by/id")]
        public IActionResult GetRentalById(int id)
        {
            var result = _rentalService.GetRentalById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all")]
        public IActionResult GetAllRentals()
        {
            var result = _rentalService.GetAllRentals();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all/by/car/id")]
        public IActionResult GetRentalsByCarId(int carId)
        {
            var result = _rentalService.GetRentalsByCarId(carId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all/by/customer/id")]
        public IActionResult GetRentalsByCustomerId(int customerId)
        {
            var result = _rentalService.GetRentalsByCustomerId(customerId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get/all/details")]
        public IActionResult GetRentalsDetails()
        {
            var result = _rentalService.GetRentalsDetails();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.Add(rental);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Rental rental)
        {
            var result = _rentalService.Delete(rental);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Rental rental)
        {
            var result = _rentalService.Update(rental);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}