using System.ComponentModel.DataAnnotations;

namespace MenuComida.Models
{
    public class Usuarios
    {
        [Key]
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
    }
}
