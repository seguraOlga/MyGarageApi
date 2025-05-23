namespace MyGarageApi.Models
{
    public class FacturaSupercifial
    {
        public int IdFactura { get; set; }

        public int? IdReparacio { get; set; }

        public DateTime Data { get; set; }

        public decimal? PreuTotal { get; set; }

        public virtual Reparacio IdReparacioNavigation { get; set; }
    }
}
