using AsistenciaWeb.Models;

namespace AsistenciaWeb.ViewModels
{
    public class DetalleAsistenciaVM
    {
        public Grupo Grupo { get; set; }
        public DateOnly? Fecha { get; set; }
        public List<Asistencium>? Asistencias { get; set; }
    }
}
