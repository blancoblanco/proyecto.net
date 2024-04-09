using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class UsuarioViewModel
    {
        [Key]
        public int idUsuario { get; set; }

        [MaxLength(50, ErrorMessage = "el campo debe tener al menos un caracter")]
        public string nombre { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "el campo debe tener al menos un caracter")]
        public string correo { get; set; } = null!;
        public double telefono { get; set; }

    }
}
