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
    public class CmcustomerCaresController : Controller
    {
        private readonly casemanagementContext _context;

        public CmcustomerCaresController(casemanagementContext context)
        {
            _context = context;
        }

        // GET: CmcustomerCares
        public async Task<IActionResult> Index()
        {
              return View(await _context.CmcustomerCares.ToListAsync());
        }

        // GET: CmcustomerCares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CmcustomerCares == null)
            {
                return NotFound();
            }

            var cmcustomerCare = await _context.CmcustomerCares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cmcustomerCare == null)
            {
                return NotFound();
            }

            return View(cmcustomerCare);
        }

        // GET: CmcustomerCares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CmcustomerCares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,CreatedAt,UpdatedAt,PhoneNumber")] CmcustomerCare cmcustomerCare)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cmcustomerCare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cmcustomerCare);
        }

        // GET: CmcustomerCares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CmcustomerCares == null)
            {
                return NotFound();
            }

            var cmcustomerCare = await _context.CmcustomerCares.FindAsync(id);
            if (cmcustomerCare == null)
            {
                return NotFound();
            }
            return View(cmcustomerCare);
        }

        // POST: CmcustomerCares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,CreatedAt,UpdatedAt,PhoneNumber")] CmcustomerCare cmcustomerCare)
        {
            if (id != cmcustomerCare.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cmcustomerCare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CmcustomerCareExists(cmcustomerCare.Id))
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
            return View(cmcustomerCare);
        }

        // GET: CmcustomerCares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CmcustomerCares == null)
            {
                return NotFound();
            }

            var cmcustomerCare = await _context.CmcustomerCares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cmcustomerCare == null)
            {
                return NotFound();
            }

            return View(cmcustomerCare);
        }

        // POST: CmcustomerCares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CmcustomerCares == null)
            {
                return Problem("Entity set 'casemanagementContext.CmcustomerCares'  is null.");
            }
            var cmcustomerCare = await _context.CmcustomerCares.FindAsync(id);
            if (cmcustomerCare != null)
            {
                _context.CmcustomerCares.Remove(cmcustomerCare);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CmcustomerCareExists(int id)
        {
          return _context.CmcustomerCares.Any(e => e.Id == id);
        }
    }
}
