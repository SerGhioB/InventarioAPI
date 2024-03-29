﻿using AutoMapper;
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
    public class CompraController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public CompraController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraDTO>>> Get()
        {
            var compras = await contexto.Compras.Include("Proveedor").ToListAsync(); //conexion a la bd y se extrae 
            var comprasDTO = mapper.Map<List<CompraDTO>>(compras); //mapeo entre el objeto "categorias y CategoriaDTO
            return comprasDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetCompraPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<CompraPaginacionDTO>> GetCompraPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var compraPaginacionDTO = new CompraPaginacionDTO();
            var query = contexto.Compras.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            compraPaginacionDTO.Number = numeroDePagina;

            var compras = await contexto.Compras
                .Skip(cantidadDeRegistros * (compraPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            compraPaginacionDTO.TotalPages = totalPaginas;
            compraPaginacionDTO.Content = mapper.Map<List<CompraDTO>>(compras);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                compraPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                compraPaginacionDTO.Last = true;
            }
            return compraPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetCompra")]
        public async Task<ActionResult<CompraDTO>> GetCompra(int id)
        {
            var compra = await contexto.Compras.FirstOrDefaultAsync(x => x.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }
            var compraDTO = mapper.Map<CompraDTO>(compra);
            return compraDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CompraCreacionDTO compraCreacion)
        {
            var compra = mapper.Map<Compra>(compraCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(compra);
            await contexto.SaveChangesAsync();
            var compraDTO = mapper.Map<CompraDTO>(compra);
            return new CreatedAtRouteResult("GetCompra", new { id = compra.IdCompra }, compraDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CompraCreacionDTO compraActualizacion)
        {
            var compra = mapper.Map<Compra>(compraActualizacion);
            compra.IdCompra = id;
            contexto.Entry(compra).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CompraDTO>> Delete(int id)
        {
            var codigoCompra = await contexto.Compras.Select(x => x.IdCompra).FirstOrDefaultAsync(x => x == id);
            if (codigoCompra == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Compra { IdCompra = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }

    }
}
