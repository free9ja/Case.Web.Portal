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
    public class UserCasesController : Controller
    {
        private readonly casemanagementContext _context;

        public UserCasesController(casemanagementContext context)
        {
            _context = context;
        }

        // GET: UserCases
        public async Task<IActionResult> Index(int? UserId)
        {
            var casemanagementContext = _context.Cmcases.Include(c => c.CMcaseType).Include(c => c.CMcustomer).Include(c => c.CustomerCare).Where(c=>c.CMcustomerId==UserId);
            return View(await casemanagementContext.ToListAsync());
        }

        // GET: UserCases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cmcases == null)
            {
                return NotFound();
            }

            var cmcase = await _context.Cmcases
                .Include(c => c.CMcaseType)
                .Include(c => c.CMcustomer)
                .Include(c => c.CustomerCare)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cmcase == null)
            {
                return NotFound();
            }

            return View(cmcase);
        }

        // GET: UserCases/Create
        public IActionResult Create()
        {
            ViewData["CMcaseTypeId"] = new SelectList(_context.CmcaseTypes, "Id", "Id");
            ViewData["CMcustomerId"] = new SelectList(_context.Cmcustomers, "Id", "Id");
            ViewData["CustomerCareId"] = new SelectList(_context.CmcustomerCares, "Id", "Id");
            return View();
        }

        // POST: UserCases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,CreatedAt,ResolvedAt,Status,CaseTypeId,CMcaseTypeId,CustomerId,CMcustomerId,CustomerCareId,State")] Cmcase cmcase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cmcase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CMcaseTypeId"] = new SelectList(_context.CmcaseTypes, "Id", "Id", cmcase.CMcaseTypeId);
            ViewData["CMcustomerId"] = new SelectList(_context.Cmcustomers, "Id", "Id", cmcase.CMcustomerId);
            ViewData["CustomerCareId"] = new SelectList(_context.CmcustomerCares, "Id", "Id", cmcase.CustomerCareId);
            return View(cmcase);
        }

        // GET: UserCases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cmcases == null)
            {
                return NotFound();
            }

            var cmcase = await _context.Cmcases.FindAsync(id);
            if (cmcase == null)
            {
                return NotFound();
            }
            ViewData["CMcaseTypeId"] = new SelectList(_context.CmcaseTypes, "Id", "Id", cmcase.CMcaseTypeId);
            ViewData["CMcustomerId"] = new SelectList(_context.Cmcustomers, "Id", "Id", cmcase.CMcustomerId);
            ViewData["CustomerCareId"] = new SelectList(_context.CmcustomerCares, "Id", "Id", cmcase.CustomerCareId);
            return View(cmcase);
        }

        // POST: UserCases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,CreatedAt,ResolvedAt,Status,CaseTypeId,CMcaseTypeId,CustomerId,CMcustomerId,CustomerCareId,State")] Cmcase cmcase)
        {
            if (id != cmcase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cmcase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CmcaseExists(cmcase.Id))
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
            ViewData["CMcaseTypeId"] = new SelectList(_context.CmcaseTypes, "Id", "Id", cmcase.CMcaseTypeId);
            ViewData["CMcustomerId"] = new SelectList(_context.Cmcustomers, "Id", "Id", cmcase.CMcustomerId);
            ViewData["CustomerCareId"] = new SelectList(_context.CmcustomerCares, "Id", "Id", cmcase.CustomerCareId);
            return View(cmcase);
        }

        // GET: UserCases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cmcases == null)
            {
                return NotFound();
            }

            var cmcase = await _context.Cmcases
                .Include(c => c.CMcaseType)
                .Include(c => c.CMcustomer)
                .Include(c => c.CustomerCare)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cmcase == null)
            {
                return NotFound();
            }

            return View(cmcase);
        }

        // POST: UserCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cmcases == null)
            {
                return Problem("Entity set 'casemanagementContext.Cmcases'  is null.");
            }
            var cmcase = await _context.Cmcases.FindAsync(id);
            if (cmcase != null)
            {
                _context.Cmcases.Remove(cmcase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CmcaseExists(int id)
        {
          return _context.Cmcases.Any(e => e.Id == id);
        }
    }
}
