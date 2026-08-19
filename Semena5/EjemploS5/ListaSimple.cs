namespace EjemploS5
{
    class ListaSimple
    {
        Nodo? inicio;
        Nodo? final;

        public ListaSimple()
        {
            inicio = null;
            final = null;
        }

        public void insertar(int dato)
        {
            Nodo nuevo = new Nodo(dato);

            if (inicio == null && final == null)
            {
                inicio = nuevo;
                final = nuevo;
            }
            else
            {
                final.siguiente = nuevo;
                final = nuevo;
            }
        }

        public void insertarEnOrden(int dato)
        {
            Nodo nuevo = new Nodo(dato);

            if (inicio == null && final == null)
            {
                inicio = nuevo;
                final = nuevo;
            }
            else
            {
                Nodo temporal = inicio;
                while (temporal != null)
                {
                    if (dato >= temporal.dato)
                    {
                        Nodo? aux = temporal.siguiente;
                        Console.WriteLine($"Nodo aux {aux.dato}");
                        temporal.siguiente = nuevo;
                        nuevo.siguiente = aux;

                        // ordenar lista completa
                        // Ordenar();
                        break;
                    }
                    temporal = temporal.siguiente;
                }
            }
        }

        public void Ordenar()
        {
            Nodo? temporal = inicio;
            bool intercambio = true;

            while (intercambio)
            {
                intercambio = false;
                temporal = inicio;
                while (temporal.siguiente != null)
                {
                    if (temporal.dato >= temporal.siguiente.dato)
                    {
                        int dato = temporal.dato;
                        temporal.dato = temporal.siguiente.dato;
                        temporal.siguiente.dato = dato;
                        intercambio = true;
                        MostrarDatos();
                        Console.WriteLine("----------------------");
                    }
                    temporal = temporal.siguiente;
                }
            }
        }

        public void MostrarDatos()
        {
            Nodo? temporal = inicio;
            while (temporal != null)
            {
                Console.WriteLine(temporal.dato);
                temporal = temporal.siguiente;
            }
        }
    }
}