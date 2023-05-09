using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    [Table("T_CARRITO ITEM")]
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }
        
        public int CarritoId { get; set; }
        
        [ForeignKey("CarritoId")]
        public Carrito? Carrito { get; set; }
        
        public int ProductoId { get; set; }
        
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }
        
        public double Precio { get; set; }
        
        public int Cantidad { get; set; }
    }
}
