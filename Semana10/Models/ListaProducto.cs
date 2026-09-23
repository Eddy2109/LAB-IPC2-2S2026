namespace Semana10.Models
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
    }
}