namespace Semana6
{
    public class ArbolBB
    {
        public Nodo? Raiz;

        public void Insertar(int valor)
        {
            Raiz = _insertar(Raiz, valor);
        }

        private Nodo _insertar(Nodo? actual, int valor)
        {
            if (actual == null) return new Nodo(valor);

            if (valor < actual.Valor)
            {
                actual.HijoIzq = _insertar(actual.HijoIzq, valor);
            }
            else
            {
                actual.HijoDer = _insertar(actual.HijoDer, valor);
            }

            return actual;
        }

        public bool Buscar(int valor)
        {
            return _buscar(Raiz, valor);
        }

        private bool _buscar(Nodo? nodo_actual, int valor)
        {
            if (nodo_actual == null) return false;
            if (nodo_actual.Valor == valor) return true;

            return valor < nodo_actual.Valor
                ? _buscar(nodo_actual.HijoIzq, valor)
                : _buscar(nodo_actual.HijoDer, valor);
        }

        public void ImprimirInOrder()
        {
            InOrder(Raiz);
            Console.WriteLine();
        }

        private void InOrder(Nodo? nodo_actual)
        {
            if (nodo_actual != null)
            {
                InOrder(nodo_actual.HijoIzq); // visito el sub-arbol izquierdo
                Console.Write($"{nodo_actual.Valor} "); // visito la raiz (imprimir o jalar el dato)
                InOrder(nodo_actual.HijoDer); // visito el sub-arbol derecho
            }
        }

        public void ImprimirPreOrder()
        {
            PreOrder(Raiz);
            Console.WriteLine();
        }

        private void PreOrder(Nodo? nodo_actual)
        {
            if (nodo_actual != null)
            {
                Console.Write($"{nodo_actual.Valor} ");  // Visita primero la raíz
                PreOrder(nodo_actual.HijoIzq);      // Recorre subárbol izquierdo
                PreOrder(nodo_actual.HijoDer);        // Recorre subárbol derecho
            }
        }

         public void ImprimirPostOrder()
        {
            PostOrder(Raiz);
            Console.WriteLine();
        }

        private void PostOrder(Nodo? nodo_actual)
        {
            if (nodo_actual != null)
            {
                PostOrder(nodo_actual.HijoIzq);     // Recorre subárbol izquierdo
                PostOrder(nodo_actual.HijoDer);       // Recorre subárbol derecho
                Console.Write($"{nodo_actual.Valor} ");  // Visita al final la raíz
            }
        }

    }
}