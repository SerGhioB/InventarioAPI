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
    public class TelefonoClienteController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public TelefonoClienteController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoClienteDTO>>> Get()
        {
            var telefonoclientes = await contexto.TelefonoClientes.ToListAsync(); //conexion a la bd y se extrae 
            var telefonoclientesDTO = mapper.Map<List<TelefonoClienteDTO>>(telefonoclientes); //mapeo entre el objeto "categorias y CategoriaDTO
            return telefonoclientesDTO;
        }


        [HttpGet("{id}", Name = "GetTelefonoCliente")]
        public async Task<ActionResult<TelefonoClienteDTO>> Get(int id)
        {
            var telefonocliente = await contexto.TelefonoClientes.FirstOrDefaultAsync(x => x.CodigoTelefono == id);
            if (telefonocliente == null)
            {
                return NotFound();
            }
            var telefonoclienteDTO = mapper.Map<TelefonoClienteDTO>(telefonocliente);
            return telefonoclienteDTO;

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TelefonoClienteCreacionDTO telefonoclienteCreacion)
        {
            var telefonocliente = mapper.Map<TelefonoCliente>(telefonoclienteCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(telefonocliente);
            await contexto.SaveChangesAsync();
            var telefonoclienteDTO = mapper.Map<TelefonoClienteDTO>(telefonocliente);
            return new CreatedAtRouteResult("GetTelefonoCliente", new { id = telefonocliente.CodigoTelefono }, telefonoclienteDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TelefonoClienteCreacionDTO telefonoclienteActualizacion)
        {
            var telefonocliente = mapper.Map<TelefonoCliente>(telefonoclienteActualizacion);
            telefonocliente.CodigoTelefono = id;
            contexto.Entry(telefonocliente).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoClienteDTO>> Delete(int id)
        {
            var codigoTelefonoCliente = await contexto.TelefonoClientes.Select(x => x.CodigoTelefono).FirstOrDefaultAsync(x => x == id);
            if (codigoTelefonoCliente == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new TelefonoCliente { CodigoTelefono = id });
            await contexto.SaveChangesAsync();
            return NotFound();
        }

    }
}
