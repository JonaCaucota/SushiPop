using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public List<Producto> Productos { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Procesado { get; set; } = false;

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Cancelado { get; set; } = false;

        public List<CarritoItem> CarritoItems { get; set; }
        [ForeignKey("PedidoId")]
        public virtual Pedido Pedido { get; set; }
    }
}
