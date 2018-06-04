using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using cjcsessionapp.Infrastructure;
using cjcsessionapp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace cjcsessionapp.Controllers
{
    [CustAuthFilter(Roles = "Admin, Registrar")]
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
        
        public async Task<ActionResult> Index()
        {     
            var sessionDelegates = await db.SessionDelegates.Include(a => a.Registered).OrderBy(m=>m.LastName).ToListAsync();

            return View(sessionDelegates);
        }
        
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SessionDelegate sessionDelegate = await db.SessionDelegates.FindAsync(id);
            if (sessionDelegate == null)
            {
                return HttpNotFound();
            }
            return View(sessionDelegate);
        }

        // GET: SessionDelegates/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionList = new SelectList(db.Institutions.OrderBy(v=>v.Name), "Id", "Name").ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,NameTitle,InstitutionId,Address,Email,AgeGroup,MartialStatus,Gender,Telephone,RequireHousing,EmergencyContactName,EmergencyContactPhone,DelegateType,Allergies,Asthma,Diabetes,Vegetarian,HighBloodPressure,BronchialDisorder")] SessionDelegateViewModel sessionDelegateViewModel)
        {
            if (ModelState.IsValid)
            {
                SessionDelegate newMap = Mapper.Map<SessionDelegate>(sessionDelegateViewModel);

                newMap.DateAdded = DateTime.Now;

                db.SessionDelegates.Add(newMap);
                await db.SaveChangesAsync();

                TempData["Success"] = "1 Delegate Successfully Added to List.";
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionList = new SelectList(db.Institutions, "Id", "Name").ToList();
            return View(sessionDelegateViewModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionDelegate sessionDelegate = await db.SessionDelegates.FindAsync(id);
            if (sessionDelegate == null)
            {
                return HttpNotFound();
            }

            ViewBag.InstitutionList = new SelectList(db.Institutions.OrderBy(v => v.Name), "Id", "Name").ToList();
            return View(sessionDelegate);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,NameTitle,DateAdded,InstitutionId,Address,Email,AgeGroup,MartialStatus,Gender,Telephone,RequireHousing,EmergencyContactName,EmergencyContactPhone,DelegateType,Allergies,Asthma,Diabetes,Vegetarian,HighBloodPressure,BronchialDisorder")] SessionDelegate sessionDelegate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionDelegate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Success"] = sessionDelegate.FirstName + " " + sessionDelegate.LastName + " record successfully updated.";

                return RedirectToAction("Index");
            }

            return View(sessionDelegate);
        }

        // GET: SessionDelegates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SessionDelegate sessionDelegate = await db.SessionDelegates.FindAsync(id);

            if (sessionDelegate == null)
            {
                return HttpNotFound();
            }
            return View(sessionDelegate);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SessionDelegate sessionDelegate = await db.SessionDelegates.FindAsync(id);
            db.SessionDelegates.Remove(sessionDelegate);
            await db.SaveChangesAsync();

            TempData["Success"] = "1 Delegate Information deleted!";
            return RedirectToAction("Index");
        }
                
        public async Task<ActionResult> RegisterDelegate(int? id)
        {
            if (id == null)
            {
                
            }

            SessionDelegate sessionDelegate = await db.SessionDelegates.FindAsync(id);

            return View(sessionDelegate);
        }
        
        [HttpPost, ActionName("RegisterDelegate")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmRegistration(int id)
        {
            SessionDelegate sessionDelegate = await db.SessionDelegates.Include(a=>a.Registered).FirstOrDefaultAsync(a=>a.Id == id);

            Registered NewRegistration = new Registered() {  SessionDelegateId = id, ApplicationUserId =User.Identity.GetUserId(), DateAndTime = DateTime.Now};

            db.Registered.Add(NewRegistration);        
            await db.SaveChangesAsync();

            //send email to user
            string delegateEmail = sessionDelegate.Email;
            if (!string.IsNullOrEmpty(delegateEmail))
            {
                //send email
                await UserManager.SendEmailAsync(User.Identity.GetUserId(), "Welcome to CJC Conference Session", "You have been registered for the 5th Quadrennial Conference Session. Welcome!");

            }

            TempData["Success"] = "1 Delegate Successfully Registered";
            return RedirectToAction("Index");
        }

        [CustAuthFilter(Roles = "Admin")]
        public async Task<ActionResult> CancelRegistration(int? id)
        {
            if (id == null)
            {

            }

            SessionDelegate sessionDelegate = await db.SessionDelegates.FindAsync(id);

            return View(sessionDelegate);
        }

        [HttpPost, ActionName("CancelRegistration")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmCancelRegistration(int id)
        {
            Registered RegistrationToDelete = await db.Registered.FirstOrDefaultAsync(a=>a.SessionDelegateId == id);

            db.Registered.Remove(RegistrationToDelete);
            await db.SaveChangesAsync();

            TempData["Success"] = "1 Delegate Registration Successfully Cancelled";
            return RedirectToAction("Index");
        }

        [CustAuthFilter(Roles = "Admin")]
        public ActionResult LoadDelegates()
        {
            string fileName = "delegateslist.csv";
            string directoryPath = Server.MapPath("~");

            StreamReader stream = new StreamReader(directoryPath + fileName);
            int counter = 0;

            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine();
                var value = line.Split(',');

                if (value != null)
                {
                    SessionDelegate newSessionDelegate = new SessionDelegate()
                    {
                        NameTitle = value[0],
                        FirstName = value[1],
                        LastName = value[2],
                        InstitutionId = Convert.ToInt32(value[3]),
                        DelegateType = value[4],
                        DateAdded = DateTime.Now
                    };

                    db.SessionDelegates.Add(newSessionDelegate);
                    db.SaveChanges();
                    counter += 1;
                }
            }

            ViewBag.Success = counter + " SessionDelegates were successfully added.";
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
