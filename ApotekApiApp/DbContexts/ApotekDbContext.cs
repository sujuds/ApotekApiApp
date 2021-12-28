using ApotekApiApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekApiApp.DbContexts
{
    public class ApotekDbContext : DbContext
    {
        public DbSet<Obat> Obats { get; set; }
        public DbSet<Transaksi> Transaksis { get; set; }
        public DbSet<TransaksiDetail> TransaksiDetails { get; set; }


        public ApotekDbContext(DbContextOptions options) : base(options) { }

    }
}
