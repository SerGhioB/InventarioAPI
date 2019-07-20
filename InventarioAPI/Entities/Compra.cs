using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Compra
    {
        public int IdCompra { get; set; }

        [Required]
        public int NumeroDocumento { get; set; }

        [Required]
        public int CodigoProveedor { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Decimal Total { get; set; }

        public virtual Proveedor Proveedor { get; set; }
    }
}
