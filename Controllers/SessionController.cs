using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("/session")]
    public class SessionController : Controller
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            string? sessionValue = Request.Cookies["ToDoApp"];
            bool successfull = int.TryParse(sessionValue, out int userId);
            if (sessionValue == null || !successfull) return Unauthorized();
            return Ok(userId);
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(string userId)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(1);
            Response.Cookies.Append("ToDoApp", userId, options);
            return Ok(userId);
        }

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("ToDoApp");
            return Ok();
        }
    }
}
