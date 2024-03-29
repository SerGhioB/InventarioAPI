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
    public class InventarioController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public InventarioController (InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioDTO>>> Get()
        {
            var inventarios = await contexto.Inventarios.Include("Producto").ToListAsync();            
            var inventariosDTO = mapper.Map<List<InventarioDTO>>(inventarios);
            return inventariosDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetInventarioPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<InventarioPaginacionDTO>> GetInventarioPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var inventarioPaginacionDTO = new InventarioPaginacionDTO();
            var query = contexto.Inventarios.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            inventarioPaginacionDTO.Number = numeroDePagina;

            var inventarios = await contexto.Inventarios
                .Skip(cantidadDeRegistros * (inventarioPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            inventarioPaginacionDTO.TotalPages = totalPaginas;
            inventarioPaginacionDTO.Content = mapper.Map<List<InventarioDTO>>(inventarios);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                inventarioPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                inventarioPaginacionDTO.Last = true;
            }
            return inventarioPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetInventario")]
        public async Task<ActionResult<InventarioDTO>> GetInventario(int id)
        {
            var inventario = await contexto.Inventarios.FirstOrDefaultAsync(x => x.CodigoInventario == id);
            if (inventario == null)
            {
                return NotFound();
            }
            var inventarioDTO = mapper.Map<InventarioDTO>(inventario);
            return inventarioDTO;
        }

        [HttpPost]
        public async Task<ActionResult>Post([FromBody] InventarioCreacionDTO inventarioCreacion)
        {
            var inventario = mapper.Map<Inventario>(inventarioCreacion);
            contexto.Add(inventario);
            await contexto.SaveChangesAsync();
            var inventarioDTO = mapper.Map<InventarioDTO>(inventario);
            return new CreatedAtRouteResult("GetInventario", new { id = inventario.CodigoInventario }, inventarioDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>Put(int id, [FromBody] InventarioCreacionDTO inventarioActualizacion)
        {
            var inventario = mapper.Map<Inventario>(inventarioActualizacion);
            inventario.CodigoInventario = id;
            contexto.Entry(inventario).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InventarioDTO>> Delete(int id)
        {
            var codigoInventario = await contexto.Inventarios.Select(x => x.CodigoInventario).FirstOrDefaultAsync(x => x == id);
            if (codigoInventario == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Inventario { CodigoInventario = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }

    }
}
