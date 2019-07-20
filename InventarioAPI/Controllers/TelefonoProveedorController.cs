using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TelefonoProveedorController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public TelefonoProveedorController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoProveedorDTO>>> Get()
        {
            var telefonoproveedores = await contexto.TelefonoProveedores.ToListAsync(); //conexion a la bd y se extrae 
            var telefonoproveedoresDTO = mapper.Map<List<TelefonoProveedorDTO>>(telefonoproveedores); //mapeo entre el objeto "categorias y CategoriaDTO
            return telefonoproveedoresDTO;
        }


        [HttpGet("{id}", Name = "GetTelefonoProveedor")]
        public async Task<ActionResult<TelefonoProveedorDTO>> Get(int id)
        {
            var telefonoproveedor = await contexto.TelefonoProveedores.FirstOrDefaultAsync(x => x.CodigoTelefono == id);
            if (telefonoproveedor == null)
            {
                return NotFound();
            }
            var telefonoproveedorDTO = mapper.Map<TelefonoProveedorDTO>(telefonoproveedor);
            return telefonoproveedorDTO;

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TelefonoProveedorCreacionDTO telefonoproveedorCreacion)
        {
            var telefonoproveedor = mapper.Map<TelefonoProveedor>(telefonoproveedorCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(telefonoproveedor);
            await contexto.SaveChangesAsync();
            var telefonoproveedorDTO = mapper.Map<TelefonoProveedorDTO>(telefonoproveedor);
            return new CreatedAtRouteResult("GetTelefonoProveedor", new { id = telefonoproveedor.CodigoTelefono }, telefonoproveedorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TelefonoProveedorCreacionDTO telefonoproveedorActualizacion)
        {
            var telefonoproveedor = mapper.Map<TelefonoProveedor>(telefonoproveedorActualizacion);
            telefonoproveedor.CodigoTelefono = id;
            contexto.Entry(telefonoproveedor).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoProveedorDTO>> Delete(int id)
        {
            var codigoTelefonoProveedor = await contexto.TelefonoProveedores.Select(x => x.CodigoTelefono).FirstOrDefaultAsync(x => x == id);
            if (codigoTelefonoProveedor == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new TelefonoProveedor { CodigoTelefono = id });
            await contexto.SaveChangesAsync();
            return NotFound();
        }

    }
}
