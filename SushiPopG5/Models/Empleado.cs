using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    [Table("T_EMPLEADO")]
    public class Empleado : Usuario
    {
        public int? Legajo { get; set; }
    }
}
