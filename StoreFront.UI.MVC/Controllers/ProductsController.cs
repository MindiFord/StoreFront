using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using StoreFront.UI.MVC.Utilities; // Added for Image Utility
using StoreFront.UI.MVC.Models; // Added for Cart

namespace StoreFront.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.PricePer).Include(p => p.Status);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.PriceID = new SelectList(db.PricePers, "PriceID", "Description");
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,CategoryID,Image,ProductDesc,UnitPrice,PriceID,StatusID")] Product product, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {

                #region File Upload

                // Use default image in none provided
                string file = "Unavailable.jpg";

                if (logo != null)
                {
                    // Reassign variable that held default image to name of uploaded file
                    file = logo.FileName;
                    // Get ext part of file name
                    string ext = file.Substring(file.LastIndexOf('.'));
                    // Approved filed extensions
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    // Check uploaded file ext against goodExts & check file size <= 4mb (ASP.NET default max)
                    if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                    {
                        // Create new file name using guid and add file ext
                        file = Guid.NewGuid() + ext;

                        #region Resize Image

                        // Define file path where images will be saved
                        string savePath = Server.MapPath("~/Content/img/product/");
                        // Convert the HttpPostFileBase to an object of type Image so Image Utility can manipulate dimenstions
                        Image convertedImage = Image.FromStream(logo.InputStream);
                        // Define max size for full size & thumbnail pic in px
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        // Pass data to ResizeImage() on the image utility
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                    }

                    // Update name of image file that will be saved in DB
                    product.Image = file;

                }

                #endregion

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.PriceID = new SelectList(db.PricePers, "PriceID", "Description", product.PriceID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", product.StatusID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.PriceID = new SelectList(db.PricePers, "PriceID", "Description", product.PriceID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", product.StatusID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,CategoryID,Image,ProductDesc,UnitPrice,PriceID,StatusID")] Product product, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                string file = product.Image;

                if (logo != null)
                {



                    file = logo.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;

                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/img/product/");
                        Image convertedImage = Image.FromStream(logo.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion

                        if (product.Image != null && product.Image != "Unavailable.png")
                        {
                            string path = Server.MapPath("~/Content/img/product/");
                            ImageUtility.Delete(path, product.Image);
                        }

                        product.Image = file;

                    }

                    #endregion

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.PriceID = new SelectList(db.PricePers, "PriceID", "Description", product.PriceID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", product.StatusID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            // Delete the image file
            string path = Server.MapPath("~/Content/img/product/");
            ImageUtility.Delete(path, product.Image);

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region AddToCart()

        [HttpPost]
        public ActionResult AddToCart(int qty, int productID)
        {
            // Create an empty version of the LOCAL ShoppingCart (dictionary collection)
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            // Check the cart in the session (GLOBAL) - if it has anything in it, assign its value to local dictionary
            if (Session["cart"] != null)
            {
                // Pull global version to our local version
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            }
            else
            {
                // Create empty instance of LOCAL dictionary
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            // Get product object being added - firstOrDefault() allows for null return
            Product product = db.Products.Where(p => p.ProductID == productID).FirstOrDefault();

            // If product is null, redirect to index view
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Create shoppingCartViewModel
                CartItemViewModel item = new CartItemViewModel(qty, product);

                // If productID already exists in shopping cart, increase qty
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                // Update the GLOBAL cart (Session)
                Session["cart"] = shoppingCart;

            }

            // As long as product was added, redirect user to ShoppingCart Index
            return RedirectToAction("Index", "ShoppingCart");

        }

        #endregion

    }
}
