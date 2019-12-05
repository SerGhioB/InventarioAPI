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
            var telefonoProveedores = await contexto.TelefonoProveedores.Include("Proveedor").ToListAsync(); //conexion a la bd y se extrae 
            var telefonoProveedoresDTO = mapper.Map<List<TelefonoProveedorDTO>>(telefonoProveedores); //mapeo entre el objeto "categorias y CategoriaDTO
            return telefonoProveedoresDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetTelefonoProveedorPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<TelefonoProveedorPaginacionDTO>> GetTelefonoProveedorPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var telefonoProveedorPaginacionDTO = new TelefonoProveedorPaginacionDTO();
            var query = contexto.TelefonoProveedores.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            telefonoProveedorPaginacionDTO.Number = numeroDePagina;

            var telefonoProveedores = await contexto.TelefonoProveedores
                .Skip(cantidadDeRegistros * (telefonoProveedorPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            telefonoProveedorPaginacionDTO.TotalPages = totalPaginas;
            telefonoProveedorPaginacionDTO.Content = mapper.Map<List<TelefonoProveedorDTO>>(telefonoProveedores);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                telefonoProveedorPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                telefonoProveedorPaginacionDTO.Last = true;
            }
            return telefonoProveedorPaginacionDTO;
        }


        [HttpGet("{id}", Name = "GetTelefonoProveedor")]
        public async Task<ActionResult<TelefonoProveedorDTO>> GetTelefonoProveedor(int id)
        {
            var telefonoProveedor = await contexto.TelefonoProveedores.FirstOrDefaultAsync(x => x.CodigoTelefono == id);
            if (telefonoProveedor == null)
            {
                return NotFound();
            }
            var telefonoProveedorDTO = mapper.Map<TelefonoProveedorDTO>(telefonoProveedor);
            return telefonoProveedorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TelefonoProveedorCreacionDTO telefonoProveedorCreacion)
        {
            var telefonoProveedor = mapper.Map<TelefonoProveedor>(telefonoProveedorCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(telefonoProveedor);
            await contexto.SaveChangesAsync();
            var telefonoProveedorDTO = mapper.Map<TelefonoProveedorDTO>(telefonoProveedor);
            return new CreatedAtRouteResult("GetTelefonoProveedor", new { id = telefonoProveedor.CodigoTelefono }, telefonoProveedorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]TelefonoProveedorCreacionDTO telefonoProveedorActualizacion)
        {
            var telefonoProveedor = mapper.Map<TelefonoProveedor>(telefonoProveedorActualizacion);
            telefonoProveedor.CodigoTelefono = id;
            contexto.Entry(telefonoProveedor).State = EntityState.Modified;
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
            return NoContent();
        }
    }
}
