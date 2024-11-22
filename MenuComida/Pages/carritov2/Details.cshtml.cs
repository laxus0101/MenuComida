using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MenuComida.Data;
using MenuComida.Models;

namespace MenuComida.Pages.carritov2
{
    public class DetailsModel : PageModel
    {
        private readonly MenuComida.Data.MenuComidaContext _context;

        public DetailsModel(MenuComida.Data.MenuComidaContext context)
        {
            _context = context;
        }

        public carrito carrito { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.carrito.FirstOrDefaultAsync(m => m.idUsuario == id);
            if (carrito == null)
            {
                return NotFound();
            }
            else
            {
                carrito = carrito;
            }
            return Page();
        }
    }
}
