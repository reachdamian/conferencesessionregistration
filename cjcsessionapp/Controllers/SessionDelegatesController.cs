using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using cjcsessionapp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace cjcsessionapp.Controllers
{
    public class SessionDelegatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public SessionDelegatesController()
        {

        }

        public SessionDelegatesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: SessionDelegates
        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem { Text = "Male", Value = "1", Selected = true };
            SelectListItem item2 = new SelectListItem { Text = "Female", Value = "2", Selected = false };

            items.Add(item1);
            items.Add(item2);

            ViewBag.Gender = items;

            var sessionDelegates = db.SessionDelegates.Include(a => a.Registered).ToList();

            return View(sessionDelegates);
        }

        // GET: SessionDelegates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionDelegate sessionDelegate = db.SessionDelegates.Find(id);
            if (sessionDelegate == null)
            {
                return HttpNotFound();
            }
            return View(sessionDelegate);
        }

        // GET: SessionDelegates/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionList = new SelectList(db.Institutions, "Id", "Name").ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Title,InstitutionId,Address,Email,AgeGroup,MartialStatus,Gender,Telephone,RequireHousing,EmergencyContactName,EmergencyContactPhone,DelegateType,Allergies,Asthma,Diabetes,Vegetarian,HighBloodPressure,BronchialDisorder")] SessionDelegateViewModel sessionDelegateViewModel)
        {
            if (ModelState.IsValid)
            {
                SessionDelegate newMap = Mapper.Map<SessionDelegate>(sessionDelegateViewModel);

                newMap.DateAdded = DateTime.Now;

                db.SessionDelegates.Add(newMap);
                db.SaveChanges();

                ViewBag.Success = "1 Delegate Successfully Added to List.";
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionList = new SelectList(db.Institutions, "Id", "Name").ToList();
            return View(sessionDelegateViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionDelegate sessionDelegate = db.SessionDelegates.Find(id);
            if (sessionDelegate == null)
            {
                return HttpNotFound();
            }

            ViewBag.InstitutionList = new SelectList(db.Institutions, "Id", "Name").ToList();
            return View(sessionDelegate);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Title,DateAdded,InstitutionId,Address,Email,AgeGroup,MartialStatus,Gender,Telephone,RequireHousing,EmergencyContactName,EmergencyContactPhone,DelegateType,Allergies,Asthma,Diabetes,Vegetarian,HighBloodPressure,BronchialDisorder")] SessionDelegate sessionDelegate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionDelegate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sessionDelegate);
        }

        // GET: SessionDelegates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionDelegate sessionDelegate = db.SessionDelegates.Find(id);
            if (sessionDelegate == null)
            {
                return HttpNotFound();
            }
            return View(sessionDelegate);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionDelegate sessionDelegate = db.SessionDelegates.Find(id);
            db.SessionDelegates.Remove(sessionDelegate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
                
        public PartialViewResult _RegisterDelegate(int? id)
        {
            if (id == null)
            {
                
            }

            SessionDelegate sessionDelegate = db.SessionDelegates.Find(id);

            return PartialView(sessionDelegate);
        }
        
        [HttpPost, ActionName("_RegisterDelegate")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmRegistration(int id)
        {
            SessionDelegate sessionDelegate = db.SessionDelegates.Include(a=>a.Registered).FirstOrDefault(a=>a.Id == id);

            Registered NewRegistration = new Registered() {  SessionDelegateId = id, ApplicationUserId =User.Identity.GetUserId(), DateAndTime = DateTime.Now};

            db.Registered.Add(NewRegistration);        
            db.SaveChanges();

            ViewBag.Success = "1 Delegate Successfully Registered";
            return RedirectToAction("Index");
        }

        public PartialViewResult _CancelRegistration(int? id)
        {
            if (id == null)
            {

            }

            SessionDelegate sessionDelegate = db.SessionDelegates.Find(id);

            return PartialView(sessionDelegate);
        }

        [HttpPost, ActionName("_CancelRegistration")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmCancelRegistration(int id)
        {
            Registered RegistrationToDelete = db.Registered.FirstOrDefault(a=>a.SessionDelegateId == id);

            db.Registered.Remove(RegistrationToDelete);
            db.SaveChanges();

            ViewBag.Success = "1 Delegate Registration Successfully Cancelled";
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
