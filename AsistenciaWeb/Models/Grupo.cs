using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

[Table("Grupo")]
public partial class Grupo
{
    [Key]
    public int id_grupo { get; set; }

    public int id_materia { get; set; }

    public int id_docente { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? horario { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? aula { get; set; }

    [InverseProperty("id_grupoNavigation")]
    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    [InverseProperty("id_grupoNavigation")]
    public virtual ICollection<Estudiante_Grupo> Estudiante_Grupos { get; set; } = new List<Estudiante_Grupo>();

    [ForeignKey("id_docente")]
    [InverseProperty("Grupos")]
    public virtual Docente? id_docenteNavigation { get; set; }

    [ForeignKey("id_materia")]
    [InverseProperty("Grupos")]
    public virtual Materium? id_materiaNavigation { get; set; }
}
