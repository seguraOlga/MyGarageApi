using System;
using System.Collections.Generic;

namespace MyGarageApi.Models;

public partial class Pressupost
{
    public int IdPressupost { get; set; }

    public DateOnly? DataPressupost { get; set; }

    public decimal? CostTreballador { get; set; }

    public string? MotiuReparacio { get; set; }

    public decimal? PreuTotal { get; set; }

    public string? Estat { get; set; }

    public byte[]? Imatges { get; set; }

    public virtual Reparacio? Reparacio { get; set; }
    public string Matricula { get; internal set; }
}
