namespace MyGarageApi.Models
{
    public class NotificacioSuperficial
    {
        public int Id { get; set; }

        public string DniClient { get; set; }

        public string Titol { get; set; }

        public string Missatge { get; set; }

        public DateTime DataEnvio { get; set; }

        public virtual Client DniClientNavigation { get; set; }
    }
}
