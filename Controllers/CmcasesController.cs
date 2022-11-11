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
    public class CmcasesController : Controller
    {
        private readonly casemanagementContext _context;

        public CmcasesController(casemanagementContext context)
        {
            _context = context;
        }

        // GET: Cmcases
        public async Task<IActionResult> Index()
        {
            var casemanagementContext = _context.Cmcases.Include(c => c.CMcaseType).Include(c => c.CMcustomer).Include(c => c.CustomerCare);
            return View(await casemanagementContext.ToListAsync());
        }

        // GET: Cmcases/Details/5
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

        // GET: Cmcases/Create
        public IActionResult Create(int userId)
        {
            ViewData["CMcaseTypeId"] = new SelectList(_context.CmcaseTypes, "Id", "Name");
            ViewData["CMcaseType"] = new SelectList(_context.CmcaseTypes, "Id", "Name");
            ViewData["CMcustomerId"] = new SelectList(_context.Cmcustomers, "Id", "Email");
            ViewData["CustomerCareId"] = new SelectList(_context.CmcustomerCares, "Id", "Email");
            ViewData["CustomerId"] = userId;

            return View();
        }

        // POST: Cmcases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var customers = (from customer in _context.Cmcustomers
                             where customer.FirstName!.StartsWith(prefix)
                             select new
                             {
                                 label = customer.Email,
                                 val = customer.Id
                             }).ToList();

            return Json(customers);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromQuery] int userId,[Bind("Id,Description,CustomerEmail,CaseTypeId,CustomerId")] CreateCase cmcase )
        {
            if (ModelState.IsValid)
            {
                var c = new Cmcase();
                var cs = _context.Cmcustomers.Where(c => c.Id == cmcase!.CustomerId).FirstOrDefault();
                var t = _context.CmcaseTypes.Where(c => c.Id == cmcase.CaseTypeId).FirstOrDefault();
                c.CMcustomer = cs!;
                c.Status = "Submitted";
                c.State = 0;
                c.CMcaseType = t;
                c.CMcaseTypeId = cmcase.CaseTypeId;
                c.Description = cmcase.Description;
                c.CustomerCare = _context.CmcustomerCares.Where(c=>c.Id == 1).FirstOrDefault();
                c.CreatedAt = DateTime.Now;
                c.ResolvedAt = DateTime.Now;
                _context.Add(c);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CMcaseTypeId"] = new SelectList(_context.CmcaseTypes, "Id", "Name", cmcase.CMcaseTypeId);
            ViewData["CMcustomerId"] = new SelectList(_context.Cmcustomers, "Id", "Email", cmcase.CMcustomer);
            //ViewData["CustomerCareId"] = new SelectList(_context.CmcustomerCares, "Id", "Email", cmcase.CustomerCare);
            return View(cmcase);
        }

        // GET: Cmcases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cmcases == null)
            {
                return NotFound();
            }

            var cmcase = await _context.Cmcases.FindAsync(id);
            var caseType = await _context.CmcaseTypes.FindAsync(cmcase.CaseTypeId);
            var c = new CreateCase();
            if (cmcase == null)
            {
                return NotFound();
            }
            if (cmcase.CustomerCare == null)
            {
                c.CustomerCare = null;
                c.CustomerCareEmail = "";
            }
            else{
                c.CustomerCare = await _context.CmcustomerCares.FindAsync(cmcase.CustomerCare!.Id);
                c.CustomerCareEmail = c.CustomerCare!.Email;
            }
            
            c.Id = cmcase.Id;
            c.CMcustomer = await _context.Cmcustomers.FindAsync(cmcase.Id);
            c.Status = cmcase.Status;
            c.ResolvedAt = cmcase.ResolvedAt;
           // c.CustomerCare= == null ? null : await _context.CmcustomerCares.FindAsync(cmcase.CustomerCare!.Id);
            c.Description = cmcase.Description;
            c.CaseTypeId = cmcase.CaseTypeId;

            c.CustomerEmail = c.CMcustomer!.Email;
            c.CaseTypeName = caseType.Name;
            

            ViewData["CMcaseTypeId"] = new SelectList(_context.CmcaseTypes, "Id", "Name", cmcase.CMcaseTypeId);
            ViewData["CaseTypeName"] = new SelectList(_context.Cmcustomers, "Id", "Email", cmcase.CMcaseType.Name);
            ViewData["CustomerCareId"] = new SelectList(_context.CmcustomerCares, "Id", "Email", cmcase.CustomerCare);
            return View(c);
        }

        // POST: Cmcases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,CustomerEmail,CaseTypeId,Status")] CreateCase cmcase)
        {


            if (id != cmcase.Id)
            {
                return NotFound();
            }



            var c = await _context.Cmcases.FindAsync(id);

            if (ModelState.IsValid)
            {
                try
                {
                   // var c = new Cmcase();
                    var cs = _context.Cmcustomers.Where(c => c.Email == cmcase.CustomerEmail).FirstOrDefault();
                    var t = _context.CmcaseTypes.Where(c => c.Id == cmcase.CaseTypeId).FirstOrDefault();
                    c!.CMcustomer = cs!;
                    c.Status = cmcase.Status!;
                    c.State = 0;
                    c.CMcaseType = t!;
                    c.CMcaseTypeId = cmcase.CaseTypeId;
                    c.Description = cmcase.Description;
                   // c.

                    if (c.CustomerCare == null)
                    {
                        c.CustomerCare = null;
                        //c.cm = "";
                    }
                    else
                    {
                        c.CustomerCare = await _context.CmcustomerCares.FindAsync(cmcase.CustomerCare!.Id);
                       // c.CustomerCareEmail = c.CustomerCare!.Email;
                    }

                   // c.CustomerCare = _context.CmcustomerCares.Where(c => c.Id == cmcase.CustomerCare!.Id).FirstOrDefault()!.Id != 1 ? _context.CmcustomerCares.Where(c => c.Id == cmcase.CustomerCare!.Id).FirstOrDefault() : _context.CmcustomerCares.Where(c => c.Id == 1).FirstOrDefault();
                   // c.CreatedAt = DateTime.Now;
                    c.ResolvedAt = DateTime.Now;
                    _context.Update(c);
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
            ViewData["CMcaseTypeId"] = new SelectList(_context.CmcaseTypes, "Id", "Name", c!.CMcaseTypeId);
            ViewData["CMcustomerId"] = new SelectList(_context.Cmcustomers, "Id", "Email", c.CMcustomer);
            ViewData["CustomerCareId"] = new SelectList(_context.CmcustomerCares, "Id", "Email", c.CustomerCare);
            return View(cmcase);
        }

        // GET: Cmcases/Delete/5
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


        public async Task<IActionResult> Assign(int? id)
        {
            if (id == null || _context.Cmcases == null)
            {
                return NotFound();
            }

            //var cm = await _context.Cmcases
            //    .Include(c => c.CMcaseType)
            //    .Include(c => c.CMcustomer)
            //    .Include(c => c.CustomerCare)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //cmcase!.CustomerCare = await _context.CmcustomerCares.FindAsync(CustomerCareID);

            ViewData["CaseID"] = id;
            //cmcase.Status = "Assigned";
            // _context.Update(cmcase);


            var ccares = await _context.CmcustomerCares.ToListAsync();



          

            return View(ccares);
        }

        public async Task<IActionResult> doAssign(int? caseId, int CustomerCareId)
        {
            if (caseId == null || _context.Cmcases == null)
            {
                return NotFound();
            }

            var cm = await _context.Cmcases
                .Include(c => c.CMcaseType)
                .Include(c => c.CMcustomer)
                .Include(c => c.CustomerCare)
                .FirstOrDefaultAsync(m => m.Id == caseId);
            cm!.CustomerCare = await _context.CmcustomerCares.FindAsync(CustomerCareId);

            ViewData["CaseID"] = caseId;
            cm.Status = "Assigned";
            _context.Update(cm);
            ViewData["CaseType"] = cm.CMcaseType.Name;
            ViewData["Customer"] = cm!.CMcustomer!.FirstName + " " + cm!.CMcustomer!.LastName + " " + cm!.CMcustomer.Email;
            ViewData["CustomerCareName"] = cm!.CustomerCare!.FirstName + " " + cm!.CustomerCare.LastName + " " + cm!.CustomerCare.Email;
            var ccares = await _context.CmcustomerCares.ToListAsync();





            return View(cm);
        }

        // POST: Cmcases/Delete/5
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
