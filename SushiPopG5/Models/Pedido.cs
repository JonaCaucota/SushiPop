using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    [Table("T_PEDIDO")]
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        
        public int CarritoId { get; set; }
        
        [ForeignKey("CarritoId")]
        public virtual Carrito? Carrito { get; set; }
        
        public int ReclamoId { get; set; }
        
        [ForeignKey("ReclamoId")]
        public virtual Reclamo? Reclamo { get; set; }

        [Display(Name = "Numero de pedido")]
        public int? NroPedido { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }
        
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Subtotal { get; set; }

        public decimal? Descuento { get; set; } = 0;

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [Display(Name = "Gasto de Envío")]
        public double GastoEnvio { get; set; } = 80;

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Total { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Estado { get; set; } = 1;
        
    }
}
