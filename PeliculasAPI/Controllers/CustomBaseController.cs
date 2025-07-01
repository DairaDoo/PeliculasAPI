using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Utilidades;
using System.Linq.Expressions;

// Aquí el el custom base controller para centralizar operaciones comúnes 
namespace PeliculasAPI.Controllers
{
    public class CustomBaseController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly string cacheTag;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper, IOutputCacheStore outputCacheStore,
            string cacheTag)
        {
            this.context = context;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
            this.cacheTag = cacheTag;
        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>(
            Expression<Func<TEntidad, object>> ordenarPor) // usamos la entidad generica y retornamos un objeto, en c# todo tipo de dato hereda de object.
            where TEntidad : class // restricción para que TEntidad sea una clase
        {
                return await context.Set<TEntidad>()
                    .OrderBy(ordenarPor)
                    .ProjectTo<TDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        // Metodo Génerico GET, que retorna todos los objetos de una entidad paginados y ordenados.
        protected async Task<List<TDTO>> Get<TEntidad, TDTO>(PaginacionDTO paginacion,
            Expression<Func<TEntidad, object>> ordenarPor) // usamos la entidad generica y retornamos un objeto, en c# todo tipo de dato hereda de object.
            where TEntidad: class // restricción para que TEntidad sea una clase
        {
            var queryable = context.Set<TEntidad>().AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            return await queryable
                .OrderBy(ordenarPor)
                .Paginar(paginacion)
                .ProjectTo<TDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        // Método genérico GET que retorna un objeto por su Id.
        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id)
            where TEntidad : class, IId // restricción para que TEntidad sea una clase y que implemente la interfaz IId
            where TDTO : IId
        {
            var entidad = await context.Set<TEntidad>()
                .ProjectTo<TDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entidad is null)
            {
                return NotFound();
            }

            return entidad;
        }

        protected async Task<IActionResult> Post<TCreacionDTO, TEntidad, TDTO>
            (TCreacionDTO creacionDTO, string nombreRuta)
            where TEntidad: class, IId
        {
            var entidad = mapper.Map<TEntidad>(creacionDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default); // limpiamos el caché al crear genero
            var entidadDTO = mapper.Map<TDTO>(entidad);
            return CreatedAtRoute(nombreRuta, new { id = entidad.Id }, entidadDTO);
        }

        protected async Task<IActionResult> Put<TCreacionDTO, TEntidad>(int id, TCreacionDTO creacionDTO)
            where TEntidad: class, IId
        {
            var entidadExiste = await context.Set<TEntidad>().AnyAsync(g => g.Id == id);

            // si no existe devolvemos not found
            if (!entidadExiste)
            {
                return NotFound();
            }

            var entidad = mapper.Map<TEntidad>(creacionDTO);
            entidad.Id = id;

            context.Update(entidad);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default); // limpiamos el caché al actualizar genero

            return NoContent();
        }

        protected async Task<IActionResult> Delete<TEntidad>(int id)
            where TEntidad: class, IId
        {
            var registrosBorrados = await context.Set<TEntidad>().Where(g => g.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }

            // limpiamos el caché ya que hicimos un cambio en la base de datos.
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }
    }
}
