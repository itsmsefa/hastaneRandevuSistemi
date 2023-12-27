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
        [Required]
        public string? HospitalName { get; set; }
        
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; } = null!;
    }
}