using AsistenciaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly MiDbContext _context;

        public EstudiantesController(MiDbContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var listaEstudiantes = await _context.Estudiantes.ToListAsync();
            return View(listaEstudiantes);
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.id_estudiante == id);

            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewBag.Carreras = new SelectList(_context.Carreras, "IdCarrera", "NombreCarrera");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("codigo_barras,carnet,nombre,apellido,IdCarrera,estado")] Estudiante estudiante)
        {
            Console.WriteLine("VALOR DE estudiante.codigo_barras [" + estudiante.codigo_barras + "]");
            Console.WriteLine("VALOR DE estudiante.carnet [" + estudiante.carnet + "]");
            Console.WriteLine("VALOR DE estudiante.nombre [" + estudiante.nombre + "]");
            Console.WriteLine("VALOR DE estudiante.apellido [" + estudiante.apellido + "]");
            Console.WriteLine("VALOR DE estudiante.IdCarrera [" + estudiante.IdCarrera + "]");
            Console.WriteLine("VALOR DE estudiante.estado [" + estudiante.estado + "]");
            Console.WriteLine("VALOR DE estudiante.IdCarreraNavigation [" + estudiante.IdCarreraNavigation + "]");

            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("VALOR DE estudiante.codigo_barras [" + estudiante.codigo_barras + "]");
                Console.WriteLine("VALOR DE estudiante.carnet [" + estudiante.carnet + "]");
                Console.WriteLine("VALOR DE estudiante.nombre [" + estudiante.nombre + "]");
                Console.WriteLine("VALOR DE estudiante.apellido [" + estudiante.apellido + "]");
                Console.WriteLine("VALOR DE estudiante.IdCarrera [" + estudiante.IdCarrera + "]");
                Console.WriteLine("VALOR DE estudiante.estado [" + estudiante.estado + "]");
                Console.WriteLine("VALOR DE estudiante.IdCarreraNavigation [" + estudiante.IdCarreraNavigation + "]");

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("ERROR: " + error.ErrorMessage);
                }
            }

            // Recargar el ViewBag en caso de error
            ViewBag.Carreras = new SelectList(_context.Carreras, "IdCarrera", "NombreCarrera", estudiante.IdCarrera);

            return View(estudiante);
        }


        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Console.WriteLine("Valor de [" + id + "]");
            if (id == null) return NotFound();

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_estudiante,codigo_barras,carnet,nombre,apellido,IdCarrera,estado")] Estudiante estudiante)
        {
            if (id != estudiante.id_estudiante) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.id_estudiante)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.id_estudiante == id);
            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.id_estudiante == id);
        }
    }
}
