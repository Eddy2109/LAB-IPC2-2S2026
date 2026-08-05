namespace IntroProyecto1;

class Program
{
    static ListaRobot listaRobots = new();
    static MatrizOrtogonal matriz = new MatrizOrtogonal();
    static void Main(string[] args)
    {
        // agregar un rotot fighter con capacidad 10
        Fighter fighter1 = new Fighter(10);
        // agregar un robot recue
        Recue recue1 = new Recue();
        // agregar los robots a la lista
        listaRobots.AgregarRobot(fighter1);
        listaRobots.AgregarRobot(recue1);
        listaRobots.MostrarRobots();

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
    }
}
