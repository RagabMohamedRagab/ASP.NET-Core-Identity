using Identity.DAL.Domain_Modes;
using Identity.DAL.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL.Context {
    public class AppDbContext:IdentityDbContext<ApplicationUser> {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }    
        public virtual DbSet<Level> Levels  { get; set; }
        public virtual DbSet<Gender> Genders  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
