namespace Semana7;

class Program
{
    static void Main(string[] args)
    {
        ArbolAVL arbol = new ArbolAVL();

        // Insertar nodos en el árbol AVL
        
        arbol.Insertar(20, "Introducción a la programación");
        arbol.Insertar(30, "Estructuras de datos");
        arbol.Insertar(40, "Algoritmos");
        arbol.Insertar(10, "Fundamentos de computación");
        arbol.Insertar(25, "Programación orientada a objetos");
        arbol.Insertar(35, "Base de datos");
        arbol.Insertar(50, "Sistemas operativos");

        // Imprimir el árbol en orden
        arbol.ImprimirInOrder();


        // Graficar el árbol AVL
        arbol.Graficar();
    }
}
