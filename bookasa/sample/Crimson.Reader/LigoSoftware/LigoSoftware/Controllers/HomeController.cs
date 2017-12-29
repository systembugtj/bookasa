using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LigoSoftware.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to LigoSoftware.com!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
