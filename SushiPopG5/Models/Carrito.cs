namespace SushiPopG5.Models
{
    public class Carrito
    {
        public long clienteId { get; set; }
        public List<Producto> productos { get; set; }
        public Boolean procesado { get; set; } = false;
        public Boolean cancelado { get; set; } = false;

    }
}
