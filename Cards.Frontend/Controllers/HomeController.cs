﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PointOfSaleSystem.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Home")]
    //[Authorize]
    public class HomeController : Controller
    {
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
    }
}
