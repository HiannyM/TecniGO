using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Solicitud
{
    public int SolicitudId { get; set; }

    public int ClienteId { get; set; }

    public int DireccionId { get; set; }

    public int ServicioId { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaProgramada { get; set; }

    public bool Urgente { get; set; }

    public string Estado { get; set; } = null!;

    public string? MotivoCancelacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaCierre { get; set; }

    public virtual ICollection<Asignacion> Asignacions { get; set; } = new List<Asignacion>();

    public virtual Calificacion? Calificacion { get; set; }

    public virtual Usuario Cliente { get; set; } = null!;

    public virtual ICollection<Cotizacion> Cotizacions { get; set; } = new List<Cotizacion>();

    public virtual Direccion Direccion { get; set; } = null!;

    public virtual ICollection<Disputum> Disputa { get; set; } = new List<Disputum>();

    public virtual ICollection<HistorialEstado> HistorialEstados { get; set; } = new List<HistorialEstado>();

    public virtual Servicio Servicio { get; set; } = null!;
}
