using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("CarritoId")]
        public Carrito? Carrito { get; set; }
        
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }
        
        public double Precio { get; set; }
        
        public int Cantidad { get; set; }
    }
}
