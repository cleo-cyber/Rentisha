using Rentisha.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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

            var prop = new Property();
            using(KodishaEntities2 dc=new KodishaEntities2())
            {
                var recent=dc.Properties.OrderByDescending(p=>p.PropertyId).Take(3).ToList();
                return View(recent);
            }
           
        }

        [HttpGet]
        public ActionResult Listings()
        {
            using(KodishaEntities2  dc=new KodishaEntities2())
            {
                List<Property> props = dc.Properties.ToList();
                return View(props);
            }

         
        }


        public ActionResult AddListings()
        {

            return View();
        }

        //Post Method
      
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddListings(Property prop)
        {
            if (prop.ImageFile != null)
            {

                string fileName = Path.GetFileNameWithoutExtension(prop.ImageFile.FileName);
                string extension = Path.GetExtension(prop.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension; //Add date time to avoid duplicate file names
                prop.Image = "~/ListingImage/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/ListingImage/") + fileName);
                prop.ImageFile.SaveAs(fileName);
            }
            

            if(ModelState.IsValid)
            {
                prop.UserId = 1;
                using (KodishaEntities2 dc=new KodishaEntities2())
                {
                    dc.Properties.Add(prop);
                    dc.SaveChanges();
                }
                ModelState.Clear();
            }
            return View();
        }


        public ActionResult ListingDetail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditListing(int property_id) 
        {
            using(var dc=new KodishaEntities2())
            {
                Property prop = dc.Properties.Find(property_id);
                if(prop != null)
                {
                   return View(prop);
                }
                else
                {
                    ViewBag.Message = "Property not found";
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditListing(Property prop)
        {
             if(ModelState.IsValid )
            {
                using(var dc=new KodishaEntities2())
                {
      

                    dc.Properties.Attach(prop);
                    dc.Entry(prop).State=System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                  
                }
            }
            return RedirectToAction("Listings");
        }


        public ActionResult DeleteListing()
        {

            return View();
        }
    }
}