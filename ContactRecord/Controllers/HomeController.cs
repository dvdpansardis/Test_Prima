using ContactRecord.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactRecord.Controllers
{
    public class HomeController : Controller
    {
        private CRContext db = new CRContext();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Institute");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}