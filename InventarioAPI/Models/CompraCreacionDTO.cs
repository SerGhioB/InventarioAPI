using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class CompraCreacionDTO
    {
        [Required]
        public int NumeroDocumento { get; set; }

        [Required]
        public int CodigoProveedor { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Decimal Total { get; set; }
    }
}
