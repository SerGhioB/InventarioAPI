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
    public class ProductoController : ControllerBase
    {                
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public ProductoController (InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get()
        {
            var productos = await contexto.Productos.Include("Categoria").Include("TipoEmpaque").ToListAsync();
            var productosDTO = mapper.Map<List<ProductoDTO>>(productos);
            return productosDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetProductoPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<ProductoPaginacionDTO>> GetProductoPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var productoPaginacionDTO = new ProductoPaginacionDTO();
            var query = contexto.Productos.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            productoPaginacionDTO.Number = numeroDePagina;

            var productos = await contexto.Productos
                .Skip(cantidadDeRegistros * (productoPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            productoPaginacionDTO.TotalPages = totalPaginas;
            productoPaginacionDTO.Content = mapper.Map<List<ProductoDTO>>(productos);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                productoPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                productoPaginacionDTO.Last = true;
            }
            return productoPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetProducto")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            var producto = await contexto.Productos.FirstOrDefaultAsync(x => x.CodigoProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            var productoDTO = mapper.Map<ProductoDTO>(producto);
            return productoDTO;
        }

        [HttpPost]
        public async Task<ActionResult>Post([FromBody] ProductoCreacionDTO productoCreacion)
        {
            var producto = mapper.Map<Producto>(productoCreacion);
            contexto.Add(producto);
            await contexto.SaveChangesAsync();
            var productoDTO = mapper.Map<ProductoDTO>(producto);
            return new CreatedAtRouteResult("GetProducto", new { id = producto.CodigoProducto }, productoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>Put(int id, [FromBody]ProductoCreacionDTO productoAcualizacion)
        {
            var producto = mapper.Map<Producto>(productoAcualizacion);
            producto.CodigoProducto = id;
            contexto.Entry(producto).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        public async Task<ActionResult>Delete (int id)
        {
            var producto = await contexto.Productos.Select(x => x.CodigoProducto).FirstOrDefaultAsync(x => x == id);
            if (producto == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Producto { CodigoProducto = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
