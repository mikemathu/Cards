using Microsoft.AspNetCore.Mvc;

namespace Cards.Frontend.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Auth")]
    public class AuthenticationController : Controller
    {
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}