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

        //GET REPARACIONS PER ID
        [Route("api/GetReparacionsId/{id}")]
        [HttpGet]
        public async Task<ActionResult<Reparacio>> GetReparacionsPerId(int id)
        {
            try
            {
                Reparacio reparacio = await _context.Reparacios.Where(x => x.IdReparacio == id).FirstOrDefaultAsync();

                if (reparacio == null)
                {

                    return Ok(new { Message = "No hi han reparacions amb aquesta Matricula" });
                }
                else
                {
                    return Ok(reparacio);
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
                var reparacions =  _context.Reparacios.Where(x => x.Matricula == matricula).ToList();

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
        public async Task<IActionResult> PutReparacio([FromBody] ReparacioSuperficial reparacio)
        {
            try
            {
                Reparacio rep = await _context.Reparacios.Where(x => x.IdReparacio == reparacio.IdReparacio).FirstOrDefaultAsync();
                if (rep != null)
                {

                    rep.IdPressupost = reparacio.IdPressupost;
                    rep.Estat = reparacio.Estat;
                    rep.Factura = reparacio.Factura;
                    rep.Matricula = reparacio.Matricula;
                    rep.CostTreballador = (decimal?)reparacio.CostTreballador;
                    rep.DataEntrada = DateOnly.FromDateTime(reparacio.DataEntrada);
                    rep.DataSortida = reparacio.DataSortida.HasValue
      ? DateOnly.FromDateTime(reparacio.DataSortida.Value)
      : (DateOnly?)null;

                    rep.ObservacionsClient = reparacio.ObservacionsClient;
                    rep.ObservacionsMecanic = reparacio.ObservacionsMecanic;
                    rep.Imatges = reparacio.Imatges;
                    rep.HoresTreballades = (decimal?)reparacio.HoresTreballades;
                    rep.MaterialReparacios = reparacio.MaterialReparacios;
                    _context.Update(rep);
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

        [Route("api/InsertReparacio")]
        [HttpPost]
        public async Task<ActionResult<Reparacio>> PostReparacio([FromBody] ReparacioSuperficial reparacio)
        {
            try
            {
                var cotxe = await _context.Cotxes.Where(x => x.Matricula == reparacio.Matricula).ToListAsync();
                Reparacio rep = await _context.Reparacios.Where(x => x.IdReparacio == reparacio.IdReparacio).FirstOrDefaultAsync();
                if (rep == null && cotxe!= null)
                {

                    Reparacio repara = new Reparacio();
                    repara.Estat = reparacio.Estat;
                    repara.Factura = reparacio.Factura;
                    repara.Matricula = reparacio.Matricula;
                    repara.CostTreballador = (decimal?)reparacio.CostTreballador;
                    repara.DataEntrada = DateOnly.FromDateTime(reparacio.DataEntrada);
                    repara.DataSortida = reparacio.DataSortida.HasValue
      ? DateOnly.FromDateTime(reparacio.DataSortida.Value)
      : (DateOnly?)null;

                    repara.ObservacionsClient = reparacio.ObservacionsClient;
                    repara.ObservacionsMecanic = reparacio.ObservacionsMecanic;
                    repara.Imatges = reparacio.Imatges;
                    repara.HoresTreballades = (decimal?)reparacio.HoresTreballades;
                    repara.MaterialReparacios = reparacio.MaterialReparacios;
                    await _context.AddAsync(repara);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Reparació afegida correctament" });
                }
                else
                {
                    return NotFound(new { Message = "Aquesta reparacio ja existeix." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Error al afegir la reparació" });
            }
        }

        // DELETE REPARACIO
        [Route("api/DeleteReparcacio/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteReparacio(int id)
        {
            try
            {
                var reparacio = await _context.Reparacios.Where(x=>x.IdReparacio == id).FirstOrDefaultAsync();
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
