﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syriatel.TranssmissionOSS.API.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
         public ActionResult Index()
        {
            //DataContext db = new DataContext();
            //var t = db.Tests.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
	}
}



