using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class DocumentoTecnico
{
    public int DocumentoId { get; set; }

    public int TecnicoId { get; set; }

    public string Tipo { get; set; } = null!;

    public string NombreArchivo { get; set; } = null!;

    public string RutaArchivo { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime FechaCarga { get; set; }

    public virtual Tecnico Tecnico { get; set; } = null!;
}
