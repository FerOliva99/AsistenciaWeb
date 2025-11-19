using AsistenciaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Controllers
{
    public class GrupoController : Controller
    {
        private readonly MiDbContext _context;

        public GrupoController(MiDbContext context)
        {
            _context = context;
        }

        // GET: Grupo
        public async Task<IActionResult> Index()
        {
            var grupos = await _context.Grupos
                .Include(g => g.id_docenteNavigation)
                .Include(g => g.id_materiaNavigation)
                .ToListAsync();

            return View(grupos);
        }

        // GET: Grupo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var grupo = await _context.Grupos
                .Include(g => g.id_docenteNavigation)
                .Include(g => g.id_materiaNavigation)
                .FirstOrDefaultAsync(m => m.id_grupo == id);

            if (grupo == null)
                return NotFound();

            return View(grupo);
        }

        // GET: Grupo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var grupo = await _context.Grupos
                .Include(g => g.id_docenteNavigation)
                .Include(g => g.id_materiaNavigation)
                .FirstOrDefaultAsync(m => m.id_grupo == id);

            if (grupo == null)
                return NotFound();

            return View(grupo);
        }

        // POST: Grupo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo != null)
            {
                _context.Grupos.Remove(grupo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Grupo/Create
        public IActionResult Create()
        {
            ViewBag.Docentes = new SelectList(_context.Docentes, "id_docente", "nombre");
            ViewBag.Materias = new SelectList(_context.Materia, "id_materia", "nombre");

            return View();
        }

        // POST: Grupo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recargar el ViewBag en caso de error
            ViewBag.Docentes = new SelectList(_context.Carreras, "id_docente", "nombre", grupo.id_docente);
            ViewBag.Materias = new SelectList(_context.Carreras, "id_materia", "nombre", grupo.id_materia);

            return View(grupo);
        }

        // GET: Grupo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
                return NotFound();

            ViewBag.Docentes = new SelectList(_context.Docentes, "id_docente", "nombre", grupo.id_docente);
            ViewBag.Materias = new SelectList(_context.Materia, "id_materia", "nombre", grupo.id_materia);

            return View(grupo);
        }

        // POST: Grupo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Grupo grupo)
        {
            if (id != grupo.id_grupo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.id_grupo))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(grupo);
        }

        private bool GrupoExists(int id)
        {
            return _context.Grupos.Any(e => e.id_grupo == id);
        }
    }
}
