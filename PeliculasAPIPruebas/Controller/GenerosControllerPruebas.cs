using Microsoft.AspNetCore.OutputCaching;
using PeliculasAPI.Controllers;
using PeliculasAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPIPruebas.Controller
{
    [TestClass]
    public sealed class GenerosControllerPruebas: BasePruebas
    {
        [TestMethod]
        public async Task Get_DevuelveTodosLosGeneros()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();
            IOutputCacheStore outputCacheStore = null!;

            contexto.Generos.Add(new Genero() { Nombre = "Género 1" });
            contexto.Generos.Add(new Genero() { Nombre = "Género 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new GenerosController(outputCacheStore, contexto2, mapper);

            // Prueba
            var respuesta = await controller.Get();

            // Verificación
            Assert.AreEqual(expected: 2, actual: respuesta.Count());
        }
    }
}
