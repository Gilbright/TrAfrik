using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurAfrikb.Models;

namespace TurAfrikb.Controllers
{
    public class TourismController : Controller
    {
       

        public ActionResult TurkishTourism(language lang )
        {
            lang.type = "french";
            return View(lang);
        }

        public ActionResult AfricanTourism()
        {
            return View();
        }
        public ActionResult TouristicShop()/// do you need a view? coz you redirect to another action of another controller
        {
            return View();
            // <!--return RedirectToAction into the shop with id of touristic products -->
        }
    }
}