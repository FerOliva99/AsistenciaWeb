using AsistenciaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Controllers
{
    public class CarreraController : Controller
    {
        private readonly MiDbContext _context;

        public CarreraController(MiDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Carrera
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Carreras.ToListAsync();
            return View(lista);
        }

        [Authorize]
        // GET: Carrera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(m => m.IdCarrera == id);

            if (carrera == null) return NotFound();

            return View(carrera);
        }

        [Authorize]
        // GET: Carrera/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        // POST: Carrera/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrera);
        }

        [Authorize]
        // GET: Carrera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera == null) return NotFound();

            return View(carrera);
        }

        [Authorize]
        // POST: Carrera/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Carrera carrera)
        {
            if (id != carrera.IdCarrera) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreraExists(carrera.IdCarrera))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carrera);
        }

        [Authorize]
        // GET: Carrera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(m => m.IdCarrera == id);

            if (carrera == null) return NotFound();

            return View(carrera);
        }

        [Authorize]
        // POST: Carrera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera != null)
            {
                _context.Carreras.Remove(carrera);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.IdCarrera == id);
        }
    }
}
