using System;
using System.Collections.Generic;

namespace MyGarageApi.Models;

public partial class Producte
{
    public int RefPeca { get; set; }

    public string? Descripcio { get; set; }

    public string? PartVehicle { get; set; }

    public decimal? PreuVenta { get; set; }

    public decimal? PreuCompra { get; set; }

    public string? MarcaCotxe { get; set; }

    public int? Quantitat { get; set; }

    public string? Imatge { get; set; }

    public virtual ICollection<MaterialReparacio> MaterialReparacios { get; set; } = new List<MaterialReparacio>();
}
