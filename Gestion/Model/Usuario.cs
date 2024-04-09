using System.ComponentModel.DataAnnotations;

namespace Gestion.Model
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [MaxLength(50,ErrorMessage ="el campo debe tener al menos un caracter")]
        public string nombre { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "el campo debe tener al menos un caracter")]
        public string correo { get; set; } = null!;
        public double telefono { get; set; } 


    }
}
