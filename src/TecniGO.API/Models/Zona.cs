using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Zona
{
    public int ZonaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Municipio { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual ICollection<Tecnico> Tecnicos { get; set; } = new List<Tecnico>();
}
