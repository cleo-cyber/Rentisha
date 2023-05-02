using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Rentisha.Models;
namespace Rentisha.Controllers
{
    public class UserController : Controller
    {
        //Registration

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        //Registration POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "isEmailVerified,ActivationCode")] User user)
        {

            bool Status = false;
            string Message = "";

            //Model Validity
            if(ModelState.IsValid)
            {
                //Email exists or not
                var isExist = isEmailExist(user.EmailId);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExists", "Email Already Exist");
                    return View(user);
                }
                //End Email Validation

                //Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                //End Generation

                //Password Hashing

                user.Password=Crypto.Hash(user.Password);
                user.ConfirmPassword=Crypto.Hash(user.ConfirmPassword);

                //End Hashing

                //Verify Email

                user.isEmailVerified = false;


                //Save to db
                using (KodishaEntities1 dc = new KodishaEntities1())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();


                    //Send Email to user
                    sendVerification(user.EmailId, user.ActivationCode.ToString());
                    Message = "Registration successfull Account activation link has been send to you email"+user.EmailId;
                    Status=true;
                }

            }
            else
            {
                Message = "Invalid Request";
            }
            ViewBag.Message=Message;
            ViewBag.Status=Status;
 


            






            return View(user);
        }



        //verify account

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using(KodishaEntities1 dc=new KodishaEntities1())
            {
                
                dc.Configuration.ValidateOnSaveEnabled = false;//To avoid password do not match error
                //Get activationcode
                var v=dc.Users.Where(a=>a.ActivationCode==new Guid(id)).FirstOrDefault();
                if (v!=null)
                {
                    v.isEmailVerified=true;
                    dc.SaveChanges();
                    Status=true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }

            }
            ViewBag.Status = true;
            return View(); 
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login,string ReturnUrl)
        {
        
                string Message = "";
                ViewBag.Message= Message;
            
            using(KodishaEntities1 dc=new KodishaEntities1())
            {
                var v=dc.Users.Where(a=>a.EmailId==login.EmailId).FirstOrDefault();
                if(v != null)
                {
                    //Compare passwords
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        //Cookie creation
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.EmailId,login.RememberMe,timeout);
                        string Encrypted=FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly= true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Property");
                        }
                    }
                    else
                    {
                        Message = "Invalid Credentials";
                    }
                }
                else
                {
                    Message = "Invalid Credentials";
                }
            }

    
            return View();
        }



        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]

        public bool isEmailExist (string EmailID)
        {
            
            using(KodishaEntities1 dc=new KodishaEntities1())
            {
                var v=dc.Users.Where(a=>a.EmailId== EmailID).FirstOrDefault();
                return v !=null;
            }
        }

        [NonAction]

        public void sendVerification(string EmailId,string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail=new MailAddress("classde979@gmail.com","Kodisha");
            var toEmail = new MailAddress(EmailId);
            //var fromEmailPassword = "GodIsGood";
            string subject = "Your account is Succesfully created";
            var body = "<br/><br/> Kodisha account created succesfully " + "Click on the link below to verify your account" + "<a href='" + link + "'>" + link +link+"</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryFormat = (SmtpDeliveryFormat)SmtpDeliveryMethod.Network,
                UseDefaultCredentials= false,
                Credentials=new NetworkCredential(fromEmail.Address, "romhckyswlsmfkwz")
            };
            using (var Message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml= true
            }) 
                smtp.Send(Message);
        }
    }


}