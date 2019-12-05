using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            var telefonoclientes = await contexto.TelefonoClientes.Include("Cliente").ToListAsync(); //conexion a la bd y se extrae 
            var telefonoclientesDTO = mapper.Map<List<TelefonoClienteDTO>>(telefonoclientes); //mapeo entre el objeto "categorias y CategoriaDTO
            return telefonoclientesDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetTelefonoClientePage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<TelefonoClientePaginacionDTO>> GetTelefonoClientePage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var telefonoClientePaginacionDTO = new TelefonoClientePaginacionDTO();
            var query = contexto.TelefonoClientes.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            telefonoClientePaginacionDTO.Number = numeroDePagina;

            var telefonoClientes = await contexto.TelefonoClientes
                .Skip(cantidadDeRegistros * (telefonoClientePaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            telefonoClientePaginacionDTO.TotalPages = totalPaginas;
            telefonoClientePaginacionDTO.Content = mapper.Map<List<TelefonoClienteDTO>>(telefonoClientes);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                telefonoClientePaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                telefonoClientePaginacionDTO.Last = true;
            }
            return telefonoClientePaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetTelefonoCliente")]
        public async Task<ActionResult<TelefonoClienteDTO>> GetTelefonoCliente(int id)
        {
            var telefonoCliente = await contexto.TelefonoClientes.FirstOrDefaultAsync(x => x.CodigoTelefono == id);
            if (telefonoCliente == null)
            {
                return NotFound();
            }
            var telefonoClienteDTO = mapper.Map<TelefonoClienteDTO>(telefonoCliente);
            return telefonoClienteDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TelefonoClienteCreacionDTO telefonoClienteCreacion)
        {
            var telefonoCliente = mapper.Map<TelefonoCliente>(telefonoClienteCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(telefonoCliente);
            await contexto.SaveChangesAsync();
            var telefonoClienteDTO = mapper.Map<TelefonoClienteDTO>(telefonoCliente);
            return new CreatedAtRouteResult("GetTelefonoCliente", new { id = telefonoCliente.CodigoTelefono }, telefonoClienteDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TelefonoClienteCreacionDTO telefonoClienteActualizacion)
        {
            var telefonoCliente = mapper.Map<TelefonoCliente>(telefonoClienteActualizacion);
            telefonoCliente.CodigoTelefono = id;
            contexto.Entry(telefonoCliente).State = EntityState.Modified;
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
            return NoContent();
        }
    }
}
