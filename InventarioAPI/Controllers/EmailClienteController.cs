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
    public class EmailClienteController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public EmailClienteController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailClienteDTO>>> Get()
        {
            var emailclientes = await contexto.EmailClientes.ToListAsync(); //conexion a la bd y se extrae 
            var emailclientesDTO = mapper.Map<List<EmailClienteDTO>>(emailclientes); //mapeo entre el objeto "categorias y CategoriaDTO
            return emailclientesDTO;
        }


        [HttpGet("{id}", Name = "GetEmailCliente")]
        public async Task<ActionResult<EmailClienteDTO>> Get(int id)
        {
            var emailcliente = await contexto.EmailClientes.FirstOrDefaultAsync(x => x.CodigoEmail == id);
            if (emailcliente == null)
            {
                return NotFound();
            }
            var emailclienteDTO = mapper.Map<EmailClienteDTO>(emailcliente);
            return emailclienteDTO;

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmailClienteCreacionDTO emailclienteCreacion)
        {
            var emailcliente = mapper.Map<EmailCliente>(emailclienteCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(emailcliente);
            await contexto.SaveChangesAsync();
            var emailclienteDTO = mapper.Map<EmailClienteDTO>(emailcliente);
            return new CreatedAtRouteResult("GetEmailCliente", new { id = emailcliente.CodigoEmail }, emailclienteDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmailClienteCreacionDTO emailclienteActualizacion)
        {
            var emailcliente = mapper.Map<EmailCliente>(emailclienteActualizacion);
            emailcliente.CodigoEmail = id;
            contexto.Entry(emailcliente).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailClienteDTO>> Delete(int id)
        {
            var codigoEmailCliente = await contexto.EmailClientes.Select(x => x.CodigoEmail).FirstOrDefaultAsync(x => x == id);
            if (codigoEmailCliente == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new EmailCliente { CodigoEmail = id });
            await contexto.SaveChangesAsync();
            return NotFound();
        }

    }
}
