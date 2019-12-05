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
    public class DetalleFacturaController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public DetalleFacturaController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFacturaDTO>>> Get()
        {
            var detalleFacturas = await contexto.DetalleFacturas.Include("Factura").Include("Producto").ToListAsync(); //conexion a la bd y se extrae 
            var detalleFacturasDTO = mapper.Map<List<DetalleFacturaDTO>>(detalleFacturas); //mapeo entre el objeto "categorias y CategoriaDTO
            return detalleFacturasDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetDetalleFacturaPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<DetalleFacturaPaginacionDTO>> GetDetalleFacturaPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var detalleFacturaPaginacionDTO = new DetalleFacturaPaginacionDTO();
            var query = contexto.DetalleFacturas.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            detalleFacturaPaginacionDTO.Number = numeroDePagina;

            var detalleFacturas = await contexto.DetalleFacturas
                .Skip(cantidadDeRegistros * (detalleFacturaPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            detalleFacturaPaginacionDTO.TotalPages = totalPaginas;
            detalleFacturaPaginacionDTO.Content = mapper.Map<List<DetalleFacturaDTO>>(detalleFacturas);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                detalleFacturaPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                detalleFacturaPaginacionDTO.Last = true;
            }
            return detalleFacturaPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetDetalleFactura")]
        public async Task<ActionResult<DetalleFacturaDTO>> GetDetalleFactura(int id)
        {
            var detalleFactura = await contexto.DetalleFacturas.FirstOrDefaultAsync(x => x.CodigoDetalle == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }
            var detalleFacturaDTO = mapper.Map<DetalleFacturaDTO>(detalleFactura);
            return detalleFacturaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleFacturaCreacionDTO detalleFacturaCreacion)
        {
            var detalleFactura = mapper.Map<DetalleFactura>(detalleFacturaCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(detalleFactura);
            await contexto.SaveChangesAsync();
            var detalleFacturaDTO = mapper.Map<DetalleFacturaDTO>(detalleFactura);
            return new CreatedAtRouteResult("GetDetalleFactura", new { id = detalleFactura.CodigoDetalle }, detalleFacturaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleFacturaCreacionDTO detalleFacturaActualizacion)
        {
            var detalleFactura = mapper.Map<DetalleFactura>(detalleFacturaActualizacion);
            detalleFactura.CodigoDetalle = id;
            contexto.Entry(detalleFactura).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleFacturaDTO>> Delete(int id)
        {
            var codigoDetalleFactura = await contexto.DetalleFacturas.Select(x => x.CodigoDetalle).FirstOrDefaultAsync(x => x == id);
            if (codigoDetalleFactura == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new DetalleFactura { CodigoDetalle = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }


    }
}
