using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hastaneRandevuSistemi.Models
{
    [Table("Appointments")]
    public class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime Apt_Date { get; set; }
        public string? DoctorFullName { get; set; }
        public string? HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}