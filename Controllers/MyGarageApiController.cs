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
  //  [Route("api/[controller]")]
   // [ApiController]
    public class MyGarageApiController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public MyGarageApiController(MyGarageDbContext context)
        {
            _context = context;
        }



        //----------------COTXES----------------//




      







        //----------------REPARACIONES----------------//







        //    //GET TODAS LAS REPARACIONES
        //[Route("api/Reparaciones")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Reparacio>>> GetReparacio()
        //{
        //    return await _context.Reparacios.ToListAsync();
        //}



        //    //GET DE UNA REPARACION POR CLIENTE

        //[Route("api/Reparacions/{dni}")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<List<Reparacio>>>> GetReparacioClient([FromBody] string dni)
        //{
        //    try

        //    {
        //        var listarep = new List<Reparacio>();
        //        var client = await _context.Clients.FindAsync(dni);
        //        if (client != null)
        //        {
        //            var listacotxes = _context.Cotxes.Where(x => x.Dni == client.Dni).ToList();
        //            foreach (var cotx in listacotxes)
        //            {
        //                foreach (var rep in cotx.Reparacios)
        //                {
        //                    listarep.Add(rep);
        //                }

        //            }
        //            return Ok(listarep);
        //        }
        //        else
        //        {
        //            return NotFound(new { Message = "El DNI introduït no pertany a cap registre actual" });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { Message = e.Message });
        //    }

        //}

        //    //REPARACIONES POR ESTADO
        //[Route("api/ReparacionEstat")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<List<Reparacio>>>> GetReparacionsEstat([FromBody] string estat)
        //{
           
        //    try
        //    {
        //        var reparacions = _context.Reparacios.Where(x => x.Estat == estat).ToList();

        //        return Ok(reparacions);


        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { Message = e.Message });
        //    }

        //}



        //    //POST DE UNA REPARACION
        //[Route("api/ReparacionesAdd")]
        //[HttpPost]
        //public async Task<ActionResult<Reparacio>> PostReparacio([FromBody] Reparacio reparacio)
        //{

        //    try
        //    {
        //        if (reparacio != null)
        //        {
        //            _context.Reparacios.AddAsync(reparacio);
        //            await _context.SaveChangesAsync();
        //            return Ok(new { Message = "Reparacio afegida correctament" });
        //        }
        //        else
        //        {
        //            return NotFound(new { Message = "La reparació és buida." });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { Message = e.Message });
        //    }
        //}





        ////----------------PRODUCTES----------------//


        //    // GET TODOS LOS PRODUCTOS
        //[Route("api/Productes")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Producte>>> GetProductes()
        //{
        //    return await _context.Productes.ToListAsync();
        //}


        ////----------------MATERIALREPARACIO----------------//


        //    //GET TODOS LOS MATERIALES
        //[Route("api/MaterialReparacio")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MaterialReparacio>>> GetMaterial()
        //{
        //    return await _context.MaterialReparacios.ToListAsync();
        //}



        ////----------------FACTURA----------------//


        //    //GET TODAS LAS FACTURAS
        //[Route("api/Factura")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Factura>>> GetFactura()
        //{
        //    return await _context.Facturas.ToListAsync();
        //}

        




        ////----------------PRESUPUESTOS----------------//



        ////GET TODOS LOS PRESUPUESTOS
        //[Route("api/Pressupost")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Pressupost>>> GetPressupost()
        //{
        //    return await _context.Pressuposts.ToListAsync();
        //}


        //    //GET PRESSUPOSTOS POR ESTADOS
        //[Route("api/PressupostEstat")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<List<Reparacio>>>> GetPressupostEstat([FromBody] string estat)
        //{

        //    try
        //    {
        //        var pressupostos = _context.Pressuposts.Where(x => x.Estat == estat).ToList();

        //        return Ok(pressupostos);


        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { Message = e.Message });
        //    }

        //}
    }
}
