namespace MyGarageApi.Models
{
    public class ClienteSuperficial
    {
        public string Dni { get; set; } = null;

        public string Nom { get; set; }

        public string Cognoms { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        public string Contrasenya { get; set; }

        public DateTime DataNaixement { get; set; }

        public virtual ICollection<Cotxe> Cotxes { get; set; } = new List<Cotxe>();

        public virtual ICollection<Notificacio> Notificacios { get; set; } = new List<Notificacio>();
    }

}
