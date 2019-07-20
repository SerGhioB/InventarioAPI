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
            var detallefacturas = await contexto.DetalleFacturas.ToListAsync(); //conexion a la bd y se extrae 
            var detallefacturasDTO = mapper.Map<List<DetalleFacturaDTO>>(detallefacturas); //mapeo entre el objeto "categorias y CategoriaDTO
            return detallefacturasDTO;
        }

        [HttpGet("{id}", Name = "GetDetalleFactura")]
        public async Task<ActionResult<DetalleFacturaDTO>> Get(int id)
        {
            var detallefactura = await contexto.DetalleFacturas.FirstOrDefaultAsync(x => x.CodigoDetalle == id);
            if (detallefactura == null)
            {
                return NotFound();
            }
            var detallefacturaDTO = mapper.Map<DetalleFacturaDTO>(detallefactura);
            return detallefacturaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleFacturaCreacionDTO detallefacturaCreacion)
        {
            var detallefactura = mapper.Map<DetalleFactura>(detallefacturaCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(detallefactura);
            await contexto.SaveChangesAsync();
            var detallefacturaDTO = mapper.Map<DetalleFacturaDTO>(detallefactura);
            return new CreatedAtRouteResult("GetDetalleFactura", new { id = detallefactura.CodigoDetalle }, detallefacturaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleFacturaCreacionDTO detalleFacturaActualizacion)
        {
            var detallefactura = mapper.Map<DetalleFactura>(detalleFacturaActualizacion);
            detallefactura.CodigoDetalle = id;
            contexto.Entry(detallefactura).State = EntityState.Modified;
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
            return NotFound();
        }


    }
}
