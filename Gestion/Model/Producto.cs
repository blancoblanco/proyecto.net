using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion.Model
{
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }
        [MaxLength(50, ErrorMessage = "el campo debe tener al menos un caracter")]
        public string nombre { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "el campo debe tener al menos un caracter")]
        public string descripcion { get; set; } = null;

        [Column(TypeName ="decimal(18,2)")]
        [DisplayFormat(DataFormatString ="{0:C2}")]
        public  decimal precio { get; set; }
    }
}
