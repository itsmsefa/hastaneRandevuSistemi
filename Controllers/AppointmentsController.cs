using Microsoft.AspNetCore.Mvc;
using hastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using hastaneRandevuSistemi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
            var viewModel = new AppointmentsViewModel
            {
                Appointments = new List<Appointments>()
            };

            return View(viewModel);
        }

        public IActionResult Search()
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
        public async Task<IActionResult> Search(string hospital, string department, DateTime? Apt_Date)
        {
            var query = _context.Appointments.AsQueryable();

            if (!string.IsNullOrEmpty(hospital))
                query = query.Where(a => a.HospitalId.ToLower() == hospital.ToLower());

            if (!string.IsNullOrEmpty(department))
                query = query.Where(a => a.DepartmentId.ToLower() == department.ToLower());

            if (Apt_Date.HasValue)
                query = query.Where(a => a.Apt_Date.Date == Apt_Date.Value.Date);

            Console.WriteLine(query.ToQueryString());

            var result = await query.ToListAsync() ?? new List<Appointments>();
            var viewModel = new AppointmentsViewModel
            {
                Appointments = result
            };


            return RedirectToAction("Index", viewModel);
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

        // GET: Appointments/Create
        public IActionResult Create()
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
        public async Task<IActionResult> Create(int city, int district, string hospital, string department, DateTime Apt_Date)
        {
            if (ModelState.IsValid)
            {
                var newAppointment = new Appointments
                {
                    CityId = city,
                    DistrictId = district,
                    HospitalId = hospital,
                    DepartmentId = department,
                    Apt_Date = Apt_Date
                };

                // Add the new appointment to the database
                _context.Appointments.Add(newAppointment);
                await _context.SaveChangesAsync();

                // Redirect to a confirmation page or the index
                TempData["message"] = "Appointment created successfully!";
                return RedirectToAction("Create"); // or any other view
            }

            // If validation fails, reload the form with initial data
            TempData["message"] = "There has been a problem please try again!";
            return RedirectToAction("Create");
        }

    }
}