using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleFacturaDTO
    {
        public int CodigoDetalle { get; set; }

        [Required]
        public int NumeroFactura { get; set; }

        [Required]
        public int CodigoProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public Decimal Precio { get; set; }

        [Required]
        public Decimal Descuento { get; set; }
        public FacturaDTO Factura { get; set; }
        public ProductoDTO Producto { get; set; }
    }
}
