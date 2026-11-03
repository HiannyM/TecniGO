using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Categorium
{
    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

    public virtual ICollection<Tecnico> Tecnicos { get; set; } = new List<Tecnico>();
}
