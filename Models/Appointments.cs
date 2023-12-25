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
        public int AppoinmentId { get; set; }

        public DateTime StartDateTime { get; set; }

        public string? PatientFullName { get; set; }

        public string? DoctorFullName { get; set; }

        public string? Hospital { get; set; }

        public string? DepartmentId { get; set; }

    }
}