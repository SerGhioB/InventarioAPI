﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Producto
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

        public virtual Categoria Categoria { get; set; }

        public virtual TipoEmpaque TipoEmpaque { get; set; }

    }
}
