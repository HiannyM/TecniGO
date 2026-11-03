using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Servicio
{
    public int ServicioId { get; set; }

    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal TarifaBase { get; set; }

    public decimal PrecioVisita { get; set; }

    public bool Activo { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();
}
