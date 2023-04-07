namespace SushiPopG5.Models
{
    public class Contacto
    {
        public string nombreCompleto { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string mensaje { get; set; } 
        public Boolean leido { get; set; } = false;
    }
}
