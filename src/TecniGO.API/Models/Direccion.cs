using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Direccion
{
    public int DireccionId { get; set; }

    public int UsuarioId { get; set; }

    public int ZonaId { get; set; }

    public string Alias { get; set; } = null!;

    public string Calle { get; set; } = null!;

    public string? Numero { get; set; }

    public string Sector { get; set; } = null!;

    public string? Referencia { get; set; }

    public bool Principal { get; set; }

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Zona Zona { get; set; } = null!;
}
