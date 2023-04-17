using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class Carrito
    {
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public List<Producto> Productos { get; set; }
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public Boolean Procesado { get; set; } = false;
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public Boolean Cancelado { get; set; } = false;

    }
}
