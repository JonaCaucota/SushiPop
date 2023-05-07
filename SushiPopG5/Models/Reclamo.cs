using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    public class Reclamo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(255, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(10, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(10, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Telefono { get; set; }

        [ForeignKey("PedidoId")] 
        public Pedido? Pedido { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(int.MaxValue, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(10, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string DetalleReclamo { get; set; }
    }
}
