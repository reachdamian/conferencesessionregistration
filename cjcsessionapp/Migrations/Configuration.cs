namespace cjcsessionapp.Migrations
{
    using cjcsessionapp.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<cjcsessionapp.Models.ApplicationDbContext>
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.SessionDelegates.AddOrUpdate(x => x.Id,
                new SessionDelegate() { FirstName = "Damian", LastName = "Chambers", DelegateType = "Regular", InstitutionId = 1, DateAdded = DateTime.Now },
                new SessionDelegate() { FirstName = "John", LastName = "Brown", DelegateType = "Regular", InstitutionId = 1, DateAdded = DateTime.Now },
                new SessionDelegate() { FirstName = "Mary", LastName = "Jane", DelegateType = "Regular", InstitutionId = 2, DateAdded = DateTime.Now }
                );

            context.Institutions.AddOrUpdate(c => c.Id,
                new Institution() { Name = "Spanish Town", NumberOfDelegatesAssigned = 5 },
                new Institution() { Name = "Mandeville", NumberOfDelegatesAssigned = 4 });


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext()));

            string name = "damian.chambers@yahoo.com";
            string password = "Ricardo@119";
            string firstName = "Damian";
            string lastName = "Chambers";                     
            string roleName = "Admin";

            string name2 = "tashanidavidson40@gmail.com";
            string password2 = "Tashani123";
            string firstName2 = "Tashani";
            string lastName2 = "Davidson";
            

            var role = roleManager.FindByName(roleName);

            if (role == null)
            {
                role = new ApplicationRole(roleName);
                var roleResult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            var user2 = userManager.FindByName(name2);


            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, FirstName = firstName, LastName = lastName, EmailConfirmed = true };
                var result = userManager.Create(user, password);

                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            if (user2 == null)
            {
                user2 = new ApplicationUser { UserName = name2, Email = name2, FirstName = firstName2, LastName = lastName2, EmailConfirmed = true };
                var result = userManager.Create(user2, password2);

                result = userManager.SetLockoutEnabled(user2.Id, false);
            }

            var rolesForUser = userManager.GetRoles(user.Id);

            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
                var result2 = userManager.AddToRole(user2.Id, role.Name);
            }

        }
    }
}
