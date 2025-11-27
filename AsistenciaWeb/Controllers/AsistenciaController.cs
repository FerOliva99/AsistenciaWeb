using AsistenciaWeb.Models;
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
}
