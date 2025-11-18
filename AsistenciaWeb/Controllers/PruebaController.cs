using AsistenciaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Controllers
{
    public class PruebaController : Controller
    {

        private readonly MiDbContext _context;

        public PruebaController(MiDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var estudiante = new Estudiante
            {
                nombre = "Juan",
                apellido = "Perez",
                carnet = "20250001",
                codigo_barras = "123456",
                IdCarrera = 1  // <- Debe existir en la tabla Carrera
            };

            _context.Estudiantes.Add(estudiante);
            _context.SaveChanges();

            var materia = new Materium
            {
                nombre = "Matemática",
                codigo = "MAT101",
                IdCarrera = 1
            };

            _context.Materia.Add(materia);
            _context.SaveChanges();

            return View();
        }
    }
}
