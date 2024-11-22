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

namespace MenuComida.Pages.personas
{
    public class consultapersonasModel : PageModel
    {
        private readonly MenuComida.Data.MenuComidaContext _context;

        public consultapersonasModel(MenuComida.Data.MenuComidaContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

        public string nombre { get; set; } 
        public string mailperfil {  get; set; }
        public int idUsuario_ { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Verifica los campos ingresados"; 
                return Page();  
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == Email);

            if (usuario == null)
            {
                // Si el correo no existe, mostrar un mensaje de error
                //ViewData["ErrorMessage"] = "Usuario incorrecto intenta de nuevo o crea una cuenta.";
                ModelState.AddModelError("Email", "Correo incorrecto, crea cuenta o intenta de nuevo.");
                return Page();
            }


            if (usuario.contrasena == Password)
            {
                // Guardar el ID del usuario en una variable global o en una sesión
                idUsuario_ = usuario.idUsuario;
                nombre = usuario.nombre;
                mailperfil = usuario.correo;

                HttpContext.Session.SetInt32("idUsuario_", idUsuario_);
                HttpContext.Session.SetString("nombre", nombre);
                HttpContext.Session.SetString("mailperfil", mailperfil);

                //return RedirectToPage("/Index");
                return Page();
            }
            else
            {

                //ViewData["ErrorMessage"] = "La clave es incorrecta intenta de nuevo";
                ModelState.AddModelError("Password", "Contraseña incorrecta.");
                return Page();
            }
        }
        public IActionResult OnPostReset()
        {
            // Reiniciar las variables
            idUsuario_ = 0;
            nombre = string.Empty;
            mailperfil = string.Empty;


            HttpContext.Session.Remove("idUsuario_");
            HttpContext.Session.Remove("nombre");
            HttpContext.Session.Remove("mailperfil");

            // Redirigir o mantener el estado actual
            TempData.Clear();  // Limpia los valores de TempData si se usan
            return RedirectToPage();  // Redirige para que se vea la página con los valores reiniciados
        }

    }
}

