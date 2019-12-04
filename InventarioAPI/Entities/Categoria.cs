using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.Entities
{
    public class Categoria
    {
        
        public int CodigoCategoria { get; set; }

        [Required]
        public string Descripcion { get; set; }

    }
}
