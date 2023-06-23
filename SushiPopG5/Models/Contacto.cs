using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    [Table("T_CONTACTO")]
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(255, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [MaxLength(10, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(10, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [MaxLength(int.MaxValue, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        public string Mensaje { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [Display(Name = "Leído")]
        public bool Leido { get; set; } = false;

    
    }
}
