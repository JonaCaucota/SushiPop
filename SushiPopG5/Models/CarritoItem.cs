using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }
        public Carrito Carrito { get; set; }
        public Producto Producto { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
