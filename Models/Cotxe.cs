using System;
using System.Collections.Generic;

namespace MyGarageApi.Models;

public partial class Cotxe
{
    public string Matricula { get; set; } = null!;

    public string? Dni { get; set; }

    public string? Marca { get; set; }

    public string? Model { get; set; }

    public int? DataFabricacio { get; set; }

    public string? Carburant { get; set; }

    public string? Cilindrada { get; set; }

    public string? Bastidor { get; set; }

    public string? Imatge { get; set; }

    public virtual Client? DniNavigation { get; set; }

    public virtual ICollection<Reparacio> Reparacios { get; set; } = new List<Reparacio>();
}
