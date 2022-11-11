using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Case.Web.Portal.Models;

namespace Case.Web.Portal.Controllers
{
    public class CmcustomersController : Controller
    {
        private readonly casemanagementContext _context;

        public CmcustomersController(casemanagementContext context)
        {
            _context = context;
        }

        // GET: Cmcustomers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cmcustomers.ToListAsync());
        }

        // GET: Cmcustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cmcustomers == null)
            {
                return NotFound();
            }

            var cmcustomer = await _context.Cmcustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cmcustomer == null)
            {
                return NotFound();
            }

            return View(cmcustomer);
        }

        // GET: Cmcustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cmcustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,CreatedAt,UpdatedAt,PhoneNumber")] Cmcustomer cmcustomer)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(cmcustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cmcustomer);
        }

        // GET: Cmcustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cmcustomers == null)
            {
                return NotFound();
            }

            var cmcustomer = await _context.Cmcustomers.FindAsync(id);
            if (cmcustomer == null)
            {
                return NotFound();
            }
            return View(cmcustomer);
        }

        // POST: Cmcustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,CreatedAt,UpdatedAt,PhoneNumber")] Cmcustomer cmcustomer)
        {
            if (id != cmcustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cmcustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CmcustomerExists(cmcustomer.Id))
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
            return View(cmcustomer);
        }

        // GET: Cmcustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cmcustomers == null)
            {
                return NotFound();
            }

            var cmcustomer = await _context.Cmcustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cmcustomer == null)
            {
                return NotFound();
            }

            return View(cmcustomer);
        }

        // POST: Cmcustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cmcustomers == null)
            {
                return Problem("Entity set 'casemanagementContext.Cmcustomers'  is null.");
            }
            var cmcustomer = await _context.Cmcustomers.FindAsync(id);
            if (cmcustomer != null)
            {
                _context.Cmcustomers.Remove(cmcustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CmcustomerExists(int id)
        {
          return _context.Cmcustomers.Any(e => e.Id == id);
        }
    }
}
