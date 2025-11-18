using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

[Index("codigo", Name = "UQ__Materia__40F9A2065140A593", IsUnique = true)]
public partial class Materium
{
    [Key]
    public int id_materia { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string nombre { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string codigo { get; set; } = null!;

    public int IdCarrera { get; set; }

    [InverseProperty("id_materiaNavigation")]
    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    [ForeignKey("IdCarrera")]
    [InverseProperty("Materia")]
    public virtual Carrera? IdCarreraNavigation { get; set; }
}
