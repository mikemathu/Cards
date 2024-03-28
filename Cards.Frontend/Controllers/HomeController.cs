using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Route("FiscalPeriods")]
        public IActionResult FiscalPeriods()
        {
            return View();
        }

        [Route("ActivateSubAccount")]
        public IActionResult ActivateSubAccount()
        {
            return View();
        }
        [Route("DeactivateSubAccount")]
        public IActionResult DeactivateSubAccount()
        {
            return View();
        }

        [Route("JournalVouchers")]
        public IActionResult JournalVouchers()
        {
            return View();
        }
        [Route("GetAllAccountsAsync")]
        public IActionResult GetAllAccountsAsync()
        {
            return View();
        }
        [Route("Taxes")]
        public IActionResult Taxes()
        {
            return View();
        }
    }
}
