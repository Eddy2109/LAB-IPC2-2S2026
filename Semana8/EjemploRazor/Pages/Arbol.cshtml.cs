using EjemploRazor.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EjemploRazor.Pages;

public class ArbolModel : PageModel
{
    private readonly ArbolAVL _arbol;
    private readonly IWebHostEnvironment _environment;

    public bool ArbolExiste { get; set; }
    public bool ImagenGenerada { get; set; }

    public ArbolModel(
        ArbolAVL arbol,
        IWebHostEnvironment environment)
    {
        _arbol = arbol;
        _environment = environment;
    }

    public void OnGet()
    {
        ArbolExiste = _arbol.Raiz != null;

        if (!ArbolExiste)
        {
            return;
        }

        string carpetaImagenes =
            Path.Combine(
                _environment.WebRootPath,
                "images"
            );

        Directory.CreateDirectory(carpetaImagenes);

        ImagenGenerada = _arbol.Graficar(
            carpetaImagenes
        );
    }
}