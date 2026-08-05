namespace IntroProyecto1
{
    class MatrizOrtogonal
    {
        private nodo? raiz;

        //agregar mas atributos ejemplo: nombreCiudad, valorAlfanumerico, etc

        // Les recomiendo tener una lista de entradas, otra de robots, recursos, etc. 
        // Para no reccorrer toda la matriz para buscarlos y mostrarlos
        //lista simple de estradas

        public MatrizOrtogonal()
        {
            raiz = new nodo('X', 0, 0);
        }

        // 1. Obtener o crear el nodo cabecera de la fila
        private nodo ObtenerOCrearCabeceraFila(int fila)
        {
            nodo actual = raiz;

            // Avanzar verticalmente hacia abajo mientras la fila sea menor
            while (actual.abajo != null && actual.abajo.fila < fila)
            {
                actual = actual.abajo;
            }

            // Si la cabecera de la fila ya existe, la retornamos
            if (actual.abajo != null && actual.abajo.fila == fila)
            {
                return actual.abajo;
            }

            // Si no existe, creamos una nueva cabecera de fila (columna = -1)
            nodo nuevaCabecera = new nodo(' ', fila, -1);
            nuevaCabecera.abajo = actual.abajo;
            nuevaCabecera.arriba = actual;

            if (actual.abajo != null)
            {
                actual.abajo.arriba = nuevaCabecera;
            }
            actual.abajo = nuevaCabecera;

            return nuevaCabecera;
        }

        // 2. Obtener o crear el nodo cabecera de la columna
        private nodo ObtenerOCrearCabeceraColumna(int columna)
        {
            nodo actual = raiz;

            // Avanzar horizontalmente hacia la derecha mientras la columna sea menor
            while (actual.siguiente != null && actual.siguiente.columna < columna)
            {
                actual = actual.siguiente;
            }

            // Si la cabecera de la columna ya existe, la retornamos
            if (actual.siguiente != null && actual.siguiente.columna == columna)
            {
                return actual.siguiente;
            }

            // Si no existe, creamos una nueva cabecera de columna (fila = -1)
            nodo nuevaCabecera = new nodo(' ', -1, columna);
            nuevaCabecera.siguiente = actual.siguiente;
            nuevaCabecera.anterior = actual;

            if (actual.siguiente != null)
            {
                actual.siguiente.anterior = nuevaCabecera;
            }
            actual.siguiente = nuevaCabecera;

            return nuevaCabecera;
        }

        // 3. Insertar o actualizar un nodo en la matriz
        public void Insertar(char dato, int fila, int columna)
        {
            nodo cabeceraFila = ObtenerOCrearCabeceraFila(fila);
            nodo cabeceraColumna = ObtenerOCrearCabeceraColumna(columna);

            nodo nuevo = new nodo(dato, fila, columna);

            InsertarEnFila(nuevo, cabeceraFila);
            InsertarEnColumna(nuevo, cabeceraColumna);
        }

        private void InsertarEnFila(nodo nuevo, nodo cabeceraFila)
        {
            nodo actual = cabeceraFila;

            // Avanzar horizontalmente
            while (actual.siguiente != null && actual.siguiente.columna < nuevo.columna)
            {
                actual = actual.siguiente;
            }

            // Si el nodo en esa posición ya existe, solo actualizamos el dato
            if (actual.siguiente != null && actual.siguiente.columna == nuevo.columna)
            {
                actual.siguiente.dato = nuevo.dato;
                return;
            }

            // Enlazar horizontalmente
            nuevo.siguiente = actual.siguiente;
            nuevo.anterior = actual;

            if (actual.siguiente != null)
            {
                actual.siguiente.anterior = nuevo;
            }
            actual.siguiente = nuevo;
        }

        private void InsertarEnColumna(nodo nuevo, nodo cabeceraColumna)
        {
            nodo actual = cabeceraColumna;

            // Avanzar verticalmente
            while (actual.abajo != null && actual.abajo.fila < nuevo.fila)
            {
                actual = actual.abajo;
            }

            // Si ya existe (y se actualizó en la fila), no volvemos a enlazar
            if (actual.abajo != null && actual.abajo.fila == nuevo.fila)
            {
                return;
            }

            // Enlazar verticalmente
            nuevo.abajo = actual.abajo;
            nuevo.arriba = actual;

            if (actual.abajo != null)
            {
                actual.abajo.arriba = nuevo;
            }
            actual.abajo = nuevo;
        }

        public void ImprimirCuadrula()
        {
            if (raiz.abajo == null || raiz.siguiente == null)
            {
                Console.WriteLine("La matriz está vacía.");
                return;
            }

            // 1. Calcular el límite máximo de filas recorriendo las cabeceras verticales
            int maxFila = 0;
            nodo tempFila = raiz.abajo;
            while (tempFila != null)
            {
                maxFila = tempFila.fila;
                tempFila = tempFila.abajo;
            }

            // 2. Calcular el límite máximo de columnas recorriendo las cabeceras horizontales
            int maxColumna = 0;
            nodo tempCol = raiz.siguiente;
            while (tempCol != null)
            {
                maxColumna = tempCol.columna;
                tempCol = tempCol.siguiente;
            }

            // 3. Imprimir la cuadrícula hasta los límites encontrados
            nodo cabeceraFila = raiz.abajo;

            for (int f = 0; f <= maxFila; f++)
            {
                nodo actual = null;

                // Verificamos si existe la cabecera para la fila actual
                if (cabeceraFila != null && cabeceraFila.fila == f)
                {
                    actual = cabeceraFila.siguiente; // Primer nodo real de la fila (ignora la cabecera)
                    cabeceraFila = cabeceraFila.abajo;
                }

                for (int c = 0; c <= maxColumna; c++)
                {
                    // Si hay un nodo de datos en la coordenada (f, c)
                    if (actual != null && actual.columna == c)
                    {
                        Console.Write($"[{actual.dato}] ");
                        actual = actual.siguiente;
                    }
                }
                Console.WriteLine();
            }
        }


    }
}