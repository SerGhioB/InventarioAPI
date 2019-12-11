using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InventarioAPI.Entities;

namespace InventarioAPI.Models
{
    public class ProductoDTO
    {
        public int CodigoProducto { get; set; }

        [Required]
        public int CodigoCategoria { get; set; }

        [Required]
        public int CodigoEmpaque { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public Decimal PrecioUnitario { get; set; }

        [Required]
        public Decimal PrecioPorDocena { get; set; }

        [Required]
        public Decimal PrecioPorMayor { get; set; }

        [Required]
        public int Existencia { get; set; }

        [Required]
        public string Imagen { get; set; }
        public CategoriaDTO Categoria { get; set; }
        public TipoEmpaqueDTO TipoEmpaque { get; set; }
        //public List<InventarioDTO> Inventarios { get; set; }
        //public List<DetalleCompraDTO> DetalleCompras { get; set; }
        //public List<DetalleFacturaDTO> DetalleFacturas { get; set; }
    }
}
