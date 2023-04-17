using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class Reserva
    { 
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int IdReserva { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public string Local { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public DateTime FechaYHora { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Confirmada { get; set; } = false;

    }
}
