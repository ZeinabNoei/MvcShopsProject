using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;
using DataLayer.ViewModels;
using System.Web;



namespace MyEShop.Controllers
{
    public class ShopController : ApiController
    {
        // GET: api/Shop
        public int Get()
        {
            int count = 0;
            List<ShopCartItem> list = new List<ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCart"] != null)
            {
                list = sessions["ShopCart"] as List<ShopCartItem>;
            }
            return list.Sum(l=>l.Count);
        }

        // GET: api/Shop/5
        public int Get(int id)
        {
            List<ShopCartItem> list = new List<ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCart"]!=null)
            {
                list=sessions["ShopCart"] as List<ShopCartItem>;
            }
            //Aya chizi dare
            if (list.Any(p=>p.ProductID==id))
            {
                int index = list.FindIndex(p => p.ProductID == id);
                list[index].Count += 1;
            }
            else
            {
                list.Add(new ShopCartItem()
                {
                    ProductID=id,
                    Count=1
                });

            }
            sessions["ShopCart"] = list;
            return Get();
        }

  
    }
}
