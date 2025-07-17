using PeliculasAPI.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPIPruebas
{
    [TestClass]
    public sealed class PrimeraLetraMayusculaAttributePruebas
    {
        [TestMethod]
        public void IsValid_DebeRetornarExitoso_SiElValorEsVacio()
        {
            // Preparar
            var primeraLetraMayusculaAttribute = new PrimeraLetraMayusculaAttribute();
            var validationContext = new ValidationContext(new object());
            var valor = string.Empty;

            // Probar
            var resultado = primeraLetraMayusculaAttribute.GetValidationResult(valor, validationContext);

            // Verificar
            Assert.AreEqual(expected: ValidationResult.Success, actual: resultado);
        }
    }
}
