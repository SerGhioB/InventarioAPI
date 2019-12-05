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
    public class FacturaController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public FacturaController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDTO>>> Get()
        {
            var facturas = await contexto.Facturas.Include("Cliente").ToListAsync(); //conexion a la bd y se extrae 
            var facturasDTO = mapper.Map<List<FacturaDTO>>(facturas); //mapeo entre el objeto "categorias y CategoriaDTO
            return facturasDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetFacturaPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<FacturaPaginacionDTO>> GetFacturaPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var facturaPaginacionDTO = new FacturaPaginacionDTO();
            var query = contexto.Facturas.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            facturaPaginacionDTO.Number = numeroDePagina;

            var facturas = await contexto.Facturas
                .Skip(cantidadDeRegistros * (facturaPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            facturaPaginacionDTO.TotalPages = totalPaginas;
            facturaPaginacionDTO.Content = mapper.Map<List<FacturaDTO>>(facturas);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                facturaPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                facturaPaginacionDTO.Last = true;
            }
            return facturaPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetFactura")]
        public async Task<ActionResult<FacturaDTO>> GetFactura(int id)
        {
            var factura = await contexto.Facturas.FirstOrDefaultAsync(x => x.NumeroFactura == id);
            if (factura == null)
            {
                return NotFound();
            }
            var facturaDTO = mapper.Map<FacturaDTO>(factura);
            return facturaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FacturaCreacionDTO facturaCreacion)
        {
            var factura = mapper.Map<Factura>(facturaCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(factura);
            await contexto.SaveChangesAsync();
            var facturaDTO = mapper.Map<FacturaDTO>(factura);
            return new CreatedAtRouteResult("GetFactura", new { id = factura.NumeroFactura }, facturaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FacturaCreacionDTO facturaActualizacion)
        {
            var factura = mapper.Map<Factura>(facturaActualizacion);
            factura.NumeroFactura = id;
            contexto.Entry(factura).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FacturaDTO>> Delete(int id)
        {
            var codigoFactura = await contexto.Facturas.Select(x => x.NumeroFactura).FirstOrDefaultAsync(x => x == id);
            if (codigoFactura == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Factura { NumeroFactura = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
