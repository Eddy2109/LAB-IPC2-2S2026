namespace Semana7;

class Program
{
    static void Main(string[] args)
    {
        ArbolAVL arbol = new ArbolAVL();

        // Insertar nodos en el árbol AVL
        
        arbol.Insertar(20);
        arbol.Insertar(30);
        arbol.Insertar(40);
        // arbol.Insertar(10);
        // arbol.Insertar(25);
        // arbol.Insertar(35);
        // arbol.Insertar(50);

        // Imprimir el árbol en orden
        arbol.ImprimirInOrder();


        // Graficar el árbol AVL
        arbol.Graficar();
    }
}
