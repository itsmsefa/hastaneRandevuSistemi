using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace hastaneRandevuSistemi.Models
{
    public class IdentityContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options)
        {

        }

        public DbSet<City>? City { get; set; }
        public DbSet<District>? District { get; set; }
        public DbSet<Hospital>? Hospital { get; set; }
        public DbSet<Department>? Department { get; set; }
    }
}