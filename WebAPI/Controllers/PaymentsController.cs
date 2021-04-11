using System;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : Controller // FAKE PAYMENT
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        
        [HttpPost("add")]
        public IActionResult Add([FromBody]int totalPrice) // Test
        {
            Console.WriteLine("TOTAL PRİCE BU KADAR " + totalPrice);
            var result = paymentService.Add(totalPrice);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}