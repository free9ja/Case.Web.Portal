using Case.Web.Portal.Models;
using Case.Web.Portal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Case.Web.Portal.Controllers
{
    public class AdminCaseController : Controller
    {
        private readonly casemanagementContext _context;

        public AdminCaseController(casemanagementContext context)
        {
            _context = context;
        }
        // GET: AdminCaseContoller
        public async Task<IActionResult> Index()
        {
            var caseList = new List<AdminCase>();
            var caseContext = _context.Cmcases.Include(c=>c.CMcaseType).Include(x=>x.CMcustomer).Include(y=>y.CustomerCare);
            var cases = await caseContext.ToListAsync();

            foreach (Cmcase c in cases)
            {
                AdminCase x = new AdminCase();
                x.ID = c.Id;
                x.CustomerCare = c.CustomerCare;
                x.Customer = c.CMcustomer;
                x.Case = c;
                x.CaseType = c.CMcaseType;
                caseList.Add(x);

            }
           // var caseType = _context.CmcaseTypes.Where(c=>c.Id == );
            return View(caseList);
           
        }

        // GET: AdminCaseContoller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminCaseContoller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminCaseContoller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminCaseContoller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminCaseContoller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminCaseContoller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminCaseContoller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
