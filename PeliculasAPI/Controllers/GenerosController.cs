using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController: ControllerBase
    {
        private readonly IRepositorio repositorio;

        public GenerosController(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listadoGeneros")]
        [OutputCache]
        public List<Genero> Get()
        {
            var generos = repositorio.ObtenerTodosLosGeneros();

            return generos;
        }

        [HttpGet("{id:int}")] // api/generos/500
        [OutputCache]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await repositorio.ObtenerPorId(id);

            if (genero is null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpGet("{nombre}")] // api/generos/Felipe
        public async Task<Genero?> Get(string nombre)
        {   
            var genero = await repositorio.ObtenerPorId(1);
            return genero;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Genero genero)
        {
            var yaExisteUnGeneroConDichoNombre = repositorio.Existe(genero.Nombre);

            if (yaExisteUnGeneroConDichoNombre)
            {
                return BadRequest($"Ya existe un género con el nombre {genero.Nombre}");
            }

            return Ok();

        }

        [HttpPut]
        public void Put()
        {

        }

        [HttpDelete]
        public void Delete()
        {

        }

    }
}
