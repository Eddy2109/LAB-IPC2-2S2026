using Microsoft.AspNetCore.Mvc;
using Semana12.Models;
using System.Text.RegularExpressions;

namespace Semana12.Controllers
{
    [ApiController]
    // [Route("api/[controller]")]
    [Route("api/productos")]
    public class ProductosApiController : ControllerBase
    {

        private readonly ListaProductos lista;

        private bool ValidarProducto(Producto producto, out string mensaje)
        {
            // Validar el código: 3 letras mayúsculas y 3 dígitos
            if (!Regex.IsMatch(producto.Codigo ?? "", @"^[A-Z]{3}\d{3}$"))
            {
                mensaje = "El código debe tener 3 letras mayúsculas seguidas de 3 números. Ejemplo: LAP001.";
                return false;
            }

            // Validar el nombre: solo letras y espacios
            if (!Regex.IsMatch(
                producto.Nombre ?? "",
                @"^[A-Za-zÁÉÍÓÚáéíóúÜüÑñ ]+$"))
            {
                mensaje = "El nombre solo puede contener letras y espacios.";
                return false;
            }

            // Validar que el precio sea mayor que cero
            if (producto.Precio <= 0)
            {
                mensaje = "El precio debe ser mayor que cero.";
                return false;
            }

            mensaje = string.Empty;
            return true;
        }

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
            // Producto? existente = lista.Buscar(producto.Id);

            // if (existente != null)
            // {
            //     return Conflict(new
            //     {
            //         mensaje = "Ya existe un producto con ese ID"
            //     });
            // }

            // lista.Agregar(producto);

            // return CreatedAtAction(
            //     nameof(ObtenerProducto),
            //     new { id = producto.Id },
            //     producto
            // );
            if (!ValidarProducto(producto, out string mensaje))
            {
                return BadRequest(new { mensaje });
            }

            if (lista.Buscar(producto.Id) != null)
            {
                return Conflict(new
                {
                    mensaje = "Ya existe un producto con ese ID."
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

            // validación antes de actualizar
            if (!ValidarProducto(producto, out string mensaje))
            {
                return BadRequest(new { mensaje });
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