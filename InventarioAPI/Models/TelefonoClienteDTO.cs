using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class TelefonoClienteDTO
    {
        public int CodigoTelefono { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Nit { get; set; }
        public ClienteDTO Cliente { get; set; }
    }
}
