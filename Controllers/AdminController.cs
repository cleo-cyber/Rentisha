using Rentisha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Rentisha.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Listings()
        {
            using(KodishaEntities2 dc=new KodishaEntities2())
            {
               List<Property> prop=dc.Properties.ToList();
                if(prop.Count > 0)
                {
                    return View(prop);
                }
                else
                {   
                    return View();
                }
            }
       
        }

        //[Authorize]
        [HttpPost]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Property");
        }
    }
}