using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekApiApp.Models
{
    public class Transaksi
    {
        public int Id { get; set; }
        public string Kode { get; set; }
        public int Total { get; set; }
        public IEnumerable<TransaksiDetail> TransaksiDetails { get; set; }
    }
}
