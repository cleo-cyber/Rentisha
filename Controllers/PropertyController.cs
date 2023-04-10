using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentisha.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listings()
        {
            return View();
        }

        public ActionResult AddListings()
        {

            return View();
        }
        public ActionResult ListingDetails()
        {
            return View();
        }
        public ActionResult EditListing() 
        {
            return View();
        }
        public ActionResult DeleteListing()
        {
            return View();
        }
    }
}