using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyGarageApi.Models;

public partial class Reparacio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdReparacio { get; set; }

    public int? IdPressupost { get; set; }

    [Required]
    public string? Matricula { get; set; }

    public decimal? CostTreballador { get; set; }

    public decimal? HoresTreballades { get; set; }

    public string? Estat { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataSortida { get; set; }

    public string? ObservacionsClient { get; set; }

    public string? ObservacionsMecanic { get; set; }

    public string? Imatges { get; set; }

    public virtual Factura? Factura { get; set; }

    public virtual Pressupost? IdPressupostNavigation { get; set; }

    public virtual ICollection<MaterialReparacio> MaterialReparacios { get; set; } = new List<MaterialReparacio>();

    public virtual Cotxe? MatriculaNavigation { get; set; }
}
