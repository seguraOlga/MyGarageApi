using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGarageApi.Models;

namespace MyGarageApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CotxesController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public CotxesController(MyGarageDbContext context)
        {
            _context = context;
        }

        //GET TODOS LOS COCHES
        [Route("api/Cotxes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotxe>>> GetCotxes()
        {
            return await _context.Cotxes.ToListAsync();
        }
        //GET COTXE BY MATRICULA
        [Route("api/CotxesMatricula/{matricula}")]

        [HttpGet]
        public async Task<ActionResult<Cotxe>> GetCotxeMatricula(string id)
        {
            var cotxe = await _context.Cotxes.FindAsync(id);

            if (cotxe == null)
            {
                return NotFound();
            }

            return cotxe;
        }

    

        //COCHES POR ESTADO 
        [Route("api/Cotxes/Estats")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List<Cotxe>>>> GetCotxesEnReparacio([FromBody] string estat)
        {
            List<Cotxe> cotxesEnReparacio = new List<Cotxe>();
            try
            {
                var matriculasEnReparacion = _context.Reparacios.Where(x => x.Estat == estat).Select(x => x.Matricula).ToList();

                foreach (var mat in matriculasEnReparacion)
                {
                    var cotxe = await _context.Cotxes.FindAsync(mat);
                    cotxesEnReparacio.Add(cotxe);

                }

                return Ok(cotxesEnReparacio);


            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }



        //DELETE COCHE POR MATRICULA
        [Route("api/deleteCotxe/{matricula}")]
        [HttpDelete]
        public async Task<ActionResult<IEnumerable<Cotxe>>> DeleteCotxe(string matricula)
        {
            try
            {
                var cotxe = await _context.Cotxes.FindAsync(matricula);
                if (cotxe != null)
                {
                    _context.Cotxes.Remove(cotxe);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Cotxe eliminat correctament" });
                }
                else
                {
                    return NotFound(new { Message = "La matricula introduïda no pertany a cap registre actual" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
        // POST COTXE
        [Route("api/InsertarCotxe")]
        [HttpPost]
        public async Task<IActionResult> PostCotxe([FromBody] Cotxe cotxe)
        {
            try
            {
                var cotxes = _context.Cotxes.Where(x => x.Matricula == cotxe.Matricula).ToList();
                if (cotxes == null)
                {
                   _context.Cotxes.Add(cotxe);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound(new { Message = "Aquesta matricula ja existeix, un cotxe no pot tenir dos prpietaris " });
                }

                    return NoContent();
            }catch(Exception e)
            {
                return BadRequest(new { Message = e.Message }); 
            }
        }

        [Route("api/CotxeActualizar")]
        [HttpPut]
        public async Task<IActionResult> PutCotxe([FromBody] Cotxe cotxe)
        {
            try
            {
                var cotxes = _context.Cotxes.Where(x => x.Matricula == cotxe.Matricula).ToList();
                if (cotxes != null)
                {
                    _context.Cotxes.Add(cotxe);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound(new { Message = "No s'ha trobat cap cotxe amb aquesta matrícula" });
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }








    }
}
