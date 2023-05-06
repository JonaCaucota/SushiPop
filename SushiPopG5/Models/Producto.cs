using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class Producto
    {
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(100, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(4, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(250, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(20, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public decimal Precio { get; set; }

        public decimal Costo { get; set; }
        public string? Foto { get; set; }
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Stock { get; set; } = 100;
    }
}
