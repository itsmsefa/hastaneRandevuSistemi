using hastaneRandevuSistemi.Models;

namespace hastaneRandevuSistemi.ViewModels
{
    public class SearchViewModel
    {
        public List<Appointments>? SearchResults { get; set; }
        public string? DoctorFullName { get; set; }
        public string? HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime Apt_Date { get; set; }
    }
}
