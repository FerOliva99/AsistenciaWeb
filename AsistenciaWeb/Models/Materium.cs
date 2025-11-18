using System;
using System.Collections.Generic;

namespace AsistenciaWeb.Models;

public partial class Materium
{
    public int IdMateria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string Carrera { get; set; } = null!;

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
