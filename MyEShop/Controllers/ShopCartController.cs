using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;


namespace MyEShop.Controllers
{
    public class ShopCartController : Controller
    {
        MvcEShop_DBEntities db = new MvcEShop_DBEntities();
        // GET: ShopCart
        public ActionResult ShowCart()
        {
            List<ShopCartItemViewModel> list = new List<ShopCartItemViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listshop = (List<ShopCartItem>)Session["ShopCart"];

                foreach (var item in listshop)
                {
                    var product = db.Products.Where(p => p.ProductID == item.ProductID).Select(p => new
                    {
                        p.ProductImageName,
                        p.ProductTitle,

                    }).Single();
                    list.Add(new ShopCartItemViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Title = product.ProductTitle,
                        ImageName = product.ProductImageName
                    });
                }

            }


            return PartialView(list);
        }

        public ActionResult Index()
        {
            return View();
        }
        List<ShowOrderViewModel> getListOrder()
        {
            List<ShowOrderViewModel> list = new List<ShowOrderViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listshop = Session["ShopCart"] as List<ShopCartItem>;
                foreach (var item in listshop)
                {
                    var product = db.Products.Where(p => p.ProductID == item.ProductID).Select(p => new
                    {
                        p.ProductImageName,
                        p.ProductTitle,
                        p.ProductPrice
                    }).Single();
                    list.Add(new ShowOrderViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Price = product.ProductPrice,
                        ImageName = product.ProductImageName,
                        Title = product.ProductTitle,
                        Sum = item.Count * product.ProductPrice
                    });
                }

            }
            return list;
        }
        public ActionResult Order()
        {
          

            return PartialView(getListOrder());
        }

        public ActionResult CommandOrder(int id, int count)
        {
            List<ShopCartItem> listshop = Session["ShopCart"] as List<ShopCartItem>;
            int index = listshop.FindIndex(p => p.ProductID == id);
            if (count == 0)
            {
                listshop.RemoveAt(index);

            }
            else
            {
                listshop[index].Count = count;
            }
            Session["ShopCart"] = listshop;
            //core
            //Session("MyKey", listshop);


            return View("Order", getListOrder());
        }
        [Authorize]
        public ActionResult Payment()
        {
            int userId = db.Users.Single(u => u.UserName == User.Identity.Name).UserID;
            Orders order = new Orders()
            {
                UserID= userId,
                RegisterDate=DateTime.Now,
                OrderIsFinaly=false,
                ModificationDate = DateTime.Now,
            };
            db.Orders.Add(order);

            var listDetails = getListOrder();
            foreach (var item in listDetails)
            {
                db.OrderDetails.Add(new OrderDetails()
                {
                    OrderCount=item.Count,
                    OrderID=order.OrderID,
                    OrderPrice=item.Price,
                    ProductID=item.ProductID,
                    RegisterDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                });
            }
          
            db.SaveChanges();
            //ToDo: online Payment 


            return Redirect("/");
        }
    }
}