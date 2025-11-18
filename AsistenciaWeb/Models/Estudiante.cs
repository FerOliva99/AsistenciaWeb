using System;
using System.Collections.Generic;

namespace AsistenciaWeb.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string CodigoBarras { get; set; } = null!;

    public string Carnet { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Carrera { get; set; } = null!;

    public byte? Estado { get; set; }

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<EstudianteGrupo> EstudianteGrupos { get; set; } = new List<EstudianteGrupo>();
}
