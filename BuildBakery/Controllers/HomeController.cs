using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BuildBakery;
using BuildBakery;

namespace BuildBakery.Controllers
{
    public class HomeController : Controller
    {
        private buggybakerydbEntities db = new buggybakerydbEntities();

        // GET: NewProducts
        public ActionResult Index()
        {
            return View(db.NewProducts.ToList());
        }

        // GET: NewProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProduct newProduct = db.NewProducts.Find(id);
            if (newProduct == null)
            {
                return HttpNotFound();
            }

            //switch(id)
            //{
            //    case 1:
            //        Utility.MakeDatabaseCallAndFetchData();
            //        break;
            //    case 2:
            //        await Utility.CallWebApi();
            //        break;
            //    case 3:
            //        Utility.MakeOutboundConnections();
            //        break;
            //    case 4:
            //        Utility.SlowSqlCall();
            //        break;
            //    case 6:
            //        Utility.ThrowException();
            //        break;


            //}

            if (id == 4)
            {
                CheckUpdateNeeded();
            }
            return View(newProduct);
        }

        private bool CheckUpdateNeeded()
        {
            bool updateInfo = true;
            if (ConfigurationManager.AppSettings["UPDATE_BREAD_INFO_EXTERNALLY"].ToString() != "1")
            {
                updateInfo = false;
            }
            return updateInfo;
        }

        // GET: NewProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,ImageName")] NewProduct newProduct)
        {
            if (ModelState.IsValid)
            {
                db.NewProducts.Add(newProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newProduct);
        }

        // GET: NewProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProduct newProduct = db.NewProducts.Find(id);
            if (newProduct == null)
            {
                return HttpNotFound();
            }
            return View(newProduct);
        }

        // POST: NewProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,ImageName")] NewProduct newProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newProduct);
        }

        // GET: NewProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProduct newProduct = db.NewProducts.Find(id);
            if (newProduct == null)
            {
                return HttpNotFound();
            }
            return View(newProduct);
        }

        // POST: NewProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewProduct newProduct = db.NewProducts.Find(id);
            db.NewProducts.Remove(newProduct);
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
