using ApotekApiApp.DbContexts;
using ApotekApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObatController : ControllerBase
    {
        private ApotekDbContext _context;

        public ObatController(ApotekDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Obat>>> GetAll()
        {
            var result = await _context.Obats.ToListAsync();
            return new JsonResult(new {
                    status = "success",
                    data = result
                });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Obat>> GetById(int id)
        {
            var result = await _context.Obats.FindAsync(id);
            if (result == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new
                {
                    status = "error",
                    message = "id tidak ditemukan"
                });
            }

            return new JsonResult(new {
                status = "success",
                data = result
            });
        }

        [HttpPost]
        public async Task<ActionResult<Obat>> Add(Obat obat)
        {
            _context.Obats.Add(obat);
            await _context.SaveChangesAsync();

            var result = CreatedAtAction("GetObat", new { id = obat.Id }, obat);

            return new JsonResult(new
            {
                status = "success",
                message = "data berhasil ditambahkan!",
                data = result.Value
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Obat obat)
        {
            obat.Id = id;

            _context.Entry(obat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataExists(id))
                {
                    Response.StatusCode = StatusCodes.Status404NotFound;
                    return new JsonResult(new
                    {
                        status = "error",
                        message = "id tidak ditemukan"
                    });
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new
            {
                status = "success",
                message = "data berhasil diubah!",
                data = obat
            });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Obat>> Delete(int id)
        {
            var obat = await _context.Obats.FindAsync(id);
            if (obat == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new
                {
                    status = "error",
                    message = "id tidak ditemukan"
                });
            }

            _context.Obats.Remove(obat);
            await _context.SaveChangesAsync();
            return new JsonResult(new
            {
                status = "success",
                message = "data berhasil dihapus!"
            });
        }

        private bool DataExists(int id)
        {
            return _context.Obats.Any(e => e.Id == id);
        }
    }
}
