using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Telefono { get; set; }

    public bool Activo { get; set; }

    public int IntentosFallidos { get; set; }

    public DateTime? BloqueadoHasta { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual ICollection<Calificacion> Calificacions { get; set; } = new List<Calificacion>();

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual ICollection<Disputum> DisputumResueltaPorNavigations { get; set; } = new List<Disputum>();

    public virtual ICollection<Disputum> DisputumUsuarios { get; set; } = new List<Disputum>();

    public virtual ICollection<HistorialEstado> HistorialEstados { get; set; } = new List<HistorialEstado>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<RecuperacionPassword> RecuperacionPasswords { get; set; } = new List<RecuperacionPassword>();

    public virtual Rol Rol { get; set; } = null!;

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();

    public virtual Tecnico? TecnicoTecnicoNavigation { get; set; }

    public virtual ICollection<Tecnico> TecnicoValidadoPorNavigations { get; set; } = new List<Tecnico>();
}
