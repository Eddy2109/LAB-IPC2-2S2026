using Microsoft.AspNetCore.Mvc;
using Semana11.Models;

namespace Semana11.Controllers
{
    [ApiController]
    // [Route("api/[controller]")]
    [Route("api/productos")]
    public class ProductosApiController : ControllerBase
    {
        private readonly ListaProductos lista;

        public ProductosApiController(ListaProductos lista)
        {
            this.lista = lista;
        }

        // GET: /api/Productos
        [HttpGet]
        public IActionResult ObtenerMensaje()
        {
            Producto[] productos = lista.ObtenerTodos();
            return Ok(productos);
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

        // PUT: /api/productos/id
        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(
            int id,
            [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID de la URL no coincide con el ID del producto"
                });
            }

            bool actualizado = lista.Actualizar(producto);

            if (!actualizado)
            {
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });
            }

            return Ok(producto);
        }

        // DELETE: /api/productos/1
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            bool eliminado = lista.Eliminar(id);

            if (!eliminado)
            {
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });
            }

            return Ok(new
            {
                mensaje = "Producto eliminado correctamente"
            });
        }
    }
}