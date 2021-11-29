using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class PricePersController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: PricePers
        public ActionResult Index()
        {
            return View(db.PricePers.ToList());
        }

        // GET: PricePers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricePer pricePer = db.PricePers.Find(id);
            if (pricePer == null)
            {
                return HttpNotFound();
            }
            return View(pricePer);
        }

        // GET: PricePers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PricePers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriceID,Description")] PricePer pricePer)
        {
            if (ModelState.IsValid)
            {
                db.PricePers.Add(pricePer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pricePer);
        }

        // GET: PricePers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricePer pricePer = db.PricePers.Find(id);
            if (pricePer == null)
            {
                return HttpNotFound();
            }
            return View(pricePer);
        }

        // POST: PricePers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceID,Description")] PricePer pricePer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pricePer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pricePer);
        }

        // GET: PricePers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricePer pricePer = db.PricePers.Find(id);
            if (pricePer == null)
            {
                return HttpNotFound();
            }
            return View(pricePer);
        }

        // POST: PricePers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PricePer pricePer = db.PricePers.Find(id);
            db.PricePers.Remove(pricePer);
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
    }
}
