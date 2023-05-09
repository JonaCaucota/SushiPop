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
        public int Dia { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Porcentaje { get; set; }

        [Display(Name = "Descuento maximo")]
        public double? DescuentoMax { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Activo { get; set; } = true;
        
        public int ProductoId { get; set; }
        
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

    }
}
