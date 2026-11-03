using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Asignacion
{
    public int AsignacionId { get; set; }

    public int SolicitudId { get; set; }

    public int TecnicoId { get; set; }

    public decimal Puntaje { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaOferta { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public string? MotivoRechazo { get; set; }

    public virtual Solicitud Solicitud { get; set; } = null!;

    public virtual Tecnico Tecnico { get; set; } = null!;
}
