using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Disputum
{
    public int DisputaId { get; set; }

    public int SolicitudId { get; set; }

    public int UsuarioId { get; set; }

    public string Motivo { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Resolucion { get; set; }

    public int? ResueltaPor { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaResolucion { get; set; }

    public virtual Usuario? ResueltaPorNavigation { get; set; }

    public virtual Solicitud Solicitud { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
