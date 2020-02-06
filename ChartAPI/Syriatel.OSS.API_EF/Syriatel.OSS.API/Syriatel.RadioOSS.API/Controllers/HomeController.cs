using Syriatel.RadioOSS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syriatel.RadioOSS.API.Controllers
{
    public class HomeController : Controller
    {
        Entities4 context = new Entities4();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
