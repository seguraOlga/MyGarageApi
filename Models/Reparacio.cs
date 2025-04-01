using System;
using System.Collections.Generic;

namespace MyGarageApi.Models;

public partial class Reparacio
{
    public int IdReparacio { get; set; }

    public int? IdPressupost { get; set; }

    public string? Matricula { get; set; }

    public decimal? CostTreballador { get; set; }

    public decimal? HoresTreballades { get; set; }

    public string? Estat { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataSortida { get; set; }

    public string? ObservacionsClient { get; set; }

    public string? ObservacionsMecanic { get; set; }

    public byte[]? Imatges { get; set; }

    public virtual Factura? Factura { get; set; }

    public virtual Pressupost? IdPressupostNavigation { get; set; }

    public virtual ICollection<MaterialReparacio> MaterialReparacios { get; set; } = new List<MaterialReparacio>();

    public virtual Cotxe? MatriculaNavigation { get; set; }
}
