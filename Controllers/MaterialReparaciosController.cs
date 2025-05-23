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
    public class MaterialReparaciosController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public MaterialReparaciosController(MyGarageDbContext context)
        {
            _context = context;
        }

        // GET: api/MaterialReparacios
        [Route("api/MaterialUtilitzat")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialReparacio>>> GetMaterialReparacios()
        {
            return await _context.MaterialReparacios.ToListAsync();
        }

        // GET: api/MaterialReparacios/5
        [Route("api/MaterialUtilitzat/{id}")]
        [HttpGet]
        public async Task<ActionResult<MaterialReparacio>> GetMaterialReparacio(int id)
        {
            var materialReparacio = await _context.MaterialReparacios.FindAsync(id);

            if (materialReparacio == null)
            {
                return NotFound();
            }

            return materialReparacio;
        }

        //GET /api/MaterialsPerReparacio/{idReparacio}
        [Route("api/MaterialsPerReparacio/{idReparacio}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialReparacio>>> GetMaterialsPerReparacio(int idReparacio)
        {
            var materials = await _context.MaterialReparacios
                .Where(m => m.IdReparacio == idReparacio)
                .Include(m => m.RefPecaNavigation)
                .ToListAsync();

            if (materials == null || !materials.Any())
            {
                return NotFound();
            }

            return Ok(materials);
        }


        //PUT
        [Route("api/PutMaterial")]
        [HttpPut]
        public async Task<IActionResult> PutMaterialReparacio([FromBody] MaterialReparacio materialReparacio)
        {
            try
            {
                var material = _context.MaterialReparacios.Where(x => x.IdMaterial == materialReparacio.IdMaterial).FirstOrDefault();
                if (material != null)
                {
                    _context.MaterialReparacios.Update(materialReparacio);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Material actualitzat correctament" });
                }
                else
                {
                    return NotFound(new { Message = "Material no trobat" });
                }
            }
            catch (Exception e)
            {

                return BadRequest(new { Message = e.Message });
            }
        }

        //POST
        [Route("api/PostMaterial")]
        [HttpPost]
        public async Task<ActionResult<MaterialReparacio>> PostMaterialReparacio([FromBody] MaterialReparacio materialReparacio)
        {
            try
            {
                var material = _context.MaterialReparacios.Where(x => x.IdMaterial == materialReparacio.IdMaterial).FirstOrDefault();
                if (material == null)
                {
                    await _context.MaterialReparacios.AddAsync(materialReparacio);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Material Insertat correctament" });
                }
                else
                {
                    return BadRequest(new { Message = "Material exitent" });
                }
            }
            catch (Exception e)
            {

                return BadRequest(new { Message = e.Message });
            }
        }

        // DELETE
        [Route("api/deleteMaterial")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMaterialReparacio(int id)
        {
            var materialReparacio = await _context.MaterialReparacios.FindAsync(id);
            if (materialReparacio == null)
            {
                return NotFound();
            }

            _context.MaterialReparacios.Remove(materialReparacio);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
