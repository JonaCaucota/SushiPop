using System.ComponentModel;
using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SushiPopG5.Models
{
    [Table("T_USUARIO")]
    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido) ]
        [MaxLength(30, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(2, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(30, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(2, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(100, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(10, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(10, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de alta")]
        public DateTime? FechaAlta { get; set; }
        
        public bool? Activo { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
