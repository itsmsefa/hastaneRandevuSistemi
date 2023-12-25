using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hastaneRandevuSistemi.Models
{
    [Table("Hospital")]
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }
        
        [ForeignKey("City")]
        public int CityId { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public string? HospitalName { get; set; }
    }
}