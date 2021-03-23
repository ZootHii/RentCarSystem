using Business.Abstract;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("controller")]
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
                return BadRequest(resultUser.Message);
            }

            var resultToken = _authService.CreateAccessToken(resultUser.Data);
            return Ok(resultToken);
        }
        
        [HttpPost("register")]
        public ActionResult Register(UserRegisterDto userRegisterDto)
        {
            var resultUser = _authService.Register(userRegisterDto);
            if (!resultUser.Success)
            {
                return BadRequest(resultUser);
            }

            var resultToken = _authService.CreateAccessToken(resultUser.Data);
            return Ok(resultToken);
        }
    }
}