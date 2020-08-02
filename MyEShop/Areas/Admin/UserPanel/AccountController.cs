using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Security;

namespace MyEShop.Areas.Admin.UserPanel
{
    public class AccountController : Controller
    {
        MvcEShop_DBEntities db = new MvcEShop_DBEntities();
        // GET: Admin/Account
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Single(u=>u.UserName== User.Identity.Name);
                string OldPsswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(change.OldPassword, "MD5");

                if (user.UserPassword == OldPsswordHash)
                {
                    string hashNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(change.Password,"MD5");
                    user.UserPassword = hashNewPassword;
                    db.SaveChanges();
                    ViewBag.Success = true;

                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Current password is not correct");
                }
            }

            return View();
        }
    }
}