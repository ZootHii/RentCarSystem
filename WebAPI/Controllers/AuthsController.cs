using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthsController : Controller
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserLoginDto userLoginDto)
        {
            var resultUser = _authService.Login(userLoginDto);
            if (!resultUser.Success)
            {
                return BadRequest(resultUser);
            }

            return Ok(resultUser);
        }
        
        [HttpPost("register")]
        public ActionResult Register(UserRegisterDto userRegisterDto)
        {
            var resultUser = _authService.Register(userRegisterDto);
            if (!resultUser.Success)
            {
                return BadRequest(resultUser);
            }

            return Ok(resultUser);
        }
        
        /*[HttpPost("access")]
        public ActionResult Ac()
        {
            var user = new User
            {
                EMail = "a@.d",
                FirstName = "a",
                LastName = "b",
            };
            var resultToken = _authService.CreateAccessToken(user);
            if (!resultToken.Success)
            {
                return BadRequest(resultToken);
            }
            return Ok(resultToken);
        }*/
        
    }
}