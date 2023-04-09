using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentisha.Models;
using System.Security.Cryptography;
using System.Text;

namespace Rentisha.Controllers
{
    public class UserController : Controller
    {

        KodishaEntities db= new KodishaEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user) 
        {


            db.Users.Add(user);
            db.SaveChanges();
        return View();
        }


        public void GenerateHash(User user)
        {
            var userPass = user.Password1;
            using(var hash=new System.Security.Cryptography.HMACSHA512())
            {
                byte[] hashPass = hash.ComputeHash(Encoding.UTF8.GetBytes(userPass));
                byte[] salt = hash.Key;
            }

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(User user)
        {
            var log = db.Users.Where(a => a.Username.Equals(user.Username) && a.Password1.Equals(user.Password1)).FirstOrDefault();
            if (log != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
