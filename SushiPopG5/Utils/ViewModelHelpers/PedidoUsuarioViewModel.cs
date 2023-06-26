using SushiPopG5.Models;

namespace SushiPopG5.Utils;

public class PedidoUsuarioViewModel
{
    public Pedido Pedido { get; set; }
    public List<CarritoItem> CarritoItems { get; set; }
    public Usuario Usuario { get; set; }
    
}