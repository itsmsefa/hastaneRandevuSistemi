using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hastaneRandevuSistemi.Models
{
    [Table("City")]
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string? CityName { get; set; }
    }
}