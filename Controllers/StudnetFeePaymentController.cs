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
    public class StudentFeePaymentController : Controller
    {
        private readonly ModelContext _context;

        public StudentFeePaymentController(ModelContext context)
        {
            _context = context;
        }

        // GET: students
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id == "Nope")
            {
                id = (await _context.Student.FromSqlRaw($"select * from student").ToListAsync())[0].StudentId;
            }
            ViewBag.student = (await _context.Student.FromSqlRaw($"select * from student where student_id = '{id}'").ToListAsync())[0];
            ViewBag.student.StudentNavigation = (await _context.Person.FromSqlRaw($"select * from person where person_id = '{id}'").ToListAsync())[0];
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId");

            ViewBag.payments = (await _context.FeePayment.FromSqlRaw($"select * from Fee_Payment where Student_Id in  (select Student_ID from Fee_Payment where student_id = '{id}')").ToListAsync());

            return View();
        }


    }
}
