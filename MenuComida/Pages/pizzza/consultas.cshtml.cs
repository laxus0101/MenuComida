using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MenuComida.Data;
using MenuComida.Models;

namespace MenuComida.Pages.pizzza
{
    public class consultasModel : PageModel
    {
        private readonly MenuComida.Data.MenuComidaContext _context;

        public consultasModel(MenuComida.Data.MenuComidaContext context)
        {
            _context = context;
        }

        public IList<Pizzas> Pizzas { get; set; } = default!;

        // Propiedades para los criterios de búsqueda
        [BindProperty(SupportsGet = true)]
        public int? SearchId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchType { get; set; }

        public async Task OnGetAsync()
        {
            // Consulta inicial de todos los registros
            var query = _context.Pizzas.AsQueryable();

            // Filtro por ID si se proporciona
            if (SearchId.HasValue)
            {
                query = query.Where(p => p.id == SearchId.Value);
            }

            // Filtro por tipo de pizza si se proporciona
            if (!string.IsNullOrEmpty(SearchType))
            {
                query = query.Where(p => p.tipo.Contains(SearchType));
            }

            // Asignar el resultado filtrado a la propiedad Pizzas
            Pizzas = await query.ToListAsync();
        }
    }
}

