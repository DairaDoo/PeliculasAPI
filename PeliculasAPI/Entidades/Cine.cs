using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class Cine: IId
    {
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public required string Nombre { get; set; }
        // Tipo de Dato Point que se utiliza con la Libreria Net TopologySuite para manejar datos geográficos.
        public required Point Ubicacion { get; set; }
    }
}
