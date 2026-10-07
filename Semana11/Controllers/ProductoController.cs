using Microsoft.AspNetCore.Mvc;
using Semana11.Models;

namespace Semana11.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ListaProductos lista;

        public ProductosController(ListaProductos lista)
        {
            this.lista = lista;
        }

        // GET: /Productos
        [HttpGet]
        public IActionResult Index()
        {
            Producto[] productos = lista.ObtenerTodos();

            return View(productos);
        }

        // GET: /Productos/Detalle/X
        [HttpGet]
        public IActionResult Detalle(int id)
        {
            Producto? producto = lista.Buscar(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: /Productos/Crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Productos/Crear
        [HttpPost]
        public IActionResult Crear(Producto producto)
        {
            Producto? existente = lista.Buscar(producto.Id);

            if (existente != null)
            {
                ModelState.AddModelError(
                    "Id",
                    "Ya existe un producto con ese ID."
                );

                return View(producto);
            }

            lista.Agregar(producto);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Productos/Editar/1
        [HttpGet]
        public IActionResult Editar(int id)
        {
            Producto? producto = lista.Buscar(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: /Productos/Editar
        [HttpPost]
        public IActionResult Editar(Producto producto)
        {
            Producto? existente = lista.Buscar(producto.Id);

            if (existente == null)
            {
                return NotFound();
            }

            lista.Actualizar(producto);

            return RedirectToAction(nameof(Index));
        }

        // POST: /Productos/Eliminar/1
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            bool eliminado = lista.Eliminar(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}