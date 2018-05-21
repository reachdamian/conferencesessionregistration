using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using cjcsessionapp.Models;
using cjcsessionapp;

namespace cjcdonate.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUsersController()
        {

        }

        public ApplicationUsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }

            set
            {
                _roleManager = value;
            }
        }

        [Authorize]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        //// GET: ApplicationUsers/Create
        //public ActionResult Create()
        //{
        //    ViewBag.OrganizationAccountInformationId = new SelectList(db.OrganizationAccountInformation, "Id", "NameofChurch");
        //    return View();
        //}

        //// POST: ApplicationUsers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Telephone,Address,City,State,ZipCode,OrganizationAccountInformationId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ApplicationUsers.Add(applicationUser);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.OrganizationAccountInformationId = new SelectList(db.OrganizationAccountInformation, "Id", "NameofChurch", applicationUser.OrganizationAccountInformationId);
        //    return View(applicationUser);
        //}

        [Authorize]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(applicationUser.Id);

            applicationUser.RolesList = RoleManager.Roles.ToList().Select(r => new SelectListItem
            {
                Selected = userRoles.Contains(r.Name),
                Text = r.Name,
                Value = r.Name
            });

            
            //ViewBag.OrganizationAccountInformationId = new SelectList(db.OrganizationalAccountInformation, "Id", "NameofChurch", applicationUser.OrganizationAccountInformationId);
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]        
        public async Task<ActionResult> Edit([Bind(Include = "Id")] ApplicationUser applicationUser, params string[] RolesSelectedOnView)
        {
            if (ModelState.IsValid)
            {
                var rolesCurrentlyPersistedForUser = await UserManager.GetRolesAsync(applicationUser.Id);
                bool isThisUserAnAdmin = rolesCurrentlyPersistedForUser.Contains("Admin");

                RolesSelectedOnView = RolesSelectedOnView ?? new string[] { };
                bool isThisUserAdminDeselected = !RolesSelectedOnView.Contains("Admin");

                var role = await RoleManager.FindByNameAsync("Admin");
                bool isOnlyOneUserAnAdmin = role.Users.Count == 1;

                applicationUser = await UserManager.FindByIdAsync(applicationUser.Id);
                applicationUser.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = rolesCurrentlyPersistedForUser.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                });

                if(isThisUserAnAdmin && isThisUserAdminDeselected && isOnlyOneUserAnAdmin)
                {
                    ModelState.AddModelError("", "At least one user must retain Admin role; you are attempting to delete the Admin role for the last user who is Admin.");
                    return View(applicationUser);
                }
                

                var result = await UserManager.AddToRolesAsync(applicationUser.Id, RolesSelectedOnView.Except(rolesCurrentlyPersistedForUser).ToArray());

                if(!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(applicationUser);
                }

                

                result = await UserManager.RemoveFromRolesAsync(applicationUser.Id, rolesCurrentlyPersistedForUser.Except(RolesSelectedOnView).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(applicationUser);
                }

                if (rolesCurrentlyPersistedForUser.Count == 0)
                {
                    //user was not previously assigned a role
                    if (RolesSelectedOnView.Contains("User"))
                    {
                        var callbackUrl = Url.Action("Index", "Home", null, protocol: Request.Url.Scheme);                        

                        //user is now assigned a role
                        //send message to user
                        IdentityMessage msg = new IdentityMessage()
                        { 
                            Destination = applicationUser.Email,
                            Body = "You have now been assigned a role by the Risk Manager. You can now access the system to submit a claim. <br>" + callbackUrl,
                            Subject = "User Role Assigned"
                        };

                        EmailService email = new EmailService();
                        await email.SendAsync(msg);
                    }
                }

                return RedirectToAction("Index");
            }
            
            return View(applicationUser);
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<ActionResult> LockAccount([Bind(Include ="Id")] string id)
        {
            await UserManager.ResetAccessFailedCountAsync(id);
            await UserManager.SetLockoutEndDateAsync(id, DateTime.UtcNow.AddYears(100));

            TempData["AccountLocked"] = "Account was Locked successfully.";
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<ActionResult> UnlockAccount([Bind(Include = "Id")] string id)
        {
            await UserManager.ResetAccessFailedCountAsync(id);
            await UserManager.SetLockoutEndDateAsync(id, DateTime.UtcNow.AddYears(-1));

            TempData["AccountUnlocked"] = "Account was UnLocked successfully.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
