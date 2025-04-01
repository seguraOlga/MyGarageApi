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
        public async Task<ActionResult<IEnumerable<Client>>> GetClient(string dni)
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
                    return NotFound(new { Message = "Aquest nom no pertany a cap Client" });
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
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual" });
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
        public async Task<ActionResult<IEnumerable<Client>>> GetCotxesClient(string dni)
        {
            try
            {
                var client = await _context.Clients.FindAsync(dni);
                if (client != null)
                {
                    var cotxes = _context.Cotxes.Where(x => x.Dni == dni).ToList();
                    return Ok(cotxes);
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
                    return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual" });
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
        public async Task<ActionResult<IEnumerable<Client>>> PostClient([FromBody]Client client)
        {
            try
            {

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(client.Contrasenya);
                client.Contrasenya = hashedPassword;
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Client afegit correctament" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        //AFEGIR CLIENT 
        [Route("api/putCliente")]
        [HttpPut]
        public async Task<IActionResult> UpdateClient(string dni, [FromBody] Client updatedClient)
        {
            if (dni != updatedClient.Dni)
            {
                return BadRequest("DNI en la URL y en el cuerpo no coinciden.");
            }


            var existingClient = await _context.Clients.FindAsync(dni);
            if (existingClient == null)
            {
                return NotFound("Cliente no encontrado.");
            }

           
            existingClient.Nom = updatedClient.Nom;
            existingClient.Cognoms = updatedClient.Cognoms;
            existingClient.Email = updatedClient.Email;
            existingClient.Telefon = updatedClient.Telefon;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updatedClient.Contrasenya);
            existingClient.Contrasenya = hashedPassword;
            existingClient.DataNaixement = updatedClient.DataNaixement;

            await _context.SaveChangesAsync();

            return NoContent();
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
