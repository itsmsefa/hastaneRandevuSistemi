using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace hastaneRandevuSistemi.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Admin_123";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();

            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(adminUser);

            if(user == null)
            {
                user = new AppUser
                {
                    FullName = "Sefa Ozdemir",
                    UserName = adminUser,
                    Email = "muhammed.ozdemir12@ogr.sakarya.edu.tr",
                    PhoneNumber = "05555555555"

                };
                
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}