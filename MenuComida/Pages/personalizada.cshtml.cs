using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MenuComida.Data;
using MenuComida.Models;

namespace MenuComida.Pages
{
    public class personalizadaModel : PageModel
    {
        private readonly MenuComida.Data.MenuComidaContext _context;

        public personalizadaModel(MenuComida.Data.MenuComidaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public carrito carrito { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.carrito.Add(carrito);
            await _context.SaveChangesAsync();

            return RedirectToPage("./personalizada");
            //return Page();
        }
    }
}
