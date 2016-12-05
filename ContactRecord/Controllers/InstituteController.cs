using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactRecord.Models;
using ContactRecord.DAL;

namespace ContactRecord.Controllers
{
    public class InstituteController : Controller
    {
        private CRContext db = new CRContext();

        // GET: /Institute/
        public ActionResult Index()
        {
            return View(db.Institutes.ToList());
        }

        // GET: /Institute/
        public ActionResult Report()
        {
            List<Institute> institutes = db.Institutes.ToList();
            return View(db.Institutes.ToList());
        }

        // GET: /Institute/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institute institute = db.Institutes.Find(id);
            if (institute == null)
            {
                return HttpNotFound();
            }
            return View(institute);
        }

        // GET: /Institute/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Institute/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InstituteName,ContactName,TelephoneNumber")] Institute institute)
        {
            if (ModelState.IsValid)
            {
                db.Institutes.Add(institute);
                db.SaveChanges();
                return RedirectToAction("Create", "Contact", institute);
            }

            return RedirectToAction("Index", "Institute");
        }

        // GET: /Institute/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institute institute = db.Institutes.Find(id);
            institute.Contacts = db.Contacts.Where(c => c.Institute.ID == institute.ID).ToList();
            if (institute == null)
            {
                return HttpNotFound();
            }
            return View(institute);
        }

        // POST: /Institute/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,InstituteName,ContactName,TelephoneNumber")] Institute institute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(institute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create", "Contact", institute);
            }
            return RedirectToAction("Index", "Institute");
        }

        // GET: /Institute/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institute institute = db.Institutes.Find(id);
            if (institute == null)
            {
                return HttpNotFound();
            }
            return View(institute);
        }

        // POST: /Institute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Institute institute = db.Institutes.Find(id);
            List<Contact> contacts = db.Contacts.Where(c => c.Institute.ID == institute.ID).ToList();
            if (contacts != null && contacts.Count > 0)
            {
                db.Contacts.RemoveRange(contacts);
            }
            db.Institutes.Remove(institute);
            db.SaveChanges();
            return RedirectToAction("Index", "Institute");
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
