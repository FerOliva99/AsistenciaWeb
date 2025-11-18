using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

public partial class MiDbContext : DbContext
{

    public MiDbContext(DbContextOptions<MiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<EstudianteGrupo> EstudianteGrupos { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK__Asistenc__D0454A9AA02806ED");

            entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Presente")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora)
                .HasDefaultValueSql("(CONVERT([time],getdate()))")
                .HasColumnName("hora");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asistenci__id_es__38996AB5");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asistenci__id_gr__398D8EEE");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.IdDocente).HasName("PK__Docente__300DB211A6830D1D");

            entity.ToTable("Docente");

            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__E0B2763C8947BB43");

            entity.ToTable("Estudiante");

            entity.HasIndex(e => e.Carnet, "UQ__Estudian__4CDEAA6ED2F1E892").IsUnique();

            entity.HasIndex(e => e.CodigoBarras, "UQ__Estudian__730FA6AB15B29C61").IsUnique();

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Carnet)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("carnet");
            entity.Property(e => e.Carrera)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("carrera");
            entity.Property(e => e.CodigoBarras)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codigo_barras");
            entity.Property(e => e.Estado)
                .HasDefaultValue((byte)1)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<EstudianteGrupo>(entity =>
        {
            entity.HasKey(e => e.IdEstudianteGrupo).HasName("PK__Estudian__88D38805F519C5DB");

            entity.ToTable("Estudiante_Grupo");

            entity.Property(e => e.IdEstudianteGrupo).HasColumnName("id_estudiante_grupo");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.EstudianteGrupos)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__id_es__31EC6D26");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.EstudianteGrupos)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__id_gr__32E0915F");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PK__Grupo__8B68D68893A22E61");

            entity.ToTable("Grupo");

            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.Aula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aula");
            entity.Property(e => e.Horario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("horario");
            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdMateria).HasColumnName("id_materia");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grupo__id_docent__2F10007B");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdMateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grupo__id_materi__2E1BDC42");
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__Materia__7E03FD39E4660FA1");

            entity.HasIndex(e => e.Codigo, "UQ__Materia__40F9A2065140A593").IsUnique();

            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.Carrera)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("carrera");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
