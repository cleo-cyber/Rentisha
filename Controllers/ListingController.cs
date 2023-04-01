using Rentisha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentisha.Controllers
{
    public class ListingController : Controller
    {
        // GET: Listing
        public ActionResult RentListings()
        {
            return View();
        }
        public ActionResult AddListing()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddListing(Listing listing)
        {
            if(ModelState.IsValid)
            {
               
            }
            return View();
        }
    }
}