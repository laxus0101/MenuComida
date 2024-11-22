using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MenuComida.Data;
using MenuComida.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MenuComida.Pages
{
    public class loginv2Model : PageModel
    {
        private readonly MenuComida.Data.MenuComidaContext _context;

        public loginv2Model(MenuComida.Data.MenuComidaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string nombre { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string correo { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string contrasena { get; set; }

        [BindProperty]
        public Usuarios Usuarios { get; set; } = default!;

        // Método OnPostAsync para validar y registrar al usuario
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si el modelo no es válido, regresa al formulario con los errores.
            }

            // Validación de si el correo ya está registrado
            var existingUserByEmail = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.correo == correo);

            if (existingUserByEmail != null)
            {
                // Si ya existe un usuario con ese correo, agregar un error al modelo
                ModelState.AddModelError("correo", "Este correo electrónico ya está registrado.");
                return Page(); // Regresa al formulario con el error.
            }


            // Si las validaciones pasan, se guarda el usuario
            _context.Usuarios.Add(Usuarios);
            await _context.SaveChangesAsync();

            // Registro exitoso, mandar a la página principal.
            return RedirectToPage("./Index");
        }
    }

}
