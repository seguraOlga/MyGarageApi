using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGarageApi.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace MyGarageApi.Controllers
{
    //[Route("api/[controller]")]
   // [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public ClientsController(MyGarageDbContext context)
        {
            _context = context;
        }


        //----------------CLIENTS----------------//


        //TODOS LOS CLIENTES
        [Route("api/Clients")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }



        //GET CLIENTE POR DNI
        [Route("api/Client/{dni}")]
        [HttpGet]
        public async Task<ActionResult<Client>> GetClient(string dni)
        {
            try
            {
                var client = await _context.Clients.FindAsync(dni);
                if (client != null)
                {
                    return Ok(client);
                }
                else
                {
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }
        //GET Contrasnya
        [Route("api/verifica")]
        [HttpPost]
        public bool VerifyHash([FromBody] VerificaRequest verify)
        {
            var client = _context.Clients.Where(x => x.Dni == verify.Dni).FirstOrDefault();
            if (client != null)
            {
                if (BCrypt.Net.BCrypt.Verify(verify.TextClar, client.Contrasenya))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //GET CLIENTE POR NOM
        [Route("api/ClientsPerNom/{nom}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientNom(string nom)
        {
            try
            {
                var client = _context.Clients.Where(x=>x.Nom == nom).ToList();
                if (client != null)
                {
                    return Ok(client);
                }
                else
                {
                    return NotFound(new { Message = "Aquest nom no pertany a cap Client" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }

        //GET CLIENTE POR EMAIL
        [Route("api/ClientsPerEmail/{email}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientEmail(string email)
        {
            try
            {
                var client = _context.Clients.Where(x => x.Email == email).ToList();
                if (client != null)
                {
                    return Ok(client);
                }
                else
                {
                    return NotFound(new { Message = "Aquest nom no pertany a cap Client." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }

        //GET CLIENTE POR MARCA
        [Route("api/ClientsPerMarca/{marca}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientMarca(string marca)
        {
            try
            {
                List<Client> clients = new List<Client>(); 
                var cotxes = _context.Cotxes.Where(x => x.Marca == marca).ToList();  ;
                if (cotxes != null)
                {
                    foreach (var c in cotxes)
                    {
                        var client = await _context.Clients.FindAsync(c.Dni);
                        clients.Add(client);
                    }
                    return Ok(clients);

                }
                else
                {
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }

        //GET COTXES PER DNI DEL CLIENT
        [Route("api/CotxesClient/{dni}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotxe>>> GetCotxesClient(string dni)
        {
            try
            {
                var cotxes = _context.Cotxes.Where(x => x.Dni == dni).ToList();
                if (cotxes != null)
                {
                   
                    return Ok(cotxes);
                }
                else
                {
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }

        //GET CLIENTS PER MARCA DE COTXE
      [Route("api/ClientsMarca/{marca}")]
[HttpGet]
public async Task<ActionResult<IEnumerable<Client>>> GetClientsMarca(string marca)
{
    try
    {
        List<Client> cl = new List<Client>();
        var cotxes = _context.Cotxes.Where(x => x.Marca == marca).ToList();

        if (cotxes.Any())
        {
            foreach (var c in cotxes)
            {
                Client client = await _context.Clients.FindAsync(c.Dni);
                if (client != null && !cl.Any(x => x.Dni == client.Dni)) 
                {
                    cl.Add(client);
                }
            }
            return Ok(cl);
        }
        else
        {
            return NotFound(new { Message = "No s'han trobat cotxes amb aquesta marca." });
        }
    }
    catch (Exception e)
    {
        return BadRequest(new { Message = e.Message });
    }
}

        //GET PRESSUPOST PER DNI DEL CLIENT
        [Route("api/Pressupost/{dni}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetPressupostPerClient(string dni)
        {
            try
            {
                List<Pressupost> pressupost = new List<Pressupost>();
                var client = await _context.Clients.FindAsync(dni);
                if (client != null)
                {
                    var cotxes = _context.Cotxes.Where(x => x.Dni == dni).ToList();
                    foreach (var cotxe in cotxes)
                    {
                         pressupost = _context.Pressuposts.Where(x => x.Matricula == cotxe.Matricula).ToList();
                    }
                    return Ok(pressupost);
                }
                else
                {
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }

        //GET REPARACIOS PER DNI DEL CLIENT
        [Route("api/Reparacions/{dni}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetReparacionsClient(string dni)
        {
            try
            {
                List<Reparacio> reparacions = new List<Reparacio>();
                var client = await _context.Clients.FindAsync(dni);
                if (client != null)
                {
                    var cotxes = _context.Cotxes.Where(x => x.Dni == dni).ToList();
                    foreach (var cotxe in cotxes)
                    {
                        reparacions = _context.Reparacios.Where(x => x.Matricula == cotxe.Matricula).ToList();
                    }
                    return Ok(reparacions);
                }
                else
                {
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }


        //GET FACTURAS PER DNI DEL CLIENT
        [Route("api/Facturas/{dni}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetFacturasClient(string dni)
        {
            try
            {
                List<Reparacio> reparacions = new List<Reparacio>();
                List<Factura> facturas = new List<Factura>();
                var client = await _context.Clients.FindAsync(dni);
                if (client != null)
                {
                    var cotxes = _context.Cotxes.Where(x => x.Dni == dni).ToList();
                    foreach (var cotxe in cotxes)
                    {
                        reparacions = _context.Reparacios.Where(x => x.Matricula == cotxe.Matricula).ToList();
                        foreach (var reparacio in reparacions)
                        {
                            facturas = _context.Facturas.Where(x => x.IdReparacio == reparacio.IdReparacio).ToList();
                        }
                    }
                    return Ok(facturas);
                }
                else
                {
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }





        //AFEGIR CLIENT 
        [Route("api/postCliente")]
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient([FromBody]ClienteSuperficial client)
        {
            try
            {
                if (client != null)
                {
                   

                    if ( await _context.Clients.FindAsync(client.Dni)== null)
                    {

                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(client.Contrasenya);
                        client.Contrasenya = hashedPassword;
                        DateTime fecha = DateTime.Now;
                        DateOnly soloFecha = DateOnly.FromDateTime(client.DataNaixement);

                        Client c = new Client()
                        {
                            Nom = client.Nom,
                            Cognoms = client.Cognoms,
                            Email = client.Email,
                            Dni = client.Dni,
                            Telefon = client.Telefon,
                            Contrasenya = client.Contrasenya,
                            DataNaixement = soloFecha,
                            Notificacios = new List<Notificacio>(),
                            Cotxes = new List<Cotxe>()
                        };
                        await _context.Clients.AddAsync(c);
                        await _context.SaveChangesAsync();
                        return Ok(client);
                    }
                    else
                    {
                        return NotFound(new { Message = "Aquest client ja existeix" });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "El client rebut es null " });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        ////AFEGIR CLIENT 
        [Route("api/putCliente")]
        [HttpPut]
        public async Task<ActionResult<Client>> UpdateClient([FromBody]ClienteSuperficial updatedClient)
        {
            try
            {
                    Client client = await _context.Clients.FindAsync(updatedClient.Dni);

                if (client != null)
                {
                    DateTime fecha = DateTime.Now;
                    DateOnly soloFecha = DateOnly.FromDateTime(updatedClient.DataNaixement);

                if (client.Contrasenya == updatedClient.Contrasenya)
                {




                        client.Nom = updatedClient.Nom;
                        client.Cognoms = updatedClient.Cognoms;
                        client.Email = updatedClient.Email;
                        client.Dni = updatedClient.Dni;
                        client.Telefon = updatedClient.Telefon;
                        client.Contrasenya = updatedClient.Contrasenya;
                        client.DataNaixement = soloFecha;
                        client.Notificacios = client.Notificacios;
                        client.Cotxes = client.Cotxes;  


                        _context.Clients.Update(client);
                        await _context.SaveChangesAsync();
                        return Ok(new { Message = "Client actualitzat correctament" });
                    }
                    else
                    {
                      
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updatedClient.Contrasenya);
                        updatedClient.Contrasenya = hashedPassword;

                        client.Nom = updatedClient.Nom;
                        client.Cognoms = updatedClient.Cognoms;
                        client.Email = updatedClient.Email;
                        client.Dni = updatedClient.Dni;
                        client.Telefon = updatedClient.Telefon;
                        client.Contrasenya = hashedPassword;
                        client.DataNaixement = soloFecha;
                        client.Notificacios = client.Notificacios;
                        client.Cotxes = client.Cotxes;


                        _context.Clients.UpdateRange(client);
                        await _context.SaveChangesAsync();
                        return Ok(new { Message = "Client actualitzat correctament" });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "El client rebut es null " });
                    
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }




        //DELETE CLIENTE POR DNI
        [Route("api/deleteClient/{dni}")]
        [HttpDelete]
        public async Task<ActionResult<IEnumerable<Client>>> DeleteClient(string dni)
        {
            try
            {
                var client = await _context.Clients.FindAsync(dni);
                if (client != null)
                {
                    _context.Clients.Remove(client);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Client eliminat correctament" });
                }
                else
                {
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        //VALIDATE LOGIN

        public bool ValidateLogin(string dni, string password)
        {
            var client = _context.Clients.Where(x => x.Dni == dni).FirstOrDefault();
            if (client != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, client.Contrasenya))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



    }
}
