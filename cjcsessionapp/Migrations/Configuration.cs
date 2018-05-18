namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<cjcsessionapp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(cjcsessionapp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method v
            //  to avoid creating duplicate seed data.

            context.SessionDelegates.AddOrUpdate(x => x.Id,
                new Models.SessionDelegate() { FirstName = "Damian", LastName = "Chambers", DelegateType = "Regular", InstitutionId = 1 },
                new Models.SessionDelegate() { FirstName = "John", LastName = "Brown", DelegateType = "Regular", InstitutionId = 1 },
                new Models.SessionDelegate() { FirstName = "Mary", LastName = "Jane", DelegateType = "Regular", InstitutionId = 2 }
                );

            context.Institutions.AddOrUpdate(c => c.Id,
                new Models.Institution() { Name = "Spanish Town", NumberOfDelegatesAssigned = 5 },
                new Models.Institution() { Name = "Mandeville", NumberOfDelegatesAssigned = 4 });
        }
    }
}
