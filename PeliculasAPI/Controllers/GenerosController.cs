using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    public class GenerosController: ControllerBase
    {

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listadoGeneros")]
        public List<Genero> Get()
        {
            var repositorio = new RepositorioEnMemoria();   
            var generos = repositorio.ObtenerTodosLosGeneros();

            return generos;
        }

        [HttpGet("{id:int}")] // api/generos/500
        public ActionResult<Genero> Get(int id)
        {
            var repositorio = new RepositorioEnMemoria();
            var genero = repositorio.ObtenerPorId(id);

            if (genero is null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpGet("{nombre}")] // api/generos/Felipe
        public Genero? Get(string nombre)
        {   
            var repositorio = new RepositorioEnMemoria();
            var genero = repositorio.ObtenerPorId(1);
            return genero;
        }

        [HttpPost]
        public void Post()
        {

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
