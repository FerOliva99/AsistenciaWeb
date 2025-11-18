using System;
using System.Collections.Generic;

namespace AsistenciaWeb.Models;

public partial class Grupo
{
    public int IdGrupo { get; set; }

    public int IdMateria { get; set; }

    public int IdDocente { get; set; }

    public string? Horario { get; set; }

    public string? Aula { get; set; }

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<EstudianteGrupo> EstudianteGrupos { get; set; } = new List<EstudianteGrupo>();

    public virtual Docente IdDocenteNavigation { get; set; } = null!;

    public virtual Materium IdMateriaNavigation { get; set; } = null!;
}
