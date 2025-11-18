using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaWeb.Models;

public partial class MiDbContext : DbContext
{
    public MiDbContext()
    {
    }

    public MiDbContext(DbContextOptions<MiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Estudiante_Grupo> Estudiante_Grupos { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-QEROJPO;Database=AsistenciaUNAB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.id_asistencia).HasName("PK__Asistenc__D0454A9AA02806ED");

            entity.Property(e => e.estado).HasDefaultValue("Presente");
            entity.Property(e => e.fecha).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.hora).HasDefaultValueSql("(CONVERT([time],getdate()))");

            entity.HasOne(d => d.id_estudianteNavigation).WithMany(p => p.Asistencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asistenci__id_es__38996AB5");

            entity.HasOne(d => d.id_grupoNavigation).WithMany(p => p.Asistencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asistenci__id_gr__398D8EEE");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.IdCarrera).HasName("PK__Carreras__884A8F1F1025FA7F");

            entity.Property(e => e.estado).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.id_docente).HasName("PK__Docente__300DB211A6830D1D");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.id_estudiante).HasName("PK__Estudian__E0B2763C8947BB43");

            entity.Property(e => e.estado).HasDefaultValue((byte)1);

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Estudiantes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estudiante_Carrera");
        });

        modelBuilder.Entity<Estudiante_Grupo>(entity =>
        {
            entity.HasKey(e => e.id_estudiante_grupo).HasName("PK__Estudian__88D38805F519C5DB");

            entity.HasOne(d => d.id_estudianteNavigation).WithMany(p => p.Estudiante_Grupos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__id_es__31EC6D26");

            entity.HasOne(d => d.id_grupoNavigation).WithMany(p => p.Estudiante_Grupos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__id_gr__32E0915F");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.id_grupo).HasName("PK__Grupo__8B68D68893A22E61");

            entity.HasOne(d => d.id_docenteNavigation).WithMany(p => p.Grupos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grupo__id_docent__2F10007B");

            entity.HasOne(d => d.id_materiaNavigation).WithMany(p => p.Grupos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grupo__id_materi__2E1BDC42");
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.HasKey(e => e.id_materia).HasName("PK__Materia__7E03FD39E4660FA1");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Materia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Materia_Carrera");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
