using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Cotizacion
{
    public int CotizacionId { get; set; }

    public int SolicitudId { get; set; }

    public int TecnicoId { get; set; }

    public decimal ManoObra { get; set; }

    public decimal Materiales { get; set; }

    public decimal? Total { get; set; }

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Solicitud Solicitud { get; set; } = null!;

    public virtual Tecnico Tecnico { get; set; } = null!;
}
