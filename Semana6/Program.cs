namespace Semana6;

class Program
{
    static void Main(string[] args)
    {
        ArbolBB arbol = new ArbolBB();

        arbol.Insertar(50);
        arbol.Insertar(30);
        arbol.Insertar(70);
        arbol.Insertar(20);
        arbol.Insertar(40);

        Console.Write("In-Order (Izquierda -> Raíz -> Derecha):   ");
        arbol.ImprimirInOrder();   // Salida: 20 30 40 50 70

        Console.Write("Pre-Order (Raíz -> Izquierda -> Derecha):  ");
        arbol.ImprimirPreOrder();  // Salida: 50 30 20 40 70

        Console.Write("Post-Order (Izquierda -> Derecha -> Raíz): ");
        arbol.ImprimirPostOrder(); // Salida: 20 40 30 70 50
    }
}
