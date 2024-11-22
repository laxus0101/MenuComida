using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuComida.Models;

namespace MenuComida.Models
{
    public class carrito
    {
        [Key]
        public int idpedido { get; set; }
        public string producto { get; set; }
        public string imagen { get; set; }
        public int cantidad { get; set; }
        public int precio { get; set; }
        public int idUsuario { get; set; }
        
    }
}
