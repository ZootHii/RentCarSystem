using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalsCustomers : Controller
    {
        private IRentalService _rentalService;

        public RentalsCustomers(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }
    }
}