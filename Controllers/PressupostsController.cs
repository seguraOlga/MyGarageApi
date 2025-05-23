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
    public class PressupostsController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public PressupostsController(MyGarageDbContext context)
        {
            _context = context;
        }

        // GET ALL PRESSUPOSTS
        [Route("api/GetPressuposts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pressupost>>> GetPressuposts()
        {
            return await _context.Pressuposts.ToListAsync();
        }
        // GET  PRESSUPOSTS PER DATA
        [Route("api/GetPressupostsPerData")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pressupost>>> GetPressupostsPerData()
        {
            try
            {
                var pressuposts = await _context.Pressuposts.OrderBy(x => x.DataPressupost).ToListAsync();
                return Ok(pressuposts);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Error" + e });
            }
        }

        // GET PER  ESTAT
        [Route("api/GetPressuposts/{estat}")]
        [HttpGet]
        public async Task<ActionResult<Pressupost>> GetPressupost(string estat)
        {
            try
            {
                var pressupost = _context.Pressuposts.Where(x => x.Estat == estat).ToList();

                if (pressupost == null)
                {
                    return NotFound(new { Message = "No s'han trobat pressuposts amb aquest estat " });
                }
                else
                {
                    return Ok(pressupost);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });

            }
        }

        // GET PER  ESTAT
        [Route("api/GetPressupostsMatricula/{matricula}")]
        [HttpGet]
        public async Task<ActionResult<Pressupost>> GetPressupostPerMatricula(string matricula)
        {
            try
            {
                var pressupost = _context.Pressuposts.Where(x => x.Matricula == matricula).ToList();

                if (pressupost == null)
                {
                    return NotFound(new { Message = "No s'han trobat pressuposts amb aquesta Matricula " });
                }
                else
                {
                    return Ok(pressupost);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });

            }
        }

        // PUT PRESUPOSST
        [Route("api/PutPressuposts")]
        [HttpPut]
        public async Task<IActionResult> PutPressupost([FromBody] Pressupost pressupost)
        {
            try
            {
                var pressupostDB = _context.Pressuposts.Find(pressupost.IdPressupost);
                if (pressupostDB != null)
                {
                    _context.Update(pressupost);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Pressupost actualitzrt correctament" });
                }
                else
                {
                    return NotFound(new { Message = "Pressupost no trobat" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }


        }

        // POST PRESSUPOST
        [Route("api/PostPressuposts")]
        [HttpPost]
        public async Task<ActionResult<Pressupost>> PostPressupost([FromBody] PressupostSuperficial pressupost)
        {
            try
            {

                var pressupostDB = await _context.Pressuposts.Where(x=>x.IdPressupost == pressupost.IdPressupost).FirstOrDefaultAsync();


                if (pressupostDB == null)
                {
                    Pressupost presu = new Pressupost();
                    presu.PreuTotal = (decimal?)pressupost.PreuTotal;
                    presu.DataPressupost = DateOnly.FromDateTime(DateTime.Now);
                    presu.MotiuReparacio = pressupost.MotiuReparacio;
                    presu.Matricula = pressupost.Matricula;
                    presu.CostTreballador = (decimal?)pressupost.CostTreballador;
                    presu.Estat = pressupost.Estat;
                    presu.Imatges = pressupost.Imatges;
                    await _context.Pressuposts.AddAsync(presu);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Pressupost creat correctament" });
                }
                else
                {
                    return BadRequest(new {Message="Hi ha hagut algún problema amb el pressupost." });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        // DELETE PRESSUPOST
        [Route("api/DeletePressupost/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePressupost(int id)
        {
            try
            {
                var pressupost = await _context.Pressuposts.FindAsync(id);
                if (pressupost == null)
                {
                    return NotFound(new { Message = "El pressupost no existeix" });
                }
                else
                {

                    _context.Pressuposts.Remove(pressupost);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Pressupost eliminat correctament" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

        }


    }
}
