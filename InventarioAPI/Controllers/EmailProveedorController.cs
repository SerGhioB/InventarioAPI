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
            var emailproveedores = await contexto.EmailProveedores.ToListAsync(); //conexion a la bd y se extrae 
            var emailproveedoresDTO = mapper.Map<List<EmailProveedorDTO>>(emailproveedores); //mapeo entre el objeto "categorias y CategoriaDTO
            return emailproveedoresDTO;
        }

        [HttpGet("{id}", Name = "GetEmailProveedor")]
        public async Task<ActionResult<EmailProveedorDTO>> Get(int id)
        {
            var emailproveedor = await contexto.EmailProveedores.FirstOrDefaultAsync(x => x.CodigoEmail == id);
            if (emailproveedor == null)
            {
                return NotFound();
            }            
            var emailproveedorDTO = mapper.Map<EmailProveedorDTO>(emailproveedor);
            return emailproveedorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmailProveedorCreacionDTO emailproveedorCreacion)
        {
            var emailproveedor = mapper.Map<EmailProveedor>(emailproveedorCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(emailproveedor);
            await contexto.SaveChangesAsync();
            var emailproveedorDTO = mapper.Map<EmailProveedorDTO>(emailproveedor);
            return new CreatedAtRouteResult("GetEmailProveedor", new { id = emailproveedor.CodigoEmail }, emailproveedorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmailProveedorCreacionDTO emailproveedorActualizacion)
        {
            var emailproveedor = mapper.Map<EmailProveedor>(emailproveedorActualizacion);
            emailproveedor.CodigoEmail = id;
            contexto.Entry(emailproveedor).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailProveedorDTO>> Delete(int id)
        {
            var codigoEmailProveedor = await contexto.EmailProveedores.Select(x => x.CodigoEmail).FirstOrDefaultAsync(x => x == id);
            if (codigoEmailProveedor == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new EmailProveedor { CodigoEmail = id });
            await contexto.SaveChangesAsync();
            return NotFound();
        }



    }
}
