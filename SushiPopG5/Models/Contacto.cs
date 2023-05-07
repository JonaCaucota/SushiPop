using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(255, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public string Email { get; set; }

        [MaxLength(10, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(10, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string? Telefono { get; set; }

        [MaxLength(int.MaxValue, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        public string Mensaje { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Leido { get; set; } = false;
    }
}
