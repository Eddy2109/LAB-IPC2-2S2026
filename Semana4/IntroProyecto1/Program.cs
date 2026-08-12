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

        String fila1 = "*****";
        String fila2 = "E    ";
        String fila3 = "**** ";
        String fila4 = "***  ";
        String fila5 = " R  *";
        String fila6 = "*****";

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
        for(int i = 0; i < fila6.Length; i++)
        {
            matriz.Insertar(fila6[i], 6, i+1);
        }

        matriz.ImprimirCuadrula();

        matriz.Graficar();
    }
}
