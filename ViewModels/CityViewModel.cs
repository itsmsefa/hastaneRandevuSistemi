using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hastaneRandevuSistemi.ViewModels
{
    public class CityViewModel
    {
        [DisplayName("City")]
        public string? CityId { get; set; }
        public List<SelectListItem>? ListOfCities { get; set; }
    }
}