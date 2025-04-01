using System;
using System.Collections.Generic;

namespace MyGarageApi.Models;

public partial class Notificacio
{
    public int Id { get; set; }

    public string? DniClient { get; set; }

    public string? Titol { get; set; }

    public string? Missatge { get; set; }

    public DateTime? DataEnvio { get; set; }

    public virtual Client? DniClientNavigation { get; set; }
}
