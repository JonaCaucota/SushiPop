using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    public class Cliente : Usuario
    {
        public int NumeroCliente { get; set; } = 4200000;
        public List<Pedido> Pedidos { get; set; }
    }
}
