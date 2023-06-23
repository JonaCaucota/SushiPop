using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    [Table("T_DESCUENTO")]
    public class Descuento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [Display(Name = "Día")]
        [DataType(DataType.Date)]
        public DateTime Dia { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public decimal Porcentaje { get; set; }

        [Display(Name = "Descuento maximo")]
        public decimal DescuentoMax { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Activo { get; set; } = true;
        
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int ProductoId { get; set; }
        
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

    }
}
