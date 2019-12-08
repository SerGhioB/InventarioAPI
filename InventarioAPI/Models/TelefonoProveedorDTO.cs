using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class TelefonoProveedorDTO
    {
        public int CodigoTelefono { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int CodigoProveedor { get; set; }
        public ProveedorDTO Proveedor { get; set; }
    }
}
