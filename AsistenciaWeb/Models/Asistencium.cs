using System;
using System.Collections.Generic;

namespace AsistenciaWeb.Models;

public partial class Asistencium
{
    public int IdAsistencia { get; set; }

    public int IdEstudiante { get; set; }

    public int IdGrupo { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public string? Estado { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
