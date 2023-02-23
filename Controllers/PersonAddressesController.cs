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
    public class PersonAddressesController : Controller
    {
        private readonly ModelContext _context;

        public PersonAddressesController(ModelContext context)
        {
            _context = context;
        }

        // GET: PersonAddresses
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.PersonAddress.Include(p => p.Address).Include(p => p.Person);
            return View(await modelContext.ToListAsync());
        }

        // GET: PersonAddresses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddress
                .Include(p => p.Address)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (personAddress == null)
            {
                return NotFound();
            }

            return View(personAddress);
        }

        // GET: PersonAddresses/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId");
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "PersonId");
            return View();
        }

        // POST: PersonAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,AddressId")] PersonAddress personAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", personAddress.AddressId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "PersonId", personAddress.PersonId);
            return View(personAddress);
        }

        // GET: PersonAddresses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddress.FindAsync(id);
            if (personAddress == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", personAddress.AddressId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "PersonId", personAddress.PersonId);
            return View(personAddress);
        }

        // POST: PersonAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,AddressId")] PersonAddress personAddress)
        {
            if (id != personAddress.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonAddressExists(personAddress.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", personAddress.AddressId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "PersonId", personAddress.PersonId);
            return View(personAddress);
        }

        // GET: PersonAddresses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddress
                .Include(p => p.Address)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (personAddress == null)
            {
                return NotFound();
            }

            return View(personAddress);
        }

        // POST: PersonAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var personAddress = await _context.PersonAddress.FindAsync(id);
            _context.PersonAddress.Remove(personAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonAddressExists(string id)
        {
            return _context.PersonAddress.Any(e => e.PersonId == id);
        }
    }
}
