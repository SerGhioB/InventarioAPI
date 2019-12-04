using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class ProductoCreacionDTO
    {
        [Required] 
        public int codigoCategoria { get; set; }

        [Required]
        public int codigoEmpaque { get; set; }

        [Required]
        public string descripcion { get; set; }

        [Required]
        public Decimal precioUnitario { get; set; }

        [Required]
        public Decimal precioPorDocena { get; set; }

        [Required]
        public Decimal precioPorMayor { get; set; }

        [Required]
        public int existencia { get; set; }

        [Required]
        public string imagen { get; set; }

    }
}
