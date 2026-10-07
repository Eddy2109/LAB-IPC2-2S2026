namespace EjemploS5;

class Program
{
    static ListaSimple lista1 = new ListaSimple();
    static void Main(string[] args)
    {
        lista1.insertar(5);
        lista1.insertar(8);
        lista1.insertar(4);
        lista1.insertarEnOrden(9);

        lista1.MostrarDatos();

        Console.WriteLine("----------------------");
        lista1.Ordenar();
        lista1.MostrarDatos();

    }
}
