using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TecniGO.API.Models;

namespace TecniGO.API.Data;

public partial class TecniGoDbContext : DbContext
{
    public TecniGoDbContext()
    {
    }

    public TecniGoDbContext(DbContextOptions<TecniGoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignacion> Asignacions { get; set; }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Calificacion> Calificacions { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cotizacion> Cotizacions { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Disponibilidad> Disponibilidads { get; set; }

    public virtual DbSet<Disputum> Disputa { get; set; }

    public virtual DbSet<DocumentoTecnico> DocumentoTecnicos { get; set; }

    public virtual DbSet<HistorialEstado> HistorialEstados { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<RecuperacionPassword> RecuperacionPasswords { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Solicitud> Solicituds { get; set; }

    public virtual DbSet<Tecnico> Tecnicos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignacion>(entity =>
        {
            entity.HasKey(e => e.AsignacionId).HasName("PK__Asignaci__D82B5B57F848DBC3");

            entity.ToTable("Asignacion");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Ofertada", "DF_Asignacion_Estado");
            entity.Property(e => e.FechaOferta).HasDefaultValueSql("(sysutcdatetime())", "DF_Asignacion_FechaOferta");
            entity.Property(e => e.MotivoRechazo).HasMaxLength(250);
            entity.Property(e => e.Puntaje).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.Solicitud).WithMany(p => p.Asignacions)
                .HasForeignKey(d => d.SolicitudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asignacion_Solicitud");

            entity.HasOne(d => d.Tecnico).WithMany(p => p.Asignacions)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asignacion_Tecnico");
        });

        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.AuditoriaId).HasName("PK__Auditori__095694C3A4A0D123");

            entity.Property(e => e.Accion).HasMaxLength(20);
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(45)
                .HasColumnName("DireccionIP");
            entity.Property(e => e.Entidad).HasMaxLength(50);
            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysutcdatetime())", "DF_Auditoria_Fecha");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Auditoria_Usuario");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.CalificacionId).HasName("PK__Califica__4CF54ADEC4C8B851");

            entity.ToTable("Calificacion");

            entity.HasIndex(e => e.SolicitudId, "UQ__Califica__85E95DC62F95ECEC").IsUnique();

            entity.Property(e => e.Comentario).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysutcdatetime())", "DF_Calificacion_Fecha");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calificacion_Cliente");

            entity.HasOne(d => d.Solicitud).WithOne(p => p.Calificacion)
                .HasForeignKey<Calificacion>(d => d.SolicitudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calificacion_Solicitud");

            entity.HasOne(d => d.Tecnico).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calificacion_Tecnico");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E54CF7E85D");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF704EB81E").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Categoria_Activo");
            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(80);
        });

        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasKey(e => e.CotizacionId).HasName("PK__Cotizaci__30443A7905E181D4");

            entity.ToTable("Cotizacion");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Pendiente", "DF_Cotizacion_Estado");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())", "DF_Cotizacion_FechaCrea");
            entity.Property(e => e.ManoObra).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Materiales).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total)
                .HasComputedColumnSql("([ManoObra]+[Materiales])", true)
                .HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.Solicitud).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.SolicitudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cotizacion_Solicitud");

            entity.HasOne(d => d.Tecnico).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cotizacion_Tecnico");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.DireccionId).HasName("PK__Direccio__68906D64D087E6D8");

            entity.ToTable("Direccion");

            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.Calle).HasMaxLength(150);
            entity.Property(e => e.Numero).HasMaxLength(20);
            entity.Property(e => e.Referencia).HasMaxLength(250);
            entity.Property(e => e.Sector).HasMaxLength(100);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion_Usuario");

            entity.HasOne(d => d.Zona).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.ZonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion_Zona");
        });

        modelBuilder.Entity<Disponibilidad>(entity =>
        {
            entity.HasKey(e => e.DisponibilidadId).HasName("PK__Disponib__0300F3F4D9799629");

            entity.ToTable("Disponibilidad");

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Disponibilidad_Activo");

            entity.HasOne(d => d.Tecnico).WithMany(p => p.Disponibilidads)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disponibilidad_Tecnico");
        });

        modelBuilder.Entity<Disputum>(entity =>
        {
            entity.HasKey(e => e.DisputaId).HasName("PK__Disputa__B43B973EA2972ABE");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Abierta", "DF_Disputa_Estado");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())", "DF_Disputa_FechaCrea");
            entity.Property(e => e.Motivo).HasMaxLength(500);
            entity.Property(e => e.Resolucion).HasMaxLength(500);

            entity.HasOne(d => d.ResueltaPorNavigation).WithMany(p => p.DisputumResueltaPorNavigations)
                .HasForeignKey(d => d.ResueltaPor)
                .HasConstraintName("FK_Disputa_ResueltaPor");

            entity.HasOne(d => d.Solicitud).WithMany(p => p.Disputa)
                .HasForeignKey(d => d.SolicitudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disputa_Solicitud");

            entity.HasOne(d => d.Usuario).WithMany(p => p.DisputumUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disputa_Usuario");
        });

        modelBuilder.Entity<DocumentoTecnico>(entity =>
        {
            entity.HasKey(e => e.DocumentoId).HasName("PK__Document__5DDBFC76F11E1E4B");

            entity.ToTable("DocumentoTecnico");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Pendiente", "DF_DocTecnico_Estado");
            entity.Property(e => e.FechaCarga).HasDefaultValueSql("(sysutcdatetime())", "DF_DocTecnico_FechaCarga");
            entity.Property(e => e.NombreArchivo).HasMaxLength(200);
            entity.Property(e => e.RutaArchivo).HasMaxLength(300);
            entity.Property(e => e.Tipo).HasMaxLength(30);

            entity.HasOne(d => d.Tecnico).WithMany(p => p.DocumentoTecnicos)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocTecnico_Tecnico");
        });

        modelBuilder.Entity<HistorialEstado>(entity =>
        {
            entity.HasKey(e => e.HistorialId).HasName("PK__Historia__9752068F2CF3BBC9");

            entity.ToTable("HistorialEstado");

            entity.Property(e => e.Comentario).HasMaxLength(250);
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysutcdatetime())", "DF_Historial_Fecha");

            entity.HasOne(d => d.CambiadoPorNavigation).WithMany(p => p.HistorialEstados)
                .HasForeignKey(d => d.CambiadoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Usuario");

            entity.HasOne(d => d.Solicitud).WithMany(p => p.HistorialEstados)
                .HasForeignKey(d => d.SolicitudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Solicitud");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pago__F00B6138250F1E20");

            entity.ToTable("Pago");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Pendiente", "DF_Pago_Estado");
            entity.Property(e => e.FechaPago).HasDefaultValueSql("(sysutcdatetime())", "DF_Pago_FechaPago");
            entity.Property(e => e.Metodo).HasMaxLength(20);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Referencia).HasMaxLength(100);

            entity.HasOne(d => d.Cotizacion).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.CotizacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Cotizacion");

            entity.HasOne(d => d.RegistradoPorNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.RegistradoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_RegistradoPor");
        });

        modelBuilder.Entity<RecuperacionPassword>(entity =>
        {
            entity.HasKey(e => e.RecuperacionId).HasName("PK__Recupera__32BA9F637066CAEA");

            entity.ToTable("RecuperacionPassword");

            entity.Property(e => e.TokenHash).HasMaxLength(255);

            entity.HasOne(d => d.Usuario).WithMany(p => p.RecuperacionPasswords)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recuperacion_Usuario");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302F1A5AFF9AD");

            entity.ToTable("Rol");

            entity.HasIndex(e => e.Nombre, "UQ__Rol__75E3EFCFBFA5FAF4").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(30);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioId).HasName("PK__Servicio__D5AEECC28285672A");

            entity.ToTable("Servicio");

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Servicio_Activo");
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrecioVisita).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TarifaBase).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servicio_Categoria");
        });

        modelBuilder.Entity<Solicitud>(entity =>
        {
            entity.HasKey(e => e.SolicitudId).HasName("PK__Solicitu__85E95DC7B8BC3534");

            entity.ToTable("Solicitud");

            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Pendiente", "DF_Solicitud_Estado");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())", "DF_Solicitud_FechaCrea");
            entity.Property(e => e.MotivoCancelacion).HasMaxLength(250);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitud_Cliente");

            entity.HasOne(d => d.Direccion).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.DireccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitud_Direccion");

            entity.HasOne(d => d.Servicio).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitud_Servicio");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.TecnicoId).HasName("PK__Tecnico__C866A05DFD4AAED0");

            entity.ToTable("Tecnico");

            entity.HasIndex(e => e.Cedula, "UQ__Tecnico__B4ADFE38CA5F89F1").IsUnique();

            entity.Property(e => e.TecnicoId).ValueGeneratedNever();
            entity.Property(e => e.Biografia).HasMaxLength(500);
            entity.Property(e => e.CalificacionPromedio).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Cedula).HasMaxLength(15);
            entity.Property(e => e.Disponible).HasDefaultValue(true, "DF_Tecnico_Disponible");
            entity.Property(e => e.EstadoValidacion)
                .HasMaxLength(20)
                .HasDefaultValue("Pendiente", "DF_Tecnico_EstadoVal");
            entity.Property(e => e.MotivoRechazo).HasMaxLength(250);

            entity.HasOne(d => d.TecnicoNavigation).WithOne(p => p.TecnicoTecnicoNavigation)
                .HasForeignKey<Tecnico>(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tecnico_Usuario");

            entity.HasOne(d => d.ValidadoPorNavigation).WithMany(p => p.TecnicoValidadoPorNavigations)
                .HasForeignKey(d => d.ValidadoPor)
                .HasConstraintName("FK_Tecnico_ValidadoPor");

            entity.HasMany(d => d.Categoria).WithMany(p => p.Tecnicos)
                .UsingEntity<Dictionary<string, object>>(
                    "TecnicoCategorium",
                    r => r.HasOne<Categorium>().WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TecCat_Categoria"),
                    l => l.HasOne<Tecnico>().WithMany()
                        .HasForeignKey("TecnicoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TecCat_Tecnico"),
                    j =>
                    {
                        j.HasKey("TecnicoId", "CategoriaId").HasName("PK__TecnicoC__87539C4328D65515");
                        j.ToTable("TecnicoCategoria");
                    });

            entity.HasMany(d => d.Zonas).WithMany(p => p.Tecnicos)
                .UsingEntity<Dictionary<string, object>>(
                    "TecnicoZona",
                    r => r.HasOne<Zona>().WithMany()
                        .HasForeignKey("ZonaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TecZona_Zona"),
                    l => l.HasOne<Tecnico>().WithMany()
                        .HasForeignKey("TecnicoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TecZona_Tecnico"),
                    j =>
                    {
                        j.HasKey("TecnicoId", "ZonaId").HasName("PK__TecnicoZ__B997400AF1213115");
                        j.ToTable("TecnicoZona");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8539067FD");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534C7509F38").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Usuario_Activo");
            entity.Property(e => e.Apellido).HasMaxLength(80);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(sysutcdatetime())", "DF_Usuario_FechaReg");
            entity.Property(e => e.Nombre).HasMaxLength(80);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.ZonaId).HasName("PK__Zona__1F1E0576EC13D9D9");

            entity.ToTable("Zona");

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Zona_Activo");
            entity.Property(e => e.Municipio).HasMaxLength(80);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Provincia).HasMaxLength(80);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
