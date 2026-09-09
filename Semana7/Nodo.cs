namespace Semana7
{
    public class Nodo
    {
        public int Codigo { get; set; }
        public string Curso { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public int Altura { get; set; }

        public Nodo(int codigo, string curso)
        {
            Codigo = codigo;
            Curso = curso;
            Izquierdo = null;
            Derecho = null;
            Altura = 1;
        }
    }
}