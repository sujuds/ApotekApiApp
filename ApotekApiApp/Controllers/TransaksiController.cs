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
    public class TransaksiController : ControllerBase
    {
        private ApotekDbContext _context;

        public TransaksiController(ApotekDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaksi>>> GetAll()
        {
            var result = await _context.Transaksis.Select(o => new
            {
                o.Id,
                o.Kode,
                o.Total,
                TransaksiDetails =  o.TransaksiDetails.Select(ot => 
                    new {
                        ot.Id,
                        ot.Jumlah,
                        Obat = ot.Obat
                    }
                ).ToList()
            }).ToListAsync();

            return new JsonResult(new
            {
                status = "success",
                data = result
            });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Transaksi>> GetById(int id)
        {
            var result = await _context.Transaksis
                .Where(t => t.Id == id)
                .Select(o => new {
                    o.Id,
                    o.Kode,
                    o.Total,
                    TransaksiDetails = o.TransaksiDetails.Select(ot =>
                        new {
                            ot.Id,
                            ot.Jumlah,
                            Obat = ot.Obat
                        }
                    ).ToList()
                }).ToListAsync();

            if (result == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new
                {
                    status = "error",
                    message = "id tidak ditemukan"
                });
            }

            return new JsonResult(new
            {
                status = "success",
                data = result
            });
        }

        [HttpPost]
        public async Task<ActionResult<Transaksi>> Add(Transaksi transaksi)
        {
            foreach (var item in transaksi.TransaksiDetails)
            {
                item.ObatId = item.Obat.Id;
                item.Obat = null;
            }

            _context.Transaksis.Add(transaksi);
            await _context.SaveChangesAsync();

            return new JsonResult(new
            {
                status = "success",
                message = "data berhasil ditambahkan!"
            });
        }
    }
}
