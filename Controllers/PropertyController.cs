﻿using Rentisha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentisha.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Listings()
        {
            using (KodishaEntities2 dc = new KodishaEntities2())
            {
                List<Property> props = dc.Properties.ToList();
                return View(props);
            }

        }



        public ActionResult AddListings()
        {

            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddListings(Property prop)
        {
            if (ModelState.IsValid)
            {
                using (KodishaEntities2 dc = new KodishaEntities2())
                {
                    dc.Properties.Add(prop);
                    dc.SaveChanges();
                }

            }
            return RedirectToAction("Listings");
        }
        public ActionResult ListingDetail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditListing(int property_id)
        {
            using (var dc = new KodishaEntities2())
            {
                Property prop = dc.Properties.Find(property_id);
                if (prop != null)
                {
                    return View(prop);
                }
                else
                {
                    ViewBag.Message = "Property not found";
                }
            }

            return RedirectToAction("Listings");

        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditListing(Property prop)
        {
            if (ModelState.IsValid)
            {
                using (var dc = new KodishaEntities2())
                {
                    dc.Properties.Attach(prop);
                    dc.Entry(prop).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                    ViewBag.Message = "Property updated";

                }
            }
            return RedirectToAction("Listings");
        }
        [HttpGet]
        public ActionResult DeleteListing([Required] int property_id)
        {
            using (var dc = new KodishaEntities2())
            {
                Property prop = dc.Properties.Find(property_id);
                if (prop != null)
                {
                    dc.Properties.Remove(prop);
                    dc.SaveChanges();
                    ViewBag.Message = "Property deleted";
                }
                else
                {
                    ViewBag.Message = "Property not found";
                }
            }

            return RedirectToAction("Listings");
        }

        public ActionResult EditDetail()
        {
            return View();
        }
    }

}