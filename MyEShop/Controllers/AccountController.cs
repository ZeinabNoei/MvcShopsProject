using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Security;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace MyEShop.Controllers
{
    public class AccountController : Controller
    {
        MvcEShop_DBEntities db = new MvcEShop_DBEntities();
        // GET: Account

        public ActionResult Recaptcha(FormCollection form)
        {
            string urlToPost = "https://www.google.com/recaptcha/api/siteverify";
            string secretKey = "6LdApqMUAAAAAGMT4EXe3b3xgt6Xjn9tHebIZHax"; // change this
            string gRecaptchaResponse = form["g-recaptcha-response"];

            var postData = "secret=" + secretKey + "&response=" + gRecaptchaResponse;

            // send post data
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPost);
            request.Method = "POST";
            request.ContentLength = postData.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            // receive the response now
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }

            // validate the response from Google reCaptcha
            var captChaesponse = JsonConvert.DeserializeObject<reCaptchaResponse>(result);
            if (!captChaesponse.Success)
            {
                ViewBag.Message = "Sorry, please validate the reCAPTCHA";
                return View("Register");
            }
            return View("Register");
        }

        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost ]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModels register)
        {
           
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u=>u.UserEmail==register.Email.Trim().ToLower()))
                {
                    Users user = new Users()
                    {
                        UserEmail = register.Email,
                        UserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        UserActiveCode = Guid.NewGuid().ToString(),
                        UserIsActive=false,
                        RegisterDate=DateTime.Now,
                        RoleID=1,
                        UserName=register.UserName
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                    //send Active Email 
                    string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActivationEmail", user);
                    SendEmail.Send(user.UserEmail,"Active Email",body);
                    //end Active Email 
                    return View("Succesregister", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "Entered email is duplicate");
                }

            }
       
                return View(register);
            
        }
    
        public ActionResult ActivUser(string id)
        {
            var user = db.Users.SingleOrDefault(u => u.UserActiveCode == id);
            if (user==null)
            {
                return HttpNotFound();
            }
            user.UserIsActive = true;
            user.UserActiveCode = Guid.NewGuid().ToString();
            db.SaveChanges();
            ViewBag.UserName = user.UserName;
            return View();
        }
        [Route("Login")] 
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login, string RedirectUrl="/")
        {
            if (ModelState.IsValid)
            {
                string hashpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = db.Users.SingleOrDefault(u => u.UserEmail == login.Email && u.UserPassword == hashpassword);
                if (user != null)
                {
                    if (user.UserIsActive==true )
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName,login.RememberMe);
                        return Redirect(RedirectUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "your account has not been activated yet");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "User with the entered information was not found");
                }
            }
            
            return View(login);
        }
        [Route("logOff")]
        public ActionResult logOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserEmail == forgot.Email);
                if (user != null)
                {
                    if (user.UserIsActive == true)
                    {
                        string bodyEmail = PartialToStringClass.RenderPartialView("ManageEmails", "RecoveryPassword", user);
                        SendEmail.Send(user.UserEmail, "Recovery Email", bodyEmail);
                        return View("SuccessForgotPassword", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "your account is not activated");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "user not found");
                }
                }
            return View();
        }
        [Route("RecoveryPassword")]
        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }
        [HttpPost]
        [Route("RecoveryPassword")]
        public ActionResult RecoveryPassword(string id,RecoveryPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserActiveCode == id);
                if (user==null)
                {
                    return HttpNotFound();
                }
                user.UserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                user.UserActiveCode = Guid.NewGuid().ToString();
                db.SaveChanges();
                return Redirect("/Login?recovery=true");
            }
            return View();
        }
    }
}