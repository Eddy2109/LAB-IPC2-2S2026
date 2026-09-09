using EjemploRazor.Models;
using EjemploRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EjemploRazor.Pages;

public class IngresarModel : PageModel
{
    private readonly ArbolAVL _arbol;

    public IngresarModel(ArbolAVL arbol)
    {
        _arbol = arbol;
    }

    [BindProperty]
    public ArbolViewModel Datos { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (Datos.Codigo <= 0)
        {
            Datos.Mensaje = "El código debe ser mayor que 0.";
            return Page();
        }

        if (string.IsNullOrWhiteSpace(Datos.Curso))
        {
            Datos.Mensaje = "Debe ingresar el nombre del curso.";
            return Page();
        }

        _arbol.Insertar(
            Datos.Codigo,
            Datos.Curso
        );

        Datos.Mensaje =
            $"Curso '{Datos.Curso}' agregado correctamente.";

        return Page();
    }
}