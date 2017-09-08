using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TurAfrikb.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult AfricanNews()
        {
            return View();
        }

        public ActionResult TurkishNews()
        {
            return View();
        }

        public ActionResult BothNews()
        {
            return View();
        }
    }
}