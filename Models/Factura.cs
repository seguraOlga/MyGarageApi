using System;
using System.Collections.Generic;

namespace MyGarageApi.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int? IdReparacio { get; set; }

    public DateOnly? Data { get; set; }

    public decimal? PreuTotal { get; set; }

    public virtual Reparacio? IdReparacioNavigation { get; set; }

    public string? RutaPdf { get; set; }

}
