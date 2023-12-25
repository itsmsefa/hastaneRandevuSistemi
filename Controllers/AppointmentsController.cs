using Microsoft.AspNetCore.Mvc;
using hastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using hastaneRandevuSistemi.ViewModels;

namespace hastaneRandevuSistemi.Controllers
{
    public class AppointmentsController: Controller
    {
        private IdentityContext _identityContext;

        public AppointmentsController(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }
        public IActionResult Index()
        {
            var cityList = (from city in _identityContext.City
            select new SelectListItem()
            {
                Text = city.CityName,
                Value = city.CityId.ToString()
            }).ToList();
            cityList.Insert(0, new SelectListItem()
            {
                Text = "Select",
                Value = string.Empty,
                Selected = true
            });
            ViewBag.ListOfCities = cityList;
            return View();
        }

        [HttpPost]
        public IActionResult Index(CityViewModel cityViewModel)
        {
            var selectedValue = cityViewModel.CityId;
            return View(cityViewModel);
        }
    }
}