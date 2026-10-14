namespace Semana12.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo {get; set;} = string.Empty;
        public string? Nombre { get; set; }
        public double Precio { get; set; }
    }
}