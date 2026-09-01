namespace IntroProyecto1;

class Program
{
    static MatrizOrtogonal matriz = new MatrizOrtogonal();

    // lista de entradas
    // lista de robots (recue y fighter)
    
    static void Main(string[] args)
    {

        int fila = 0;
        int columna = 0;

            // <fila numero="1">"**********"</fila>
			// <fila numero="2">"* E*****C*"</fila>
			// <fila numero="3">"* *   **R*"</fila>
			// <fila numero="4">"*   *    *"</fila>
			// <fila numero="5">"**********"</fila>

        String fila1 = "*******X**";
        String fila2 = "* E*****C*";
        String fila3 = "* *   **R*";
        String fila4 = "*   * M  *";
        String fila5 = "**********";
       

        for(int i = 0; i < fila1.Length; i++)
        {
            matriz.Insertar(fila1[i], 1, i+1);
        }
        for(int i = 0; i < fila2.Length; i++)
        {
            matriz.Insertar(fila2[i], 2, i+1);
        }
        for(int i = 0; i < fila3.Length; i++)
        {
            matriz.Insertar(fila3[i], 3, i+1);
        }
        for(int i = 0; i < fila4.Length; i++)
        {
            matriz.Insertar(fila4[i], 4, i+1);
        }
        for(int i = 0; i < fila5.Length; i++)
        {
            matriz.Insertar(fila5[i], 5, i+1);
        }
        // for(int i = 0; i < fila6.Length; i++)
        // {
        //     matriz.Insertar(fila6[i], 6, i+1);
        // }
        // for(int i = 0; i < fila7.Length; i++)
        // {
        //     matriz.Insertar(fila7[i], 7, i+1);
        // }
        // for(int i = 0; i < fila8.Length; i++)
        // {
        //     matriz.Insertar(fila8[i], 8, i+1);
        // }
        // for(int i = 0; i < fila9.Length; i++)
        // {
        //     matriz.Insertar(fila9[i], 9, i+1);
        // }
        // for(int i = 0; i < fila10.Length; i++)
        // {
        //     matriz.Insertar(fila10[i], 10, i+1);
        // }

        matriz.ImprimirCuadrula();

        matriz.Graficar();
    }
}
