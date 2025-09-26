using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcial.Data;
using PrimerParcial.Models;
using System.Linq;

namespace PrimerParcial.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly RecetasDBContext _context;

        public CategoriesController(RecetasDBContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
        {
            if (!ModelState.IsValid)
            {
                // Dump errores en consola y alerta en vista
                var errores = string.Join(" | ", ModelState
                    .Where(kv => kv.Value.Errors.Count > 0)
                    .Select(kv => $"{kv.Key}: {string.Join(",", kv.Value.Errors.Select(e => e.ErrorMessage))}"));

                Console.WriteLine("❌ MODELSTATE ERRORS => " + errores);
                TempData["err"] = "No se pudo guardar: " + errores;

                return View(category);
            }

            try
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["ok"] = "✅ Categoría registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR SAVE => " + ex.Message);
                TempData["err"] = "Error al guardar en la BD.";
                return View(category);
            }
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id) return NotFound();

            if (!ModelState.IsValid) return View(category);

            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                TempData["ok"] = "✏️ Categoría actualizada.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["ok"] = "🗑️ Categoría eliminada.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
