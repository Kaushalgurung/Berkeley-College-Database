using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using berkeley_collegee.Models;
using Microsoft.Extensions.Logging;
using Berkely_College.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace berkeley_collegee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ModelContext context,ILogger<HomeController> logger)

        {
            _logger = logger;
            _context = context;
        }

        
        public async Task<IActionResult> IndexAsync()
        {

            var modules = await _context.Module.FromSqlRaw($"select * from module").ToListAsync();
            List<DataPoint> data = new List<DataPoint>();
            foreach(var module in modules)
            {
                int x = (await _context.StudentModule.FromSqlRaw($"select * from student_module where module_code ='{module.ModuleCode}'").ToListAsync()).Count;
                string y = module.ModuleCode;
                data.Add(new DataPoint { X = x, Y = y });
            }
            ViewBag.dataPoints = JsonConvert.SerializeObject(data);
            ViewBag.ChartTitle = "Number of student studying each module";
            ViewBag.ChartSubTitle = "";
            ViewBag.ChartType = "bar";
            

            var students  = (await _context.Student.FromSqlRaw($"select * from Student").ToListAsync());
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            foreach (var student in students)
            {
                int x = (int)(await _context.StudentAttendence.FromSqlRaw($"select * from student_attendence where student_id ='{student.StudentId}'").ToListAsync())[0].Attendence;
                student.StudentNavigation = (await _context.Person.FromSqlRaw($"select * from person where person_id = '{student.StudentId}'").ToListAsync())[0];
                string y = student.StudentNavigation.FirstName;
                dataPoints2.Add(new DataPoint { X = x, Y = y });
            }
            ViewBag.dataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.ChartTitle2 = "Attendance of students ";
            ViewBag.ChartSubTitle2 = "";
            ViewBag.ChartType2 = "line";

            return View("~/Views/Chart/Index.cshtml");
        }
    public IActionResult Privacy()
        {
            try
            {
                return View();
            }
            catch
            {
                return null;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
