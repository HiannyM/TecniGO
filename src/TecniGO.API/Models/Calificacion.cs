using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Calificacion
{
    public int CalificacionId { get; set; }

    public int SolicitudId { get; set; }

    public int ClienteId { get; set; }

    public int TecnicoId { get; set; }

    public byte Puntuacion { get; set; }

    public string? Comentario { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Usuario Cliente { get; set; } = null!;

    public virtual Solicitud Solicitud { get; set; } = null!;

    public virtual Tecnico Tecnico { get; set; } = null!;
}
