namespace Semana11.Models
{
    public class ListaProductos
    {
        private NodoProducto? primero;

        public ListaProductos()
        {
            primero = null;
        }

        public void Agregar(Producto producto)
        {
            NodoProducto nuevo = new NodoProducto(producto);

            if (primero == null)
            {
                primero = nuevo;
                return;
            }

            NodoProducto actual = primero;

            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }

            actual.Siguiente = nuevo;
        }

        public Producto? Buscar(int id)
        {
            NodoProducto? actual = primero;

            while (actual != null)
            {
                if (actual.Dato.Id == id)
                {
                    return actual.Dato;
                }

                actual = actual.Siguiente;
            }

            return null;
        }

        public bool Actualizar(Producto producto)
        {
            Producto? existente = Buscar(producto.Id);

            if (existente == null)
            {
                return false;
            }

            existente.Nombre = producto.Nombre;
            existente.Precio = producto.Precio;

            return true;
        }

        public bool Eliminar(int id)
        {
            if (primero == null)
            {
                return false;
            }

            if (primero.Dato.Id == id)
            {
                primero = primero.Siguiente;
                return true;
            }

            NodoProducto actual = primero;

            while (actual.Siguiente != null)
            {
                if (actual.Siguiente.Dato.Id == id)
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

        public Producto[] ObtenerTodos()
        {
            int cantidad = 0;

            NodoProducto? actual = primero;

            while (actual != null)
            {
                cantidad++;
                actual = actual.Siguiente;
            }

            Producto[] productos = new Producto[cantidad];

            actual = primero;

            int posicion = 0;

            while (actual != null)
            {
                productos[posicion] = actual.Dato;
                posicion++;

                actual = actual.Siguiente;
            }

            return productos;
        }
    }
}