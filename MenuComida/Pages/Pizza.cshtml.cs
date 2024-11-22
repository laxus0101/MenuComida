using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MenuComida.Pages
{
    public class Contacto : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "El nombre no puede estar vacío")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "El correo no puede estar vacío")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido")]
        public string Email { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "El telefono no puede estar vacío")]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Formato de número no válido")]
        public string Telefono { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe agregar un mensaje")]
        public string Mensaje { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TempData["SuccessMessage"] = "Su formulario ha sido recibido";
            return RedirectToPage();

        }
    }
}