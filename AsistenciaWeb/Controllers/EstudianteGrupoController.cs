using AsistenciaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Controllers
{
    public class EstudianteGrupoController : Controller
    {
        private readonly MiDbContext _context;

        public EstudianteGrupoController(MiDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? grupo, string? materia, string? codigo, string? docente, string? estudiante)
        {
            // Verificar si hay filtros activos
            bool hayFiltros =
                !string.IsNullOrWhiteSpace(grupo) ||
                !string.IsNullOrWhiteSpace(materia) ||
                !string.IsNullOrWhiteSpace(codigo) ||
                !string.IsNullOrWhiteSpace(docente) ||
                !string.IsNullOrWhiteSpace(estudiante);

            if (!hayFiltros)
            {
                // Si no mandan filtros no se debe consultar la base
                return View(new List<Estudiante_Grupo>());
            }

            var datos = _context.Estudiante_Grupos
                .Include(e => e.id_estudianteNavigation)
                .Include(g => g.id_grupoNavigation)
                    .ThenInclude(m => m.id_materiaNavigation)
                .Include(g => g.id_grupoNavigation)
                    .ThenInclude(d => d.id_docenteNavigation)
                .AsQueryable();

            // FILTROS BACKEND
            if (!string.IsNullOrEmpty(grupo))
                datos = datos.Where(x => x.id_grupoNavigation.id_grupo == Int32.Parse(grupo));

            if (!string.IsNullOrEmpty(materia))
                datos = datos.Where(x => x.id_grupoNavigation.id_materiaNavigation.nombre.Contains(materia));

            if (!string.IsNullOrEmpty(codigo))
                datos = datos.Where(x => x.id_grupoNavigation.id_materiaNavigation.codigo.Contains(codigo));

            if (!string.IsNullOrEmpty(docente))
                datos = datos.Where(x =>
                    (x.id_grupoNavigation.id_docenteNavigation.nombre + " " +
                     x.id_grupoNavigation.id_docenteNavigation.apellido).Contains(docente));

            if (!string.IsNullOrEmpty(estudiante))
                datos = datos.Where(x =>
                    (x.id_estudianteNavigation.nombre + " " +
                     x.id_estudianteNavigation.apellido).Contains(estudiante));

            // Enviar valores de filtros a la vista
            ViewData["grupo"] = grupo;
            ViewData["materia"] = materia;
            ViewData["codigo"] = codigo;
            ViewData["docente"] = docente;
            ViewData["estudiante"] = estudiante;

            return View(await datos.ToListAsync());
        }

        // GET: EstudianteGrupo/Create
        public IActionResult Create()
        {
            // Cargar listas para dropdowns si quieres elegir Estudiante y Grupo
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Grupos"] = _context.Grupos.ToList();
            return View();
        }

        // POST: EstudianteGrupo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_estudiante,id_grupo")] Estudiante_Grupo estudianteGrupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudianteGrupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay errores, recargar listas
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Grupos"] = _context.Grupos.ToList();
            return View(estudianteGrupo);
        }

        public async Task<IActionResult> Details(int id)
        {
            var registro = await _context.Estudiante_Grupos
                .Include(e => e.id_estudianteNavigation)
                .Include(g => g.id_grupoNavigation)
                .ThenInclude(m => m.id_materiaNavigation)
                .Include(g => g.id_grupoNavigation)
                    .ThenInclude(d => d.id_docenteNavigation)
                .FirstOrDefaultAsync(x => x.id_estudiante_grupo == id);

            if (registro == null) return NotFound();

            return View(registro);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var registro = await _context.Estudiante_Grupos
                .Include(e => e.id_estudianteNavigation)
                .Include(g => g.id_grupoNavigation)
                .ThenInclude(m => m.id_materiaNavigation)
                .Include(g => g.id_grupoNavigation)
                    .ThenInclude(d => d.id_docenteNavigation)
                .FirstOrDefaultAsync(x => x.id_estudiante_grupo == id);

            if (registro == null) return NotFound();

            return View(registro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registro = await _context.Estudiante_Grupos.FindAsync(id);
            if (registro != null)
            {
                _context.Remove(registro);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // -------------------------------
        // EDIT - GET
        // -------------------------------
        public async Task<IActionResult> Edit(int id)
        {
            var registro = await _context.Estudiante_Grupos
                .Include(e => e.id_estudianteNavigation)
                .Include(g => g.id_grupoNavigation)
                .ThenInclude(m => m.id_materiaNavigation)
                .Include(g => g.id_grupoNavigation)
                    .ThenInclude(d => d.id_docenteNavigation)
                .FirstOrDefaultAsync(x => x.id_estudiante_grupo == id);

            if (registro == null) return NotFound();

            ViewBag.Estudiantes = new SelectList(_context.Estudiantes, "id_estudiante", "id_estudiante");
            ViewBag.Grupos = new SelectList(_context.Grupos, "id_grupo", "id_grupo");
            
            /*
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Grupos"] = _context.Grupos.ToList();
            */

            return View(registro);
        }

        // -------------------------------
        // EDIT - POST
        // -------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Estudiante_Grupo data)
        {
            if (id != data.id_estudiante_grupo) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Estudiantes = new SelectList(_context.Estudiantes, "id_estudiante", "nombre");
            ViewBag.Grupos = new SelectList(_context.Grupos, "id_grupo", "id_grupo");
            
            /*
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Grupos"] = _context.Grupos.ToList();
            */

            return View(data);
        }
    }
}
