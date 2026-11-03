using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Auditorium
{
    public long AuditoriaId { get; set; }

    public int? UsuarioId { get; set; }

    public string Entidad { get; set; } = null!;

    public int EntidadId { get; set; }

    public string Accion { get; set; } = null!;

    public string? Detalle { get; set; }

    public string? DireccionIp { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
