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
    public class EmailProveedorController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public EmailProveedorController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailProveedorDTO>>> Get()
        {
            var emailProveedores = await contexto.EmailProveedores.Include("Proveedor").ToListAsync(); //conexion a la bd y se extrae 
            var emailProveedoresDTO = mapper.Map<List<EmailProveedorDTO>>(emailProveedores); //mapeo entre el objeto "categorias y CategoriaDTO
            return emailProveedoresDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetEmailProveedorPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<EmailProveedorPaginacionDTO>> GetEmailProveedorPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var emailProveedorPaginacionDTO = new EmailProveedorPaginacionDTO();
            var query = contexto.EmailProveedores.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            emailProveedorPaginacionDTO.Number = numeroDePagina;

            var emailProveedores = await contexto.EmailProveedores
                .Skip(cantidadDeRegistros * (emailProveedorPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync(); //conexion a la bd y se extrae 

            emailProveedorPaginacionDTO.TotalPages = totalPaginas;
            emailProveedorPaginacionDTO.Content = mapper.Map<List<EmailProveedorDTO>>(emailProveedores);
            //var categoriasDTO = mapper.Map < List<CategoriaDTO>>(categorias); //mapeo entre el objeto "categorias y CategoriaDTO

            if (numeroDePagina == 0)
            {
                emailProveedorPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                emailProveedorPaginacionDTO.Last = true;
            }
            return emailProveedorPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetEmailProveedor")]
        public async Task<ActionResult<EmailProveedorDTO>> GetEmailProveedor(int id)
        {
            var emailProveedor = await contexto.EmailProveedores.FirstOrDefaultAsync(x => x.CodigoEmail == id);
            if (emailProveedor == null)
            {
                return NotFound();
            }            
            var emailProveedorDTO = mapper.Map<EmailProveedorDTO>(emailProveedor);
            return emailProveedorDTO;
        }

        [HttpPost]
        public async Task<ActionResult>Post([FromBody]EmailProveedorCreacionDTO emailProveedorCreacion)
        {
            var emailProveedor = mapper.Map<EmailProveedor>(emailProveedorCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(emailProveedor);
            await contexto.SaveChangesAsync();
            var emailProveedorDTO = mapper.Map<EmailProveedorDTO>(emailProveedor);
            return new CreatedAtRouteResult("GetEmailProveedor", new { id = emailProveedor.CodigoEmail }, emailProveedorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>Put(int id, [FromBody]EmailProveedorCreacionDTO emailProveedorActualizacion)
        {
            var emailProveedor = mapper.Map<EmailProveedor>(emailProveedorActualizacion);
            emailProveedor.CodigoEmail = id;
            contexto.Entry(emailProveedor).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailProveedorDTO>>Delete(int id)
        {
            var codigoEmailProveedor = await contexto.EmailProveedores.Select(x => x.CodigoEmail).FirstOrDefaultAsync(x => x == id);
            if (codigoEmailProveedor == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new EmailProveedor { CodigoEmail = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }



    }
}
