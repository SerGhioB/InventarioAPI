using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Proveedor
    {
        public int CodigoProveedor { get; set; }

        [Required]
        public string Nit { get; set; }

        [Required]
        public string RazonSocial { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string PaginaWeb { get; set; }

        [Required]
        public string ContactoPrincipal { get; set; }
                
        public virtual ICollection<EmailProveedor> EmailProveedores { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }

        public virtual ICollection<TelefonoProveedor> TelefonoProveedores { get; set; }
    }
}
