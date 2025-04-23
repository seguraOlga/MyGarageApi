using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using MyGarageApi.Models;

namespace MyGarageApi.Controllers
{
    public class NotificaciosController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public NotificaciosController(MyGarageDbContext context)
        {
            _context = context;
        }

        // GET: api/Notificacios
        [Route("api/Notificacions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacio>>> GetNotificacios()
        {
            return await _context.Notificacios.ToListAsync();
        }

        // GET NOTIFICACIONS PER  USUARI
        [Route("api/NotificacionsClient/{dni}")]
        [HttpGet]
        public async Task<ActionResult<Notificacio>> GetNotificacio(string dni)
        {
            try
            {
                List<Notificacio> notificacions =  _context.Notificacios.Where(x => x.DniClient == dni).OrderBy(x=>x.DataEnvio).ToList();

                if (notificacions == null)
                {
                    return NotFound(new { Message =" Notificacions no trobades"});
                }
                else
                {
                    return Ok(notificacions);
                }
            }catch(Exception e){
                return BadRequest(new { Message = e.Message });
            }

        }

 
        // AFEGIR NOTIFICACIÓ
        [Route("api/NotificacionsClient")]
        [HttpPost]
        public async Task<ActionResult<Notificacio>> PostNotificacio([FromBody] Notificacio notificacio)
        {
            try
            {
                
                _context.Notificacios.Add(notificacio);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Notificació afegida correctament" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Error al afegir la notificació" });
            }
        }

        // DELETE: ELIMINAR NOTIFICACIÓ PER ID
        [Route("api/NotificacioDelete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNotificacio(int id)
        {
            Notificacio notificacio = await _context.Notificacios.FindAsync(id);
            if (notificacio == null)
            {
                return NotFound(new { Message="Aquesta Notificació no existeix"});
            }
            else
            {
                _context.Notificacios.Remove(notificacio);
                await _context.SaveChangesAsync();

                return Ok(new { Message="Notificació eliminada correctament"});
            }
        }

       
    }
}
