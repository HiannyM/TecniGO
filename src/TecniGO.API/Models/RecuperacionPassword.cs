using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class RecuperacionPassword
{
    public int RecuperacionId { get; set; }

    public int UsuarioId { get; set; }

    public string TokenHash { get; set; } = null!;

    public DateTime FechaExpiracion { get; set; }

    public bool Usado { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
