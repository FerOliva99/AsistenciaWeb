using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

[Table("Docente")]
public partial class Docente
{
    [Key]
    public int id_docente { get; set; }

    [StringLength(120)]
    [Unicode(false)]
    public string nombre { get; set; } = null!;

    [StringLength(120)]
    [Unicode(false)]
    public string apellido { get; set; } = null!;

    [StringLength(120)]
    [Unicode(false)]
    public string? correo { get; set; }

    [InverseProperty("id_docenteNavigation")]
    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
