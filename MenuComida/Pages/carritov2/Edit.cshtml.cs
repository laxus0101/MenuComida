using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenuComida.Data;
using MenuComida.Models;

namespace MenuComida.Pages.carritov2
{
    public class EditModel : PageModel
    {
        private readonly MenuComida.Data.MenuComidaContext _context;

        public EditModel(MenuComida.Data.MenuComidaContext context)
        {
            _context = context;
        }


        [BindProperty]
        public carrito carrito { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el carrito desde la base de datos
            var carrito = await _context.carrito.FirstOrDefaultAsync(m => m.idpedido == id);

            if (carrito == null)
            {
                return NotFound();
            }
            this.carrito = carrito;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var carritoExistente = await _context.carrito.FirstOrDefaultAsync(m => m.idpedido == carrito.idpedido);

            if (carritoExistente == null)
            {
                return NotFound();
            }

            carritoExistente.cantidad = carrito.cantidad;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!carritoExists(carrito.idpedido))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool carritoExists(int id)
        {
            return _context.carrito.Any(e => e.idpedido == id);
        }
    }
}
