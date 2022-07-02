using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoRental.Controllers
{
    public class NewRentalController : Controller
    {
        // GET: NewRental
        public ActionResult New()
        {
            return View();
        }
    }
}