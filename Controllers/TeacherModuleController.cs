using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using berkeley_collegee.Models;

namespace Berkely_College.Controllers
{
    public class TeacherModuleController : Controller
    {
        private readonly ModelContext _context;

        public TeacherModuleController(ModelContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id == "Nope")
            {
                id = (await _context.Teacher.FromSqlRaw($"select * from teacher").ToListAsync())[0].TeacherId;
            }
            ViewBag.Teacher = (await _context.Teacher.FromSqlRaw($"select * from teacher where teacher_id = '{id}'").ToListAsync())[0];
            ViewBag.Teacher.TeacherNavigation = (await _context.Person.FromSqlRaw($"select * from person where person_id = '{id}'").ToListAsync())[0];
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "TeacherId");
            ViewBag.modules = (await _context.Module.FromSqlRaw($"select * from module where module_code in  (select module_code from teacher_module where teacher_id = '{id}')").ToListAsync());
            return View();
        }

        
    }
}
