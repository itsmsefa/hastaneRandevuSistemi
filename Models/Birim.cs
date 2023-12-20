using System.ComponentModel.DataAnnotations;

namespace hastaneRandevuSistemi.Models
{
    public class Birim
    {
        [Key]
        public string BirimNo { get; set; } = null!;
        public string? BirimAd { get; set; }
    }
}