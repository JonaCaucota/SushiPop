using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    public class Reserva
    { 
        [Key]
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public string Local { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public DateTime FechaYHora { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Confirmada { get; set; } = false;

    }
}
