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



        //COCHES POR MARCA 
        [Route("api/CotxesMarca/{marca}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List<Cotxe>>>> GetCotxesPerMarca(string marca)
        {
           
            try
            {
                var cotxes = _context.Cotxes.Where(x => x.Marca == marca).ToList();

                if(cotxes!= null)
                {
                    return Ok(cotxes);
                }
                else
                {
                    return NotFound(new { Message = "Aquesta Marca no te cotxes regisrats" }); 
                }


            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }

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

            //SUBIR FOTO COCHE A LA API DEVUELVE EL FILENAME
        [Route("api/Cotxes/penjarFoto")]
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


            //BUSCAR IMAGEN COCHE EN LA API
        [Route("api/Cotxes/getFoto/{fileName}")]
        [HttpGet]
        public IActionResult GetFoto(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("El nom del fitxer és necessari.");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imatges", fileName);

            if (!System.IO.File.Exists(path))
                return NotFound("Fitxer no trobat.");

            var image = System.IO.File.OpenRead(path);
            var ext = Path.GetExtension(path).ToLowerInvariant();
            var mimeType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };

            return File(image, mimeType);
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
                if (cotxe != null)
                {


                    var cotxes = _context.Cotxes.Where(x => x.Matricula == cotxe.Matricula).ToList();
                    if (await _context.Cotxes.FindAsync(cotxe.Matricula) == null)
                    {
                        _context.Cotxes.Add(cotxe);
                        await _context.SaveChangesAsync();
                        return Ok(new { Message = "Cotxe afegir correctamente" });
                    }
                    else
                    {
                        return NotFound(new { Message = "Aquesta matricula ja existeix, un cotxe no pot tenir dos prpietaris " });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "El cotxe rebut es null" }); 
                }


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
