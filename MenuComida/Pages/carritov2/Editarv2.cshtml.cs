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
    public class Editarv2Model : PageModel
    {
        private readonly MenuComida.Data.MenuComidaContext _context;

        public Editarv2Model(MenuComida.Data.MenuComidaContext context)
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

            var carrito =  await _context.carrito.FirstOrDefaultAsync(m => m.idpedido == id);
            if (carrito == null)
            {
                return NotFound();
            }
            carrito = carrito;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(carrito).State = EntityState.Modified;

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
