using System;
using System.Collections.Generic;

namespace AsistenciaWeb.Models;

public partial class EstudianteGrupo
{
    public int IdEstudianteGrupo { get; set; }

    public int IdEstudiante { get; set; }

    public int IdGrupo { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
