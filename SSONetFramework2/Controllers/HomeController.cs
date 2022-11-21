using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSONetFramework2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page. SSO Web 2";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page. SSO Web 2";

            return View();
        }
    }
}