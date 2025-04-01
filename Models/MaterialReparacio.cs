using System;
using System.Collections.Generic;

namespace MyGarageApi.Models;

public partial class MaterialReparacio
{
    public int IdMaterial { get; set; }

    public int? IdReparacio { get; set; }

    public int? RefPeca { get; set; }

    public int? Quantitat { get; set; }

    public virtual Reparacio? IdReparacioNavigation { get; set; }

    public virtual Producte? RefPecaNavigation { get; set; }
}
