using Microsoft.AspNetCore.Mvc.Rendering;

namespace AsistenciaWeb.ViewModels
{
    public class EstudianteGrupoEditVM
    {
        public int IdEstudianteGrupo { get; set; }
        public int IdEstudiante { get; set; }
        public int IdGrupo { get; set; }

        public List<SelectListItem>? Estudiantes { get; set; }
        public List<SelectListItem>? Grupos { get; set; }
    }
}
