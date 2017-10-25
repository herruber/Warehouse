using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcWarehouse.DataAccess;
using MvcWarehouse.Models;

namespace MvcWarehouse.Controllers
{
    public class StockItemsAutoController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: StockItemsAuto
        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        // GET: StockItemsAuto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.Items.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // GET: StockItemsAuto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockItemsAuto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleNumber,Name,Price,ShelfPosition,Quantity,Description")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(stockItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockItem);
        }

        // GET: StockItemsAuto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.Items.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: StockItemsAuto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleNumber,Name,Price,ShelfPosition,Quantity,Description")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockItem);
        }

        // GET: StockItemsAuto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.Items.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: StockItemsAuto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockItem stockItem = db.Items.Find(id);
            db.Items.Remove(stockItem);
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
