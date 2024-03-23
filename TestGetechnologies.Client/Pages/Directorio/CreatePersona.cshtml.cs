using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestGetechnologies.Client.RestServices;
using TestGetechnologies.Shared;

namespace TestGetechnologies.Client.Pages.Directorio
{
    public class CreatePersonaModel : PageModel
    {
        private readonly DirectorioService _directorioService;

        public CreatePersonaModel(DirectorioService directorioService)
        {
            this._directorioService = directorioService;
        }

        [TempData]
        public bool? IsSuccess { get; set; } = null;
        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public CreatePersona CreatePersona { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _directorioService.CreatePersona(this.CreatePersona);

                    this.IsSuccess = result.IsSuccess;
                    if (result.IsSuccess)
                    {
                        this.Message = $"{result.Message}: Usuario creado correctamente";
                        return LocalRedirect("/Directorio/CreatePersona");
                    }
                    else
                    {
                        string ErrorMessage = result.Message;
                        ModelState.AddModelError(nameof(ErrorMessage), ErrorMessage);
                        return Page();
                    }
                }

                return Page();
            }
            catch (Exception e)
            {
                string ErrorMessage = "Ocurrio un error inesperado";
                ModelState.AddModelError(nameof(ErrorMessage), ErrorMessage);
                return Page();
            }
        }
    }
}
