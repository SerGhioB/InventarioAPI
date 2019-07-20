using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class EmailClienteCreacionDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Nit { get; set; }
    }
}
