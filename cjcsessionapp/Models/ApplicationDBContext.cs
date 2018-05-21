using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace cjcsessionapp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<SessionDelegate> SessionDelegates { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Registered> Registered { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}