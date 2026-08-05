namespace IntroProyecto1
{
    public class nodo
    {
        public char dato; //* ' ', E, R
        public int fila;
        public int columna;
        public nodo siguiente;
        public nodo anterior;
        public nodo arriba;
        public nodo abajo;

        public nodo(char d, int f, int c)
        {
            dato = d;
            fila = f;
            columna = c;
            siguiente = null;
            anterior = null;
            arriba = null;
            abajo = null;
        }
       
    }
}