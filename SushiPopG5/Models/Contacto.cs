using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class Contacto
    {
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int IdContacto { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(255, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public string Email { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(10, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(10, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Telefono { get; set; }
        [MaxLength(int.MaxValue, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        public string Mensaje { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Leido { get; set; } = false;
    }
}
