using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hastaneRandevuSistemi.Models
{
    [Table("District")]
    public class District
    {
        [Key]
        public int DistrictId { get; set; }
        
        public string? DistrictName { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; } = null!;
    }
}