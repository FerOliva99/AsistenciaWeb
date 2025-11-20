using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

[Table("Estudiante_Grupo")]
public partial class Estudiante_Grupo
{
    [Key]
    public int id_estudiante_grupo { get; set; }

    public int id_estudiante { get; set; }

    public int id_grupo { get; set; }

    [ForeignKey("id_estudiante")]
    [InverseProperty("Estudiante_Grupos")]
    public virtual Estudiante? id_estudianteNavigation { get; set; }

    [ForeignKey("id_grupo")]
    [InverseProperty("Estudiante_Grupos")]
    public virtual Grupo? id_grupoNavigation { get; set; }
}
