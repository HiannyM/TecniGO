using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class HistorialEstado
{
    public int HistorialId { get; set; }

    public int SolicitudId { get; set; }

    public string Estado { get; set; } = null!;

    public int CambiadoPor { get; set; }

    public DateTime Fecha { get; set; }

    public string? Comentario { get; set; }

    public virtual Usuario CambiadoPorNavigation { get; set; } = null!;

    public virtual Solicitud Solicitud { get; set; } = null!;
}
