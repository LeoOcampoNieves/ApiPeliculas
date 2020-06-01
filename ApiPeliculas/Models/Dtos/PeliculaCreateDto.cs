using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ApiPeliculas.Models.Pelicula;

namespace ApiPeliculas.Models.Dtos
{
    public class PeliculaCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La ruta es obligatoria")]
        public string RutaImagen { get; set; }
        public IFormFile Foto { get; set; }
        [Required(ErrorMessage = "La descripción es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La duración es obligatoria")]
        public string Duracion { get; set; }
        public TipoClasificacion Clasificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Creo la relación con la tabla categoría
        public int CategoriaId { get; set; }
    }
}
