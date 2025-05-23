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
    public class FacturasController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public FacturasController(MyGarageDbContext context)
        {
            _context = context;
        }

        // GET ALL FACTURAS
        [Route("api/GetFacturas")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }


        // GET ALL FACTURAS
        [Route("api/GetFacturasperData")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturasPerData()
        {
            var facturas = await _context.Facturas.OrderBy(x => x.Data).ToListAsync();
            return Ok(facturas);
        }

        // GET FACTURAS PER ID
        [Route("api/GetFacturaID/{id}")]
        [HttpGet]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }

        // PUT FACTURA
        [Route("api/UpdateFactura")]
        [HttpPut]
        public async Task<IActionResult> PutFactura([FromBody] Factura factura)
        {
            try
            {
                var facturaDB = await _context.Facturas.FindAsync(factura.IdFactura);
                if (factura == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Facturas.Update(factura);
                    await _context.SaveChangesAsync();
                    return Ok(factura);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        // POST FACTURA
        [Route("api/InsertFactura")]
        [HttpPost]
        public async Task<IActionResult> PostFactura([FromBody] FacturaSupercifial facturas)
        {
            try
            {
                var facturaDB = await _context.Facturas.Where(x=>x.IdFactura == facturas.IdFactura).FirstOrDefaultAsync();
                if (facturaDB != null)
                {
                    return NotFound(new { Message = "La factura introduïda ja existeix" });
                }
                else
                {
                    Factura factura = new Factura
                    {
                        IdReparacio = facturas.IdReparacio,
                        Data = DateOnly.FromDateTime(facturas.Data), 
                        PreuTotal = facturas.PreuTotal

                    };
                    await _context.Facturas.AddAsync(factura);
                    await _context.SaveChangesAsync();
                    return Ok(factura);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }


        // DELETE FCATURAS
        [Route("api/DeleteFactura/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                var factura = await _context.Facturas.FindAsync(id);
                if (factura == null)
                {
                    return NotFound(new { Message = "No existeix aquesta factura" });
                }
                else
                {

                    _context.Facturas.Remove(factura);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Factura eliminada correctament" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }


        }
    }
}
