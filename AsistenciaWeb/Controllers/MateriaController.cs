using AsistenciaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class MateriaController : Controller
{
    private readonly MiDbContext _context;

    public MateriaController(MiDbContext context)
    {
        _context = context;
    }

    // GET: Materia
    public async Task<IActionResult> Index()
    {
        return View(await _context.Materia.Include(m => m.IdCarreraNavigation).ToListAsync());
    }

    // GET: Materia/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var materia = await _context.Materia.Include(m => m.IdCarreraNavigation).FirstOrDefaultAsync(m => m.id_materia == id);

        if (materia == null) return NotFound();

        return View(materia);
    }

    // GET: Materia/Create
    public IActionResult Create()
    {
        ViewBag.Carreras = new SelectList(_context.Carreras, "IdCarrera", "NombreCarrera");
        return View();
    }

    // POST: Materia/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Materium materium)
    {
        if (ModelState.IsValid)
        {
            _context.Add(materium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Recargar el ViewBag en caso de error
        ViewBag.Carreras = new SelectList(_context.Carreras, "IdCarrera", "NombreCarrera");

        return View(materium);
    }

    // GET: Materia/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var materia = await _context.Materia.Include(m => m.IdCarreraNavigation).FirstOrDefaultAsync(m => m.id_materia == id);
        
        if (materia == null) return NotFound();

        // Para llenar el <select>
        ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "NombreCarrera", materia.IdCarrera);

        return View(materia);
    }

    // POST: Materia/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Materium materium)
    {
        if (id != materium.id_materia) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(materium);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Materia.Any(e => e.id_materia == id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        return View(materium);
    }

    // GET: Materia/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var materia = await _context.Materia.Include(m => m.IdCarreraNavigation).FirstOrDefaultAsync(m => m.id_materia == id);
        if (materia == null) return NotFound();

        return View(materia);
    }

    // POST: Materia/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var materia = await _context.Materia.FindAsync(id);
        if (materia != null)
        {
            _context.Materia.Remove(materia);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
