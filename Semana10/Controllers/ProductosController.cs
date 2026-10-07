using Microsoft.AspNetCore.Mvc;
using Semana10.Models;

namespace Semana10.Controllers
{
    [ApiController] // http://localhost:XXXX/api/Productos
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ListaProductos lista;

        public ProductosController(ListaProductos lista)
        {
            this.lista = lista;
        }

        // GET: /api/Productos
        [HttpGet]
        public IActionResult ObtenerMensaje()
        {
            return Ok(new
            {
                mensaje = "API de productos funcionando correctamente"
            });
        }

        // GET: /api/Productos/1
        [HttpGet("{id}")]
        public IActionResult ObtenerProducto(int id)
        {
            Producto? producto = lista.Buscar(id);

            if (producto == null)
            {
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });
            }

            return Ok(producto);
        }

        // POST: /api/Productos
        [HttpPost]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            Producto? existente = lista.Buscar(producto.Id);

            if (existente != null)
            {
                return Conflict(new
                {
                    mensaje = "Ya existe un producto con ese ID"
                });
            }

            lista.Agregar(producto);

            return CreatedAtAction(
                nameof(ObtenerProducto),
                new { id = producto.Id },
                producto
            );
        }
    }
}