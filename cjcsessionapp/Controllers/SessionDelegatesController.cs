using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using cjcsessionapp.Models;

namespace cjcsessionapp.Controllers
{
    public class SessionDelegatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SessionDelegates
        public ActionResult Index()
        {
            return View(db.SessionDelegates.ToList());
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
            return View();
        }

        // POST: SessionDelegates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Title,Email,Age,Gender,Telephone,RequireHousing,EmergencyContactName,EmergencyContactPhone,DelegateType")] SessionDelegate sessionDelegate)
        {
            if (ModelState.IsValid)
            {
                db.SessionDelegates.Add(sessionDelegate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sessionDelegate);
        }

        // GET: SessionDelegates/Edit/5
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
            return View(sessionDelegate);
        }

        // POST: SessionDelegates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Title,Email,Age,Gender,Telephone,RequireHousing,EmergencyContactName,EmergencyContactPhone,DelegateType")] SessionDelegate sessionDelegate)
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

        // POST: SessionDelegates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionDelegate sessionDelegate = db.SessionDelegates.Find(id);
            db.SessionDelegates.Remove(sessionDelegate);
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
