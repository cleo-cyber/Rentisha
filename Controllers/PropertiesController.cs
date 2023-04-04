using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentisha.DAL;
using Rentisha.Models;

namespace Rentisha.Controllers
{
    public class PropertiesController : Controller
    {

        Property_DAL _propertiesDal=new Property_DAL();
        // GET: Properties
        public ActionResult DisplayListing()
        {

            var properttyList=_propertiesDal.GetAllProperties();

            if (properttyList.Count == 0)
            {
                TempData["InfoMessage"] = "No property list currently";
            }
            return View(properttyList);
        }

        // GET: Properties/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Properties/Create
        public ActionResult AddProperty()
        {
            return View();
        }

        // POST: Properties/Create
        [HttpPost]
        public ActionResult AddProperty(Properties property)
        {
            try
            {
                bool isInserted = false;
                if (ModelState.IsValid)
                {
                    isInserted = _propertiesDal.InsertProperty(property);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Listing Inserted successfully";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Unable to insert listing";
                    }
                  

                }
                return RedirectToAction("DisplayListing");
            }
            catch (Exception ex)
            {

                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Properties/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Properties/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
