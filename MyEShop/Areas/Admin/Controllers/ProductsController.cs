using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using InsertShowImage;
using KooyWebApp_MVC.Classes;


namespace MyEShop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MvcEShop_DBEntities db = new MvcEShop_DBEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.Groups = db.Product_Groups.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductTitle,ProductShortDesc,ProductText,ProductPrice,ProductImageName,ProductRecordDate,ProductModificationDate")] Products products, List<int> selectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (selectedGroups == null)
                {
                    ViewBag.ErrorSelectedGroup = true;
                    ViewBag.Groups = db.Product_Groups.ToList();
                    return View(products);
                }
                products.ProductImageName = "download.png";
                if (imageProduct!=null && imageProduct.IsImage())
                {
                    products.ProductImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ProductImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + products.ProductImageName ),
                        Server.MapPath("/Images/ProductImages/Thumb/" + products.ProductImageName));
                   
                }
                products.RecordDate = DateTime.Now;
                products.ModificationDate = DateTime.Now;
                db.Products.Add(products);
                foreach (int selectedGroup in selectedGroups)
                {
                   db.Product_Selected_Groups.Add(new Product_Selected_Groups()
                    {
                        ProductID = products.ProductID,
                        GroupID = selectedGroup,
                        RecordDate=DateTime.Now,
                        ModificationDate = DateTime.Now
                    });
                }
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (string t in tag)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            ProductTag = t.Trim(),
                            RecordDate = DateTime.Now,
                            ModificationDate = DateTime.Now
                        });
                    }
                }
          
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = db.Product_Groups.ToList();
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.SelectedGroups = products.Product_Selected_Groups.ToList();
            ViewBag.Groups = db.Product_Groups.ToList();
            ViewBag.Tags = string.Join(",", products.Product_Tags.Select(t=>t.ProductTag).ToList());
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductTitle,ProductShortDesc,ProductText,ProductPrice,ProductImageName,ProductRecordDate,ProductModificationDate")] Products products, List<int> selectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (imageProduct != null && imageProduct.IsImage())
                {
                    if (products.ProductImageName != "download.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + products.ProductImageName));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb" + products.ProductImageName));
                    }
                    products.ProductImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ProductImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + products.ProductImageName),
                        Server.MapPath("/Images/ProductImages/Thumb/" + products.ProductImageName));

                }
                db.Entry(products).State = EntityState.Modified;
                products.RecordDate = DateTime.Now;
                products.ModificationDate = DateTime.Now;

                db.Product_Tags.Where(t => t.ProductID == products.ProductID).ToList().ForEach(t=>db.Product_Tags.Remove(t));
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (string t in tag)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            ProductTag = t.Trim(),
                            RecordDate = DateTime.Now,
                            ModificationDate = DateTime.Now
                        });
                    }
                }

                db.Product_Selected_Groups.Where(g => g.ProductID == products.ProductID).ToList().ForEach(g => db.Product_Selected_Groups.Remove(g));
                foreach (int selectedGroup in selectedGroups)
                {
                    db.Product_Selected_Groups.Add(new Product_Selected_Groups()
                    {
                        ProductID = products.ProductID,
                        GroupID = selectedGroup,
                        RecordDate = DateTime.Now,
                        ModificationDate = DateTime.Now
                    });
                }

                ViewBag.SelectedGroups = products.Product_Selected_Groups.ToList();
                ViewBag.Groups = db.Product_Groups.ToList();
                ViewBag.Tags = tags;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Gallery(int id)
        {
            ViewBag.Galleries = db.Product_Galleries.Where(p => p.ProductID == id).ToList();
            return View(new Product_Galleries()
            {
                ProductID=id

            }
                
                );
        }
        [HttpPost]
        public ActionResult Gallery(Product_Galleries galleries,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp !=null && imgUp.IsImage())
                {
                    galleries.GalleryImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Images/ProductImages/" + galleries.GalleryImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + galleries.GalleryImageName),
                    Server.MapPath("/Images/ProductImages/Thumb" + galleries.GalleryImageName));
                    galleries.RecordDate = DateTime.Now;
                    galleries.ModificationDate = DateTime.Now;

                    db.Product_Galleries.Add(galleries);
                    db.SaveChanges();
                }

            }


            return RedirectToAction("Gallery", new {id=galleries.ProductID });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
