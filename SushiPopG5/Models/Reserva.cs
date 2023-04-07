namespace SushiPopG5.Models
{
    public class Reserva
    { 
        public long clienteId { get; set; }
        public string local { get; set; }
        public DateTime fechaYHora { get; set; }
        public Boolean confirmada { get; set; } = false;

    }
}
