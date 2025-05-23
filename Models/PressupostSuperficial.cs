using Microsoft.VisualBasic;

namespace MyGarageApi.Models
{
    public class PressupostSuperficial
    {
        public int IdPressupost { get; set; }

        public DateFormat? DataPressupost { get; set; }

        public Double? CostTreballador { get; set; }

        public string? MotiuReparacio { get; set; }

        public Double? PreuTotal { get; set; }

        public string? Estat { get; set; }

        public string? Imatges { get; set; }

        public virtual Reparacio? Reparacio { get; set; }
        public string Matricula { get;  set; }
    }
}
