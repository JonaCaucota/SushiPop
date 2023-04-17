using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class Categoria
    {
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int IdCategoria { get; set; } 
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(100, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(100, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Nombre { get; set; }
        [MaxLength(int.MaxValue, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        public string Descripcion { get; set; }

    }
}
