using Microsoft.AspNetCore.Identity;

namespace hastaneRandevuSistemi.Models
{
    public class AppUser: IdentityUser
    {
        public string? FullName { get; set; }

    }
}