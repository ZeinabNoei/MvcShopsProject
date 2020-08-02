using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.ViewModels;
using DataLayer; 

namespace MyEShop.Controllers
{
    public class CompareController : Controller
    {
        MvcEShop_DBEntities db = new MvcEShop_DBEntities();
        // GET: Compare
        public ActionResult Index()
        {
            List<CompareItem> list = new List<CompareItem>();
            ///kalaye digeei to list moghayese hast ya na 
            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<CompareItem>;
            }
            List<Features> features = new List<Features>();
            List<Product_Features> ProductFeatures = new List<Product_Features>();
            foreach (var item in list)
            {
                features.AddRange(db.Product_Features.Where(p => p.ProductID == item.ProductID).Select(f => f.Features).ToList());
                ProductFeatures.AddRange(db.Product_Features.Where(p => p.ProductID == item.ProductID).ToList());
            }
            ViewBag.features = features.Distinct().ToList();
            ViewBag.ProductFeatures = ProductFeatures;
            return View(list);
        }

        public ActionResult AddToCompare(int id)
        {
            List<CompareItem> list = new List<CompareItem>();
            ///kalaye digeei to list moghayese hast ya na 
            if (Session["Compare"]!=null)
            {
                list=Session["Compare"] as List<CompareItem>;
            }
            ///in kala az ghabl to in list nabashad
            if (!list.Any(p=>p.ProductID==id))
            {
                var product = db.Products.Where(p=>p.ProductID==id).Select(p=> new { p.ProductTitle, p.ProductImageName}).Single();
                list.Add(new CompareItem()
                {
                    ProductID = id,
                    CompareTitle= product.ProductTitle,
                    CompareImageName = product.ProductImageName

                });
            }
            Session["Compare"] = list;
            return PartialView("ListCompare",list);
        }

        public ActionResult ListCompare()
        {
            List<CompareItem> list = new List<CompareItem>();
            ///kalaye digeei to list moghayese hast ya na 
            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<CompareItem>;
            }
            return PartialView(list);

        }

        public ActionResult DeleteFromCompare(int id)
        {
            List<CompareItem> list = new List<CompareItem>();
            ///kalaye digeei to list moghayese hast ya na 
            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<CompareItem>;
                int index = list.FindIndex(p => p.ProductID == id);
                list.RemoveAt(index);
                Session["Compare"] = list;
            }
            return PartialView("ListCompare", list);

        }

    }
    
}