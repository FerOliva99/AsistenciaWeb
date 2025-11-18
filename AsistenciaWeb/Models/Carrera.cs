using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

public partial class Carrera
{
    [Key]
    public int IdCarrera { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string NombreCarrera { get; set; } = null!;

    public int? DuracionAnios { get; set; }

    public byte? estado { get; set; }

    [InverseProperty("IdCarreraNavigation")]
    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    [InverseProperty("IdCarreraNavigation")]
    public virtual ICollection<Materium> Materia { get; set; } = new List<Materium>();
}
