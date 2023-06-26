using Microsoft.AspNetCore.Identity;
using SushiPopG5.Models;

namespace SushiPopG5.Utils;

public class PedidoProductoViewModel
{
    public Pedido Pedido { get; set; }
    public List<Producto> Producto { get; set; }
    
    public Usuario Usuario { get; set; }
}