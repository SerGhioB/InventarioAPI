using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class InventarioDTO
    {
        public int CodigoInventario { get; set; }

        [Required]
        public int CodigoProducto { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string TipoRegistro { get; set; }

        [Required]
        public Decimal Precio { get; set; }

        [Required]
        public int Entradas { get; set; }

        [Required]
        public int Salidas { get; set; }
        public ProductoDTO Producto {get; set;}
    }
}
