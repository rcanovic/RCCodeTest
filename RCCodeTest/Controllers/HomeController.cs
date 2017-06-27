using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RCCodeTest.Dal;
using RCCodeTest.Models;
using RCCodeTest.Models.Plugins;

namespace RCCodeTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
