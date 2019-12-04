using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Factura
    {
        public int NumeroFactura { get; set; }

        [Required]
        public string Nit { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Decimal Total { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
