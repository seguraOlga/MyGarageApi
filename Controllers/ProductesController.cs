﻿using System;
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
    public class ProductesController : ControllerBase
    {
        private readonly MyGarageDbContext _context;

        public ProductesController(MyGarageDbContext context)
        {
            _context = context;
        }

        // GET ALL PRODUCTES
        [Route("api/Productes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductes()
        {
            return await _context.Productes.ToListAsync();
        }

        // GET 
        [Route("api/ProductesNom/{Nom}")]
        [HttpGet]
        public async Task<ActionResult<Producte>> GetProducte(string nom)
        {
            try
            {
                var producte = await _context.Productes.Where(x => x.Descripcio.Contains(nom)).ToListAsync();

                if (producte == null)
                {
                    return NotFound(new { Message = "No s'ha trobat cap materiala" });
                }
                else
                {

                    return Ok(producte);
                }
            }
            catch (Exception e) { return BadRequest(new { Message = e.Message }); }

        }

        // GET /api/ProductesId/{id}
        [Route("api/ProductesId/{id}")]
        [HttpGet]
        public async Task<ActionResult<Producte>> GetProductePerId(int id)
        {
            try
            {
                var producte = await _context.Productes.FirstOrDefaultAsync(x => x.RefPeca == id);

                if (producte == null)
                {
                    return NotFound(new { Message = "No s'ha trobat cap producte amb aquest ID" });
                }
                else
                {
                    return Ok(producte);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }





        // GET 
        [Route("api/ProductesPart/{part}")]
        [HttpGet]
        public async Task<ActionResult<Producte>> GetProductePartVehicle(string part)
        {
            try
            {
                var producte = await _context.Productes.Where(x => x.PartVehicle == part).ToListAsync();

                if (producte == null)
                {
                    return NotFound(new { Message = "No s'ha trobat cap materiala" });
                }
                else
                {

                    return Ok(producte);
                }
            }
            catch (Exception e) { return BadRequest(new { Message = e.Message }); }

        }

        // GET 
        [Route("api/ProductesMarca/{marca}")]
        [HttpGet]
        public async Task<ActionResult<Producte>> GetProducteMarca(string marca)
        {
            try
            {
                var producte = await _context.Productes.Where(x => x.MarcaCotxe == marca).ToListAsync();

                if (producte == null)
                {
                    return NotFound(new { Message = "No s'ha trobat cap materiala" });
                }
                else
                {

                    return Ok(producte);
                }
            }
            catch (Exception e) { return BadRequest(new { Message = e.Message }); }

        }

        //PUT PRODUCTE
        [Route("api/PutProductes")]
        [HttpPut]
        public async Task<IActionResult> PutProducte([FromBody] Producte producte)
        {
            try
            {
                var producteDB = _context.Productes.Where(x => x.RefPeca == producte.RefPeca).FirstOrDefault();
                if (producteDB.RefPeca == null)
                {
                    return NotFound();
                }
                else
                {
                    producteDB.Descripcio = producte.Descripcio;
                    producteDB.PartVehicle = producte.PartVehicle;
                    producteDB.PreuCompra = producte.PreuCompra;
                    producteDB.PreuVenta = producte.PreuVenta;
                    producteDB.MarcaCotxe = producte.MarcaCotxe;
                    producteDB.Quantitat = producte.Quantitat;
                    producteDB.Imatge = producte.Imatge;
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Producte actualitzat correctament" });
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        //POST PRODUCTE
        [Route("api/PostProducte")]
        [HttpPost]
        public async Task<IActionResult> PostProducte([FromBody] Producte producte)
        {
            try
            {
                var producteDB = await _context.Productes.FindAsync(producte.RefPeca);
                if (producteDB == null)
                {
                    _context.Productes.Add(producte);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Producte Insertat correctament" });
                }
                else
                {
                    return NotFound(new { Message = "Producte no trobat" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        // DELETE
        [Route("api/DeleteProducte/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProducte(int id)
        {
            try
            {
                var poducte = await _context.Productes.Where(x => x.RefPeca == id).FirstOrDefaultAsync(); 
                if (poducte == null)
                {
                    return NotFound();
                }
                else
                {

                    _context.Productes.Remove(poducte);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Producte eliminat correctament" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        //SUBIR FOTO COCHE A LA API DEVUELVE EL FILENAME
        [Route("api/Producte/penjarFoto")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PenjarFoto([FromForm] UploadImageRequest request)
        {
            var file = request.File;

            if (file == null || file.Length == 0)
                return BadRequest("No s'ha seleccionat cap imatge.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var imagesPath = Path.Combine(wwwRootPath, "Imatges");

            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var fullPath = Path.Combine(imagesPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //var imageUrl =fileName;
            return Ok(new { fileName = fileName });
            //return Ok(fileName); 
        }

    }
}
