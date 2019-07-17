﻿using AutoMapper;
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
    public class DetalleCompraController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public DetalleCompraController (InventarioDBContext context, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompraDTO>>> Get()
        {
            var detallecompras = await contexto.DetalleCompras.ToListAsync();
            var detallecomprasDTO = mapper.Map<List<DetalleCompraDTO>>(detallecompras);
            return detallecomprasDTO;
        }

        [HttpGet("{id}", Name = "GetDetalleCompra")]        
        public async Task<ActionResult<DetalleCompraDTO>> Get (int id)
        {
            var detallecompra = await contexto.DetalleCompras.FirstOrDefaultAsync(x => x.IdDetalle == id);
            if(detallecompra == null)
            {
                return NotFound();
            }
            var detallecompraDTO = mapper.Map < DetalleCompraDTO>(detallecompra);
            return detallecompraDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleCompraCreacionDTO detallecompraCreacion)
        {
            var detallecompra = mapper.Map<DetalleCompra>(detallecompraCreacion);
            contexto.Add(detallecompra);
            await contexto.SaveChangesAsync();
            var detallecompraDTO = mapper.Map<DetalleCompraDTO>(detallecompra);
            return new CreatedAtRouteResult("GetDetalleCompra", new { id = detallecompra.IdCompra }, detallecompraDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleCompraCreacionDTO detallecompraActualizacion)
        {
            var detallecompra = mapper.Map<DetalleCompra>(detallecompraActualizacion);
            detallecompra.IdCompra = id;
            contexto.Entry(detallecompra).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleCompraDTO>> Delete(int id)
        {
            var codigoDetalleCompra = await contexto.DetalleCompras.Select(x => x.IdCompra).FirstOrDefaultAsync(x => x == id);
            if (codigoDetalleCompra == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new DetalleCompra { IdCompra = id });
            await contexto.SaveChangesAsync();
            return NotFound();
        }



    }
}
