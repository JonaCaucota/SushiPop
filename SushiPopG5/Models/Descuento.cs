namespace SushiPopG5.Models
{
    public class Descuento
    {
        public long productoId { get; set; }
        public int dia { get; set; }
        public double porcentaje { get; set; }
        public double descuentoMax { get; set; }
        public Boolean activo { get; set; } = true;

    }
}
