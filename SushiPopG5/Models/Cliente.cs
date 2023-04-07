using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    public class Cliente : Usuario
    {
        public long clienteId { get; set; }
    }
}
