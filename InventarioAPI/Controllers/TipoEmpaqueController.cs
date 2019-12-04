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
    public class TipoEmpaqueController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public TipoEmpaqueController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoEmpaqueDTO>>> Get()
        {
            var tipoempaques = await contexto.TipoEmpaques.ToListAsync(); //conexion a la bd y se extrae 
            var tipoempaquesDTO = mapper.Map<List<TipoEmpaqueDTO>>(tipoempaques); //mapeo entre el objeto "categorias y CategoriaDTO
            return tipoempaquesDTO;
        }


        [HttpGet("{id}", Name = "GetTipoEmpaque")]
        public async Task<ActionResult<TipoEmpaqueDTO>> Get(int id)
        {
            var tipoempaque = await contexto.TipoEmpaques.FirstOrDefaultAsync(x => x.codigoEmpaque == id);
            if (tipoempaque == null)
            {
                return NotFound();
            }
            var tipoempaqueDTO = mapper.Map<TipoEmpaqueDTO>(tipoempaque);
            return tipoempaqueDTO;

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoEmpaqueCreacionDTO tipoempaqueCreacion)
        {
            var tipoempaque = mapper.Map<TipoEmpaque>(tipoempaqueCreacion); //mapeo entre el objeto "categoriaCreacion y Categoria
            contexto.Add(tipoempaque);
            await contexto.SaveChangesAsync();
            var tipoempaqueDTO = mapper.Map<TipoEmpaqueDTO>(tipoempaque);
            return new CreatedAtRouteResult("GetTipoEmpaque", new { id = tipoempaque.codigoEmpaque }, tipoempaqueDTO);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoEmpaqueCreacionDTO tipoEmpaqueActualizacion)
        {
            var tipoempaque = mapper.Map<TipoEmpaque>(tipoEmpaqueActualizacion);
            tipoempaque.codigoEmpaque = id;
            contexto.Entry(tipoempaque).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoEmpaqueDTO>> Delete(int id)
        {
            var tipoEmpaque = await contexto.TipoEmpaques.Select(x => x.codigoEmpaque).FirstOrDefaultAsync(x => x == id);
            if (tipoEmpaque == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new TipoEmpaque { codigoEmpaque = id });
            await contexto.SaveChangesAsync();
            return NotFound();
        }
    }
}
