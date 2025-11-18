using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

[Table("Estudiante")]
[Index("carnet", Name = "UQ__Estudian__4CDEAA6ED2F1E892", IsUnique = true)]
[Index("codigo_barras", Name = "UQ__Estudian__730FA6AB15B29C61", IsUnique = true)]
public partial class Estudiante
{
    [Key]
    public int id_estudiante { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string codigo_barras { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string carnet { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string nombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string apellido { get; set; } = null!;

    public byte? estado { get; set; }

    public int IdCarrera { get; set; }

    [InverseProperty("id_estudianteNavigation")]
    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    [InverseProperty("id_estudianteNavigation")]
    public virtual ICollection<Estudiante_Grupo> Estudiante_Grupos { get; set; } = new List<Estudiante_Grupo>();

    [ForeignKey("IdCarrera")]
    [InverseProperty("Estudiantes")]
    public virtual Carrera? IdCarreraNavigation { get; set; }
}
