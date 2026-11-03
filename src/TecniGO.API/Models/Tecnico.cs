using System;
using System.Collections.Generic;

namespace TecniGO.API.Models;

public partial class Tecnico
{
    public int TecnicoId { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Biografia { get; set; }

    public string EstadoValidacion { get; set; } = null!;

    public int? ValidadoPor { get; set; }

    public DateTime? FechaValidacion { get; set; }

    public string? MotivoRechazo { get; set; }

    public bool Disponible { get; set; }

    public decimal CalificacionPromedio { get; set; }

    public int TotalServicios { get; set; }

    public virtual ICollection<Asignacion> Asignacions { get; set; } = new List<Asignacion>();

    public virtual ICollection<Calificacion> Calificacions { get; set; } = new List<Calificacion>();

    public virtual ICollection<Cotizacion> Cotizacions { get; set; } = new List<Cotizacion>();

    public virtual ICollection<Disponibilidad> Disponibilidads { get; set; } = new List<Disponibilidad>();

    public virtual ICollection<DocumentoTecnico> DocumentoTecnicos { get; set; } = new List<DocumentoTecnico>();

    public virtual Usuario TecnicoNavigation { get; set; } = null!;

    public virtual Usuario? ValidadoPorNavigation { get; set; }

    public virtual ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();

    public virtual ICollection<Zona> Zonas { get; set; } = new List<Zona>();
}
