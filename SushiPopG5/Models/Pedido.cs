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
        
        public string ClienteId { get; set; }

        public int ReclamoId { get; set; }
        
        public virtual Reclamo? Reclamo { get; set; }

        [Display(Name = "Numero de pedido")]
        public int? NroPedido { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }
        
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public decimal Subtotal { get; set; }

        public decimal? Descuento { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [Display(Name = "Gasto de Envío")]
        public decimal GastoEnvio { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public decimal Total { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Estado { get; set; }
        
    }
}
