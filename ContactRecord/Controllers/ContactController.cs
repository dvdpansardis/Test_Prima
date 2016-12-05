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
using ContactRecord.ViewModels;

namespace ContactRecord.Controllers
{
    public class ContactController : Controller
    {
        private CRContext db = new CRContext();

        // GET: /Contact/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Contact/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: /Contact/Create
        public ActionResult Create(Institute institute)
        {
            var contact = new Contact();
            contact.Institute = institute;
            contact.Products = new List<Product>();
            PopulateAssignedProductData(contact);

            return View(contact);
        }

        // POST: /Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Content")] Contact contact, string[] selectedProducts, long ID)
        {
            if (selectedProducts != null)
            {
                contact.Products = new List<Product>();
                foreach (var product in selectedProducts)
                {
                    var productToAdd = db.Products.Find(long.Parse(product));
                    contact.Products.Add(productToAdd);
                }
            }
            if(ID > 0)
            {
                contact.Institute = db.Institutes.Find(ID);
            }
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index", "Institute");
            }
            PopulateAssignedProductData(contact);
            return View(contact);
        }

        // GET: /Contact/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            PopulateAssignedProductData(contact);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NameInstitute,NameContact,TelephoneNumber,Observation,Products")] Contact contact, string[] selectedProducts)
        {
            if (ModelState.IsValid)
            {
                UpdateContactProducts(selectedProducts, contact);
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: /Contact/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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

        private void PopulateAssignedProductData(Contact contact)
        {
            var allProdcuts = db.Products;
            var contactProducts = new HashSet<long>(contact.Products.Select(p => p.ID));
            var viewModel = new List<AssignedProductData>();

            foreach (var product in allProdcuts)
            {
                viewModel.Add(new AssignedProductData
                {
                    ID = product.ID,
                    Name = product.ProductName,
                    Assigned = contactProducts.Contains(product.ID)
                });
            }
            ViewBag.Products = viewModel;
        }

        private void UpdateContactProducts(string[] selectedProducts, Contact contactToUpdate)
        {
            if (selectedProducts == null)
            {
                contactToUpdate.Products = new List<Product>();
                return;
            }

            var selectedProductsHS = new HashSet<string>(selectedProducts);
            var contactProducts = new HashSet<long>(db.Contacts.Find(contactToUpdate.ID).Products.Select(p => p.ID));

            foreach (var product in db.Products)
            {
                if (selectedProductsHS.Contains(product.ID.ToString()))
                {
                    if (!contactProducts.Contains(product.ID))
                    {
                        contactToUpdate.Products.Add(product);
                    }
                }
                else
                {
                    if (contactProducts.Contains(product.ID))
                    {
                        contactToUpdate.Products.Remove(product);
                    }
                }
            }
        }
    }
}
