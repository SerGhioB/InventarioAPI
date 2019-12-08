using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleCompraDTO
    {
        public int IdDetalle { get; set; }

        [Required]
        public int IdCompra { get; set; }

        [Required]
        public int CodigoProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public Decimal Precio { get; set; }
        public ProductoDTO Producto { get; set; }
        public CompraDTO Compra { get; set; }
    }
}
