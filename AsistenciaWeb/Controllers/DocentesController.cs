using AsistenciaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AsistenciaWeb.Controllers
{
    public class DocentesController : Controller
    {
        private readonly MiDbContext _context;

        public DocentesController(MiDbContext context)
        {
            _context = context;
        }

        // GET: Docentes
        public async Task<IActionResult> Index()
        {
            var docentes = await _context.Docentes.ToListAsync();
            return View(docentes);
        }

        // GET: Docentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(m => m.id_docente == id);

            if (docente == null)
                return NotFound();

            return View(docente);
        }

        // GET: Docentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Docentes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Docente docente, string contrasena)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(contrasena))
                {
                    docente.SetPassword(contrasena); // Hash de la contraseña
                }

                _context.Add(docente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docente);
        }

        // GET: Docentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
                return NotFound();

            return View(docente);
        }

        // POST: Docentes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Docente docente, string? nuevaContrasena)
        {
            if (id != docente.id_docente)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    /*
                    _context.Update(docente);
                    await _context.SaveChangesAsync();
                    */

                    var docenteDb = await _context.Docentes.FindAsync(id);
                    if (docenteDb == null) return NotFound();

                    // Actualizar solo los campos visibles
                    docenteDb.nombre = docente.nombre;
                    docenteDb.apellido = docente.apellido;
                    docenteDb.correo = docente.correo;

                    // Solo actualizar contraseña si se envía nueva
                    if (!string.IsNullOrEmpty(nuevaContrasena))
                    {
                        docenteDb.SetPassword(nuevaContrasena);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Docentes.Any(e => e.id_docente == id))
                        return NotFound();

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(docente);
        }

        // GET: Docentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(m => m.id_docente == id);

            if (docente == null)
                return NotFound();

            return View(docente);
        }

        // POST: Docentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);

            if (docente != null)
            {
                _context.Docentes.Remove(docente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> MisGrupos()
        {
            var docenteIdClaim = User.Claims.FirstOrDefault(c => c.Type == "DocenteId")?.Value;
            if (docenteIdClaim == null)
                return RedirectToAction("Login", "Account");

            int docenteId = int.Parse(docenteIdClaim);

            var grupos = await _context.Grupos
                .Where(g => g.id_docente == docenteId)
                .ToListAsync();

            return View(grupos);
        }
    }
}
