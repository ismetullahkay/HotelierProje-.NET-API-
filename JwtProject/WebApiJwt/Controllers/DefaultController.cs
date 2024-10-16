using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Models;

namespace WebApiJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult TokenOlustur()
        {
            return Ok(new CreateToken().TokenCreate());
        } 
        [HttpGet("[action]")]
        public IActionResult TokenAdminOlustur()
        {
            return Ok(new CreateToken().AdminTokenCreate());
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Test2()
        {
            return Ok("Hoşgeldiniz");
        }
        [Authorize(Roles ="Admin,Visitor")] //bu rollere sahip değilse giriş yapılmaz
        [HttpGet("[action]")]
        public IActionResult Test3()
        {
            return Ok("Token Başarıyla Giriş Yaptı");
        }
    }
}
