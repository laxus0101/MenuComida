using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MenuComida.Data;
using MenuComida.Models;
using System.ComponentModel.DataAnnotations;

namespace MenuComida.Pages.carritov2
{
    public class IndexModel : PageModel
    {
        private readonly MenuComida.Data.MenuComidaContext _context;

        public IndexModel(MenuComida.Data.MenuComidaContext context)
        {
            _context = context;
        }

        public IList<carrito> carritoo { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var idsession = HttpContext.Session.GetInt32("idUsuario_");

            //carritoo = await _context.carrito.ToListAsync();
            if (idsession.HasValue)
            {
                carritoo = await _context.carrito
                    .Where(c => c.idUsuario == idsession.Value)
                    .ToListAsync();
            }
            else
            {
                // Manejar el caso en el que no haya un ID de sesión disponible.
                carritoo = new List<carrito>(); // O cualquier lógica que consideres apropiada
            }
        }

        // Método para manejar la eliminación de un artículo basado en el nombre del producto.
        public async Task<IActionResult> OnPostDeleteAsync(string producto)
        {
            var carrito = await _context.carrito.FirstOrDefaultAsync(c => c.producto == producto);


            if (carrito != null)
            {
                _context.carrito.Remove(carrito);
                await _context.SaveChangesAsync();
            }

            //return Page();
            return RedirectToPage();

        }

        [BindProperty]
        public carrito carrito { get; set; } = default!;

        public async Task<IActionResult> OnPostUpdateAsync(string producto)
        {
            //var productoToUpdate = await _context.carrito.FindAsync(producto);
            var productoToUpdate = await _context.carrito.FirstOrDefaultAsync(c => c.producto == producto);

            if (productoToUpdate == null)
            {
                //return NotFound();
                
                return RedirectToPage("/menu_carrito");
            }

            //if (await TryUpdateModelAsync<carrito>(
            //    productoToUpdate,
            //    "carrito",
            //    p => p.idpedido, p => p.producto, p => p.imagen, p => p.cantidad, p => p.precio, p => p.idUsuario))
            //{
            //    await _context.SaveChangesAsync();
            //    return RedirectToPage();
            //}

            _context.Attach(carrito).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Page();
        }

        //public async Task<IActionResult> OnPostClearCartAsync()
        //{
        //    var idsession = HttpContext.Session.GetInt32("idUsuario_");

        //    if (idsession.HasValue)
        //    {
        //        var carritoItems = _context.carrito.Where(c => c.idUsuario == idsession.Value);
        //        _context.carrito.RemoveRange(carritoItems);
        //        await _context.SaveChangesAsync();
        //    }

        //    return new JsonResult(new { success = true });
        //}

    }
}