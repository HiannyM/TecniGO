using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Disponibilidad
{
    public int DisponibilidadId { get; set; }

    public int TecnicoId { get; set; }

    public byte DiaSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public bool Activo { get; set; }

    public virtual Tecnico Tecnico { get; set; } = null!;
}
