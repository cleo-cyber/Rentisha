using Rentisha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
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

        //Post property
      
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

        //Get listing detail
        [HttpGet]
        
        public ActionResult ListingDetail(int prop_id)
        {
            using (var dc = new KodishaEntities2())
            {
                Property prop=dc.Properties.Find(prop_id);
                if(prop != null)
                {
                    return View(prop);
                }
                else
                {
                    ViewBag.Message = "Invalid not operation";
                }
            }
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
        [Authorize]
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

        [Authorize]
        public ActionResult DeleteListing()
        {

            return View();
        }

        public ActionResult Search(string search_string) {
            using(var dc = new KodishaEntities2())
            {
                var prop = from p in dc.Properties select p;
                if (!String.IsNullOrEmpty(search_string))
                {
                    prop=prop.Where(s=>s.County.Contains(search_string));
                }
                return View(prop.ToList());
            }
        
        }

        
    }

}