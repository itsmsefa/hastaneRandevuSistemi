using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using hastaneRandevuSistemi.Models;

namespace hastaneRandevuSistemi.ViewModels
{
    public class CreateAppointmentModel : PageModel
    {
        private readonly IdentityContext _context;

        public CreateAppointmentModel(IdentityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointments Appointment { get; set; }

        public void OnGet()
        {
            // This method can be used to initialize the page when it's loaded.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Appointments.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Redirect to a page after successful creation.
        }
    }
}
