using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using cjcsessionapp.Infrastructure;
using cjcsessionapp.Models;

namespace cjcsessionapp.Controllers
{
    [CustAuthFilter(Roles = "Admin, Registrar")]
    public class InstitutionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Institution> list = db.Institutions.OrderBy(v => v.Name).ToList();
            return View(list);
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // GET: Institutions/Create
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name, NumberOfDelegatesAssigned")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                db.Institutions.Add(institution);
                db.SaveChanges();
                TempData["Success"] = "1 Institution Successfully added to list.";
                return RedirectToAction("Index");
            }

            return View(institution);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name, NumberOfDelegatesAssigned")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(institution).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = "1 Institution Successfully updated.";
                return RedirectToAction("Index");
            }
            return View(institution);
        }

        [CustAuthFilter(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        [CustAuthFilter(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Institution institution = db.Institutions.Find(id);
            db.Institutions.Remove(institution);
            db.SaveChanges();
            TempData["Success"] = "1 Institution Successfully deleted from list.";
            return RedirectToAction("Index");
        }

        [CustAuthFilter(Roles = "Admin")]
        public ActionResult LoadInstitutions()
        {

            string fileName = "institutionlist.csv";
            string directoryPath = Server.MapPath("~");

            StreamReader stream = new StreamReader(directoryPath + fileName);
            int counter = 0;

            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine();
                var value = line.Split(',');                

                Institution newInstitution = new Institution()
                {
                    Name = value[0],
                    NumberOfDelegatesAssigned =Convert.ToInt32(value[1]) // needs to be changed to Convert.ToInt32(value[1]) 
                };

                db.Institutions.Add(newInstitution);
                db.SaveChanges();
                counter += 1;
            }

            TempData["Success"] = counter + " Institutions were successfully added.";
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
