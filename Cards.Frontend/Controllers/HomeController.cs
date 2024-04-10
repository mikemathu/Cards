using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PointOfSaleSystem.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Home")]
    //[Authorize]
    public class HomeController : Controller
    {
        [Route("Dashboard")]
        [Route("/")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Route("CreateCard")]
        public IActionResult CreateCard()
        {
            return View("Dashboard");
        }

        [Route("CardDetails/{cardId}")]
        public IActionResult CardDetails(string cardId)
        {
            return View("Dashboard");
        }

        [Route("CardEdit/{cardId}")]
        public IActionResult EditCard(string cardId)
        {
            return View("Dashboard");
        }


        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}
