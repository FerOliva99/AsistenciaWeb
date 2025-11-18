using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

[Table("EstudianteGrupo")]
public partial class EstudianteGrupo
{
    [Key]
    public int id_estudiante_grupo { get; set; }

    public int id_estudiante { get; set; }

    public int id_grupo { get; set; }

    [ForeignKey("id_estudiante")]
    [InverseProperty("EstudianteGrupos")]
    public virtual Estudiante id_estudianteNavigation { get; set; } = null!;

    [ForeignKey("id_grupo")]
    [InverseProperty("EstudianteGrupos")]
    public virtual Grupo id_grupoNavigation { get; set; } = null!;
}
