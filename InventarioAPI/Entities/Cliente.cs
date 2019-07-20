﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Cliente
    {
        public string Nit { get; set; }

        [Required]
        public string DPI { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Direccion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }

        public virtual ICollection<EmailCliente> EmailClientes { get; set; }

        public virtual ICollection<TelefonoCliente> TelefonoClientes { get; set; }
    }
}
