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

        public ActionResult AddListing(Listing listing)
        {
           
                var db = new ListingEntities();
                db.Listings.Add(listing);
                db.SaveChanges();

                Int64 id = listing.Id;
                ViewBag.msg = "Listing successfully inserted with " + id + "";
                ModelState.Clear();
           
            

            return View();
        }
        public ActionResult ListingDetail()
        {
            return View();
        }
    }
 
}