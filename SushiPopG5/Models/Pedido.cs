namespace SushiPopG5.Models
{
    public class Pedido
    {
        public long nroPedido { get; set; } = 30000;
        public DateTime fecha { get; set; } = DateTime.Now;
        public long carritoId { get; set; }
        public long clienteId { get; set; }
        public double subtotal { get; set; }
        public double descuento { get; set; } = 0;
        public double gastoEnvio { get; set; } = 80;
        public double total { get; set; }
        public double estado { get; set; } = 1;
    }
}
