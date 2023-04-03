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
           
            ListingEntities db=new ListingEntities();

               if(ModelState.IsValid)
            {
                listing.Id = db.Listings.Any() ? db.Listings.Max(l => l.Id) + 1 : 1;
                db.Listings.Add(listing);
                try
                {
                   db.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
                db.Listings.Add(listing);
                db.SaveChanges();

                Int64 id = listing.Id;
                ViewBag.msg = "Listing successfully inserted with " + id + "";
                ModelState.Clear();
           
            

            return View(listing);
        }
        public ActionResult ListingDetail()
        {
            return View();
        }
    }
 
}