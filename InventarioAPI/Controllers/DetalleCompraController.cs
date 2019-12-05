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
    public class DetalleCompraController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public DetalleCompraController (InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompraDTO>>> Get()
        {
            var detalleCompras = await contexto.DetalleCompras.Include("Compra").Include("Producto").ToListAsync();            
            var detalleComprasDTO = mapper.Map<List<DetalleCompraDTO>>(detalleCompras);
            return detalleComprasDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetDetalleCompraPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<DetalleCompraPaginacionDTO>> GetDetalleCompraPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var detalleCompraPaginacionDTO = new DetalleCompraPaginacionDTO();
            var query = contexto.DetalleCompras.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            detalleCompraPaginacionDTO.Number = numeroDePagina;

            var detalleCompras = await contexto.DetalleCompras
                .Skip(cantidadDeRegistros * (detalleCompraPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            detalleCompraPaginacionDTO.TotalPages = totalPaginas;
            detalleCompraPaginacionDTO.Content = mapper.Map<List<DetalleCompraDTO>>(detalleCompras);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                detalleCompraPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                detalleCompraPaginacionDTO.Last = true;
            }
            return detalleCompraPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetDetalleCompra")]        
        public async Task<ActionResult<DetalleCompraDTO>> GetDetalleCompra(int id)
        {
            var detalleCompra = await contexto.DetalleCompras.FirstOrDefaultAsync(x => x.IdDetalle == id);
            if(detalleCompra == null)
            {
                return NotFound();
            }
            var detalleCompraDTO = mapper.Map<DetalleCompraDTO>(detalleCompra);
            return detalleCompraDTO;
        }

        [HttpPost]
        public async Task<ActionResult>Post([FromBody]DetalleCompraCreacionDTO detalleCompraCreacion)
        {
            var detalleCompra = mapper.Map<DetalleCompra>(detalleCompraCreacion);
            contexto.Add(detalleCompra);
            await contexto.SaveChangesAsync();
            var detalleCompraDTO = mapper.Map<DetalleCompraDTO>(detalleCompra);
            return new CreatedAtRouteResult("GetDetalleCompra", new { id = detalleCompra.IdDetalle }, detalleCompraDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>Put(int id, [FromBody]DetalleCompraCreacionDTO detalleCompraActualizacion)
        {
            var detalleCompra = mapper.Map<DetalleCompra>(detalleCompraActualizacion);
            detalleCompra.IdDetalle = id;
            contexto.Entry(detalleCompra).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleCompraDTO>>Delete(int id)
        {
            var codigoDetalleCompra = await contexto.DetalleCompras.Select(x => x.IdDetalle).FirstOrDefaultAsync(x => x == id);
            if (codigoDetalleCompra == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new DetalleCompra { IdDetalle = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
