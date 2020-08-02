using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyEShop.Controllers
{
    public class SearchController : Controller
    {
        MvcEShop_DBEntities db = new MvcEShop_DBEntities();
        // GET: Search
        public ActionResult Index(string q)
        {
            List<Products> list = new List<Products>();
            list.AddRange(db.ProductTags.Where(t => t.ProductTag == q).Select(t => t.Products).ToList());
            list.AddRange(db.Products.Where(p => p.ProductTitle.Contains(q) || p.ProductShortDesc.Contains(q) || p.ProductText.Contains(q)).ToList());
            ViewBag.search = q;
            return View(list.Distinct());
        }
    }
}