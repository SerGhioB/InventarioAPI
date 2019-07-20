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
    public class ClienteController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public ClienteController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()
        {
            var clientes = await contexto.Clientes.ToListAsync(); //conexion a la bd y se extrae 
            var clientesDTO = mapper.Map<List<ClienteDTO>>(clientes); //mapeo entre el objeto "categorias y CategoriaDTO
            return clientesDTO;
        }


        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(string id)
        {
            var cliente = await contexto.Clientes.FirstOrDefaultAsync(x => x.Nit == id);
            if (cliente == null)
            {
                return NotFound();
            }
            var clienteDTO = mapper.Map<ClienteDTO>(cliente);
            return clienteDTO;

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteCreacionDTO clienteCreacion)
        {
            var cliente = mapper.Map<Cliente>(clienteCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(cliente);
            await contexto.SaveChangesAsync();
            var clienteDTO = mapper.Map<ClienteDTO>(cliente);
            return new CreatedAtRouteResult("GetCliente", new { id = cliente.Nit }, clienteDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ClienteCreacionDTO clienteActualizacion)
        {
            var cliente = mapper.Map<Cliente>(clienteActualizacion);
            cliente.Nit = id;
            contexto.Entry(cliente).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDTO>> Delete(string id)
        {
            var codigoNit = await contexto.Clientes.Select(x => x.Nit).FirstOrDefaultAsync(x => x == id);
            if (Convert.ToInt32(codigoNit) == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Cliente { Nit = id });
            await contexto.SaveChangesAsync();
            return NotFound();
        }

    }
}
