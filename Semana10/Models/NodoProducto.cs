namespace Semana10.Models
{
    public class NodoProducto
    {
        public Producto Dato { get; set; }
        public NodoProducto Siguiente { get; set; }

        public NodoProducto(Producto producto)
        {
            Dato = producto;
            Siguiente = null;
        }
    }
}