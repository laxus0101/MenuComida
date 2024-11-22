using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ordenbackend
{
    public class Ordenes : PageModel
    {
        [BindProperty]
        public List<ItemSeleccionado> Items { get; set; } = new List<ItemSeleccionado>();

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar al menos un producto.")]
        public bool IsAnyItemSelected { get; set; }

        public string ResumenCompra { get; set; }
        public decimal Total { get; set; }

        public void OnGet()
        {
            // Inicializamos los productos con sus precios
            Items = ObtenerProductos();
        }

        public IActionResult OnPost()
        {
            // Verificar si al menos un producto ha sido seleccionado
            IsAnyItemSelected = Items.Exists(item => item.Seleccionado);

            if (!IsAnyItemSelected)
            {
                ModelState.AddModelError("", "Debe seleccionar al menos un producto.");
                return Page();
            }

            // Si hay productos seleccionados, calcular el total
            Total = 0;
            ResumenCompra = "";
            foreach (var item in Items)
            {
                if (item.Seleccionado)
                {
                    Total += item.Precio;
                    ResumenCompra += $"{item.Nombre} - ${item.Precio:F2} MXN\n";
                }
            }

            return Page();
        }

        // Método para obtener los productos disponibles
        private List<ItemSeleccionado> ObtenerProductos()
        {
            return new List<ItemSeleccionado>
            {
                new ItemSeleccionado { Nombre = "Flautas", Precio = 50 },
                new ItemSeleccionado { Nombre = "Salmón", Precio = 150 },
                new ItemSeleccionado { Nombre = "Hamburguesa", Precio = 80 },
                new ItemSeleccionado { Nombre = "Arrachera", Precio = 200 },
                new ItemSeleccionado { Nombre = "Costilla", Precio = 180 },
                new ItemSeleccionado { Nombre = "Fajitas de pollo", Precio = 120 },
                new ItemSeleccionado { Nombre = "Limonada", Precio = 30 },
                new ItemSeleccionado { Nombre = "Agua de Jamaica", Precio = 25 },
                new ItemSeleccionado { Nombre = "Té helado", Precio = 35 },
                new ItemSeleccionado { Nombre = "Panacota", Precio = 60 },
                new ItemSeleccionado { Nombre = "Flan", Precio = 40 },
                new ItemSeleccionado { Nombre = "Cheesecake", Precio = 70 }
            };
        }
    }

    // Clase para representar cada producto
    public class ItemSeleccionado
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public bool Seleccionado { get; set; }
    }
}
