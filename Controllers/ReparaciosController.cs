using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGarageApi.Models;

namespace MyGarageApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ReparaciosController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public ReparaciosController(MyGarageDbContext context)
        {
            _context = context;
        }

        // GET: api/Reparacios
        [Route("api/GetReparacionsPerData")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reparacio>>> GetReparacios()
        {
            return await _context.Reparacios.ToListAsync();
        }

        //GET REPARACIONS PER ESTAT
        [Route("api/GetReparacionsPerEstat/{estat}")]
        [HttpGet]
        public async Task<ActionResult<Reparacio>> GetReparacioEstat(string estat)
        {
            try
            {
                var reparacions = _context.Reparacios.Where(x => x.Estat == estat).ToList();

                if (reparacions == null)
                {

                    return Ok(new { Message = "No hi han reparacions amb aquest estat" });
                }
                else
                {
                    return Ok(reparacions);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });


            }
        }

        //GET REPARACIONS PER MATRICULA
        [Route("api/GetReparacionsPerMatricula/{matricula}")]
        [HttpGet]
        public async Task<ActionResult<Reparacio>> GetReparacionsMatricula(string matricula)
        {
            try
            {
                var reparacions = _context.Reparacios.Where(x => x.Matricula == matricula).ToList();

                if (reparacions == null)
                {

                    return Ok(new { Message = "No hi han reparacions amb aquesta Matricula" });
                }
                else
                {
                    return Ok(reparacions);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });


            }
        }




        //GET REPARACIONS PER MATRICULA
        [Route("api/Updatereparacio")]
        [HttpPut]
        public async Task<IActionResult> PutReparacio([FromBody] Reparacio reparacio)
        {
            try
            {
                var rep = _context.Reparacios.Where(x => x.IdReparacio == reparacio.IdReparacio).FirstOrDefaultAsync();
                if (rep != null)
                {
                    _context.Update(reparacio);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Reparació actualitzada correctament" });
                }
                else
                {
                    return NotFound(new { Message = "Reparació no trobada" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Error al actualitzar la reparació" });
            }
        }

        // POST REPARACIO
        [Route("api/InsertReparacio")]
        [HttpPost]
        public async Task<ActionResult<Reparacio>> PostReparacio([FromBody] Reparacio reparacio)
        {
            try
            {
                var rep = await _context.Reparacios.FindAsync(reparacio.IdReparacio);
                if (rep == null)
                {
                    _context.Reparacios.Add(reparacio);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Reparació afegida correctament" });
                }
                else
                {
                    return NotFound(new { Message = "Reparació existent" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Error al actualitzar la reparació" });
            }
        }

        // DELETE REPARACIO
        [Route("api/DeleteReparcacio/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteReparacio(int id)
        {
            try
            {
                var reparacio = await _context.Reparacios.FindAsync(id);
                if (reparacio == null)
                {
                    return NotFound(new { Message = "Aquesta reparació no existeix" });
                }
                else
                {
                    _context.Reparacios.Remove(reparacio);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Reparació eliminada correctament" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });

            }
        }


    }
}
