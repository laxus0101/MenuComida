using System.ComponentModel.DataAnnotations;

namespace MenuComida.Models
{
    public class Pizzas
    {
        [Key]
        public int id { get; set; }
        public string tipo { get; set; }
        public int precio { get; set; }

    }
}
