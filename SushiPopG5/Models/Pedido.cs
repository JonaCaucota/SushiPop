using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class Pedido
    {
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int NroPedido { get; set; } = 30000;

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Subtotal { get; set; }

        public decimal? Descuento { get; set; } = 0;

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double GastoEnvio { get; set; } = 80;

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Total { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Estado { get; set; } = 1;

        public Cliente Cliente { get; set; }
    }
}
