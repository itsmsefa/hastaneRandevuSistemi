using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hastaneRandevuSistemi.Models
{
    public class Birim
    {
        [Key]
        public string BirimNo { get; set; }
        public string? BirimAd { get; set; }
    }
}