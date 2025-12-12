using AsistenciaWeb.Models;
using AsistenciaWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AsistenciaController : Controller
{
    private readonly MiDbContext _context;

    public AsistenciaController(MiDbContext context)
    {
        _context = context;
    }

    // GET: Asistencia/Registrar/5
    [Authorize]
    public IActionResult Registrar(int id)
    {
        // Aquí puedes obtener el grupo y/o estudiantes para enviar a la vista
        var grupo = _context.Grupos
            .Include(g => g.id_docenteNavigation)
            .Include(g => g.id_materiaNavigation)
            .FirstOrDefault(g => g.id_grupo == id);

        if (grupo == null)
        {
            return NotFound();
        }

        return View(grupo); // enviar el grupo a la vista
    }

    [Authorize]
    [HttpGet]
    public IActionResult BuscarEstudiante(string carnet, int idGrupo)
    {
        var estudiante = _context.Estudiante_Grupos
            .Include(eg => eg.id_estudianteNavigation)
            .Where(eg => eg.id_grupo == idGrupo && eg.id_estudianteNavigation.carnet == carnet)
            .Select(eg => eg.id_estudianteNavigation)
            .FirstOrDefault();

        if (estudiante == null)
        {
            return Json(new { ok = false, mensaje = "El estudiante no pertenece a este grupo o el carnet no existe." });
        }

        return Json(new
        {
            ok = true,
            estudiante = new
            {
                id = estudiante.id_estudiante,
                carnet = estudiante.carnet,
                nombre = estudiante.nombre,
                apellido = estudiante.apellido
            }
        });
    }

    [Authorize]
    [HttpPost]
    public IActionResult RegistrarAsistenciaIndividual(int idEstudiante, int idGrupo)
    {
        Console.WriteLine("ID de estudiante [" + idEstudiante + "]");
        Console.WriteLine("ID de grupo [" + idGrupo + "]");
        var fecha = DateOnly.FromDateTime(DateTime.Now);
        var hora = TimeOnly.FromDateTime(DateTime.Now);

        // Validar que pertenece al grupo
        var pertenece = _context.Estudiante_Grupos
            .Any(eg => eg.id_estudiante == idEstudiante && eg.id_grupo == idGrupo);

        if (!pertenece)
        {
            return Json(new { ok = false, mensaje = "Este estudiante no pertenece al grupo." });
        }

        // Registrar asistencia
        var asistencia = new Asistencium
        {
            id_estudiante = idEstudiante,
            id_grupo = idGrupo,
            fecha = fecha,
            hora = hora,
            estado = "PRESENTE"
        };

        _context.Asistencia.Add(asistencia);
        _context.SaveChanges();

        return Json(new { ok = true, mensaje = "Asistencia registrada correctamente" });
    }

    [Authorize]
    public IActionResult Detalle(int id, DateOnly? fecha)
    {
        var grupo = _context.Grupos
            .Include(g => g.id_materiaNavigation)
            .Include(g => g.id_docenteNavigation)
            .FirstOrDefault(g => g.id_grupo == id);

        if (grupo == null)
        {
            return NotFound();
        }

        var vm = new DetalleAsistenciaVM
        {
            Grupo = grupo,
            Fecha = fecha
        };

        if (fecha != null)
        {
            vm.Asistencias = _context.Asistencia
                .Include(a => a.id_estudianteNavigation)
                .Where(a => a.id_grupo == id && a.fecha == fecha)
                .ToList();
        }

        return View(vm);
    }
}
