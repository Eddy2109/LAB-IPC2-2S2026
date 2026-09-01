namespace Semana6
{
    public class Nodo
    {
        public int Valor {get; set;}
        public Nodo? HijoIzq {get; set;}
        public Nodo? HijoDer {get; set;}

        public Nodo(int valor)
        {
            Valor = valor;
            HijoIzq = null;
            HijoDer = null;
        }
    }
}