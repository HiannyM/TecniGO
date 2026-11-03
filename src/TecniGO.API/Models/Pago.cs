using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int CotizacionId { get; set; }

    public decimal Monto { get; set; }

    public string Metodo { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Referencia { get; set; }

    public int RegistradoPor { get; set; }

    public DateTime FechaPago { get; set; }

    public virtual Cotizacion Cotizacion { get; set; } = null!;

    public virtual Usuario RegistradoPorNavigation { get; set; } = null!;
}
