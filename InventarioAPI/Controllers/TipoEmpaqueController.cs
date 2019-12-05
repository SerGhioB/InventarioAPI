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
    public class TipoEmpaqueController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public TipoEmpaqueController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoEmpaqueDTO>>> Get()
        {
            var tipoEmpaques = await contexto.TipoEmpaques.ToListAsync(); //conexion a la bd y se extrae 
            var tipoEmpaquesDTO = mapper.Map<List<TipoEmpaqueDTO>>(tipoEmpaques); //mapeo entre el objeto "categorias y CategoriaDTO
            return tipoEmpaquesDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetTipoEmpaquePage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<TipoEmpaquePaginacionDTO>> GetTipoEmpaquePage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var tipoEmpaquePaginacionDTO = new TipoEmpaquePaginacionDTO();
            var query = contexto.TipoEmpaques.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            tipoEmpaquePaginacionDTO.Number = numeroDePagina;

            var tipoEmpaques = await contexto.TipoEmpaques
                .Skip(cantidadDeRegistros * (tipoEmpaquePaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            tipoEmpaquePaginacionDTO.TotalPages = totalPaginas;
            tipoEmpaquePaginacionDTO.Content = mapper.Map<List<TipoEmpaqueDTO>>(tipoEmpaques);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                tipoEmpaquePaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                tipoEmpaquePaginacionDTO.Last = true;
            }
            return tipoEmpaquePaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetTipoEmpaque")]
        public async Task<ActionResult<TipoEmpaqueDTO>> GetTipoEmpaque(int id)
        {
            var tipoEmpaque = await contexto.TipoEmpaques.FirstOrDefaultAsync(x => x.CodigoEmpaque == id);
            if (tipoEmpaque == null)
            {
                return NotFound();
            }
            var tipoEmpaqueDTO = mapper.Map<TipoEmpaqueDTO>(tipoEmpaque);
            return tipoEmpaqueDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoEmpaqueCreacionDTO tipoEmpaqueCreacion)
        {
            var tipoEmpaque = mapper.Map<TipoEmpaque>(tipoEmpaqueCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(tipoEmpaque);
            await contexto.SaveChangesAsync();
            var tipoEmpaqueDTO = mapper.Map<TipoEmpaqueDTO>(tipoEmpaque);
            return new CreatedAtRouteResult("GetTipoEmpaque", new { id = tipoEmpaque.CodigoEmpaque }, tipoEmpaqueDTO);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoEmpaqueCreacionDTO tipoEmpaqueActualizacion)
        {
            var tipoEmpaque = mapper.Map<TipoEmpaque>(tipoEmpaqueActualizacion);
            tipoEmpaque.CodigoEmpaque = id;
            contexto.Entry(tipoEmpaque).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoEmpaqueDTO>> Delete(int id)
        {
            var codigoTipoEmpaque = await contexto.TipoEmpaques.Select(x => x.CodigoEmpaque).FirstOrDefaultAsync(x => x == id);
            if (codigoTipoEmpaque == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new TipoEmpaque { CodigoEmpaque = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
