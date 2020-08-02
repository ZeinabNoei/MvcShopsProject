using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace MyEShop.Controllers
{
    public class ProductController : Controller
    {
        MvcEShop_DBEntities db = new MvcEShop_DBEntities();
  
        // GET: Product
        public ActionResult ShowGroups()
        {
            return PartialView(db.Product_Groups.ToList());
        }
    }
}