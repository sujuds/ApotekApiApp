using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekApiApp.Models
{
    public class TransaksiDetail
    {
        public int Id { get; set; }
        public int? TransaksiId { get; set; }
        public Transaksi Transaksi { get; set; }
        public int? ObatId { get; set; }
        public Obat Obat { get; set; }
        public int Jumlah { get; set; }
    }
}
