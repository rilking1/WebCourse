using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Services;



namespace WebApplication4.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DbcoursesContext _context;
        private IDataPortServiceFactory<Category> _categoryDataPortServiceFactory; // Declare without initialization

        public CategoriesController(DbcoursesContext context)
        {
            _context = context;
            _categoryDataPortServiceFactory = new CategoryDataPortServiceFactory(_context); // Initialize in the constructor
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorys.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category1")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categorys.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category1")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categorys.FindAsync(id);
            if (category != null)
            {
                _context.Categorys.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categorys.Any(e => e.Id == id);
        }

        //[HttpGet]
        //public IActionResult Import() { return View(); }

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<ActionResult> Import(IFormFile fileExel, CancellationToken cancellationToken = default)
        //{
        //    var importService = _categoryDataPortServiceFactory.GetImportService(fileExel.ContentType);

        //    using var stream = fileExel.OpenReadStream();


        //}
        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel, CancellationToken cancellationToken = default)
        {
            var importService = _categoryDataPortServiceFactory.GetImportService(fileExcel.ContentType);

            using var stream = fileExcel.OpenReadStream();

            await importService.ImportFromStreamAsync(stream, cancellationToken);

            return RedirectToAction(nameof(Index));
        }





        //    [HttpGet]
        //    public async Task<IActionResult> Export([FromQuery] string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //CancellationToken cancellationToken = default)
        //    {
        //        var exportService = _categoryDataPortServiceFactory.GetExportService(contentType);

        //        var memoryStream = new MemoryStream();

        //        await exportService.WriteToAsync(memoryStream, cancellationToken);

        //        await memoryStream.FlushAsync(cancellationToken);
        //        memoryStream.Position = 0;


        //        return new FileStreamResult(memoryStream, contentType)
        //        {
        //            FileDownloadName = $"categiries_{DateTime.UtcNow.ToShortDateString()}.xlsx"
        //        };
        //    }








    }
}
