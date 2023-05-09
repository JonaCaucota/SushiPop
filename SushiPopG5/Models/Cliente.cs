using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    [Table("T_CLIENTE")]
    public class Cliente : Usuario
    {
        public int? NumeroCliente { get; set; }
        
        public List<Carrito>? Carritos { get; set; }
        
        public List<Reserva>? Reservas { get; set; }
        
    }
}
