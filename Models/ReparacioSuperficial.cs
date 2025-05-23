namespace MyGarageApi.Models
{
    public class ReparacioSuperficial
    {
        public int IdReparacio { get; set; }

        public int? IdPressupost { get; set; }

        public string Matricula { get; set; }

        public Double? CostTreballador { get; set; }

        public Double? HoresTreballades { get; set; }

        public string Estat { get; set; }

        public DateTime DataEntrada { get; set; }

        public DateTime? DataSortida { get; set; }

        public string ObservacionsClient { get; set; }
        public string ObservacionsMecanic { get; set; }
        public string Imatges { get; set; }

        public Factura Factura { get; set; }
        public Pressupost IdPressupostNavigation { get; set; }

        public ICollection<MaterialReparacio> MaterialReparacios { get; set; } = new List<MaterialReparacio>();

        public Cotxe MatriculaNavigation { get; set; }
    }
}

