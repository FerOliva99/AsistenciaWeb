using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [StringLength(256)]
    [Unicode(false)]
    public string? contrasena { get; set; }

    [InverseProperty("id_docenteNavigation")]
    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public void SetPassword(string password)
    {
        var hasher = new PasswordHasher<Docente>();
        this.contrasena = hasher.HashPassword(this, password);
    }

    public bool CheckPassword(string password)
    {
        var hasher = new PasswordHasher<Docente>();
        var result = hasher.VerifyHashedPassword(this, this.contrasena, password);
        return result == PasswordVerificationResult.Success;
    }
}