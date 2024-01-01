namespace hastaneRandevuSistemi.Models
{
    public class AppointmentsViewModel
    {
        public List<Appointments>? Appointments { get; set; }
        public AppointmentsViewModel()
        {
            Appointments = new List<Appointments>();
        }
        public List<City>? City { get; set; }
        public List<Department>? Department { get; set; }
        public string? HospitalId { get; set; }
        public string? DepartmentId { get; set; }
        public DateTime? Apt_Date { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }

    }
}