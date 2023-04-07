namespace SushiPopG5.Models
{
    public class Usuario
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public DateTime fechaAlta = DateTime.Now;
        public Boolean activo { get; set; }
        public string email { get; set; }

    }
}
