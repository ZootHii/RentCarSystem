using System;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : Controller // FAKE PAYMENT
    {
        private readonly IFakePaymentService _fakePaymentService;

        public PaymentsController(IFakePaymentService fakePaymentService)
        {
            _fakePaymentService = fakePaymentService;
        }
        
        [HttpPost("add")]
        public IActionResult Add([FromBody]int totalPrice) // Test
        {
            Console.WriteLine("TOTAL PRİCE BU KADAR " + totalPrice);
            var result = _fakePaymentService.Add(totalPrice);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}