using Microsoft.AspNetCore.Mvc;
using hastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using hastaneRandevuSistemi.ViewModels;

namespace hastaneRandevuSistemi.Controllers
{
    public class AppointmentsController: Controller
    {
        private IdentityContext _context;

        public AppointmentsController(IdentityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<City> data = _context.City.ToList();
            List<Department> dprtdata = _context.Department.ToList();

            var cities = (from i in data select new SelectListItem() { Text = i.CityName, Value = i.CityId.ToString() 
            }).ToList();
            ViewData["cities"] = cities;

            var departments = (from i in dprtdata select new SelectListItem() { Text = i.DepartmentName, Value = i.DepartmentId.ToString()
            }).ToList();
            ViewData["departments"] = departments;

            return View();
        }

        [HttpPost]
        public IActionResult Index(int city, int district, int hospital, int department)
        {
            var cty = _context.City.Find(city);
            var dst = _context.District.Find(district);
            var hsp = _context.Hospital.Find(hospital);
            var dprt = _context.Department.Find(department);

            ViewData["message"] = "You selected: " + cty.CityName + " - " + dst.DistrictName + " - " + hsp.HospitalName + " - " + dprt.DepartmentName;

            List<City> data = _context.City?.ToList();
            var cities = (from i in data select new SelectListItem() { Text = i.CityName, Value = i.CityId.ToString() }).ToList();
            ViewData["cities"] = cities;

            List<Department> dprtdata = _context.Department.ToList();
            var departments = (from i in dprtdata
                               select new SelectListItem()
                               {
                                   Text = i.DepartmentName,
                                   Value = i.DepartmentId.ToString()
                               }).ToList();
            ViewData["departments"] = departments;

            return View();
        }

        public IActionResult GetDistricts(int id)
        {
            List<District> data = _context.District.Where(i => i.CityId == id).ToList();
            return Ok(data);
        }

        public IActionResult GetHospitals(int id)
        {
            List<Hospital> data = _context.Hospital.Where(i => i.DistrictId == id).ToList();
            return Ok(data);
        }
    }
}