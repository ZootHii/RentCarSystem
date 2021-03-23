using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        // TODO isimlendirmelerin hepsini bu controllera göre tekrar düzenle
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("user/by/id")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetUserById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("users/all")]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("users/by/email")]
        public IActionResult GetUsersByEMail(string eMail)
        {
            var result = _userService.GetUsersByEMail(eMail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("user/by/email")]
        public IActionResult GetUserByEMail(string eMail)
        {
            var result = _userService.GetUserByEMail(eMail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // todo by name olarak ta yapılabilir ad ve soyad beraber
        [HttpGet("users/by/first/name")]
        public IActionResult GetUsersByFirstName(string firstName)
        {
            var result = _userService.GetUsersByFirstName(firstName);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("users/by/last/name")]
        public IActionResult GetUsersByLastName(string lastName)
        {
            var result = _userService.GetUsersByLastName(lastName);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("user/operation/claims")]
        public IActionResult GetUserOperationClaims(User user)
        {
            var result = _userService.GetUserOperationClaims(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = _userService.Add(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {
            var result = _userService.Delete(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}