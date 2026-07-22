namespace Semana1;

class Program
{

    static void ciclo_for(int a)
    {
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"{a} * {i} = {a * i}");
        }
    }

    static bool es_par(int a)
    {
        return a % 2 == 0;
    }

    static void Main(string[] args)
    {
        // Como imprimir en consola 
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Como imprimir en consola");
        Console.WriteLine("Hello, World!");
        Console.WriteLine(5);
        Console.WriteLine(5.6);
        Console.WriteLine('A');
        Console.WriteLine(true);
        Console.WriteLine(8+9*7);
        Console.WriteLine("--------------------------------");

        // Declaracion de variables
        int numero = 5;
        double n_decimal = 5.7;
        float n_decimal2 = 4.7f;
        bool var_boleana = false;
        char caracter = 'r';
        string cadena = "Bienvenidos a IPC2";

        Console.WriteLine("--------------------------------");
        Console.WriteLine("Declaracion de variables");
        Console.WriteLine(numero);
        Console.WriteLine(n_decimal);
        Console.WriteLine(n_decimal2);
        Console.WriteLine(var_boleana);
        Console.WriteLine(caracter);
        Console.WriteLine(cadena);
        Console.WriteLine("--------------------------------");

        // diferentes formas de imprimir
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Diferentes formas de imprimir");
        Console.WriteLine(numero);
        Console.WriteLine("Numero: " + numero);
        Console.WriteLine($"Numero: {numero}");
        Console.WriteLine("--------------------------------");

        // operaciones aritmeticas con y sin variales
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Operaciones aritmeticas con y sin variables");
        Console.WriteLine(8+9*7);
        Console.WriteLine(numero + n_decimal * n_decimal2);
        Console.WriteLine(numero - n_decimal / n_decimal2);
        Console.WriteLine(numero % 2);
        Console.WriteLine("--------------------------------");

        // identificadores
        int Variables = 2;
        int vARIABLES = 3;
        int _variables = 4;
        // int 5_variales = 5; --> error

        // if-else
        Console.WriteLine("--------------------------------");
        Console.WriteLine("if-else");
        int nota = -61;
        if (nota >= 61 && nota <=100)
        {
            Console.WriteLine("Curso Aprovado");
        }
        else if (nota >= 0 && nota < 61)
        {
            Console.WriteLine("Curso Reprovado");
        }
        else
        {
            Console.WriteLine("La nota no es correcta");
        }
        Console.WriteLine("--------------------------------");

        // Operadores logicos
        // && (and)
        // || (or) 
        // ! (negacion)
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Operadores logicos");
        bool var1 = !true && 5>2;
        Console.WriteLine($"var1: {var1}");
        bool var2 = 8!=4;
        Console.WriteLine($"var2: {var2}");
        Console.WriteLine(var1 || var2);

        // Menu
        Console.WriteLine("-----------------------");
        Console.WriteLine("Switch case");
        Console.WriteLine("Escoga una opcion:");
        Console.WriteLine("1. Tabla de Multiplica");
        Console.WriteLine("2. Numero par o impar");
        Console.WriteLine("3. salir");


        // pedir numeros en consola 
        string opc = Console.ReadLine();
        int opciones = int.Parse(opc);

        switch (opciones)
        {
            case 1:
                Console.WriteLine("Ingrese el numero para la tabla de multiplicar:");
                int _numero = int.Parse(Console.ReadLine());
                ciclo_for(_numero);
                break;
            case 2:
                Console.WriteLine("Ingrese un numero para saber si es par o impar:");
                int numero_ = int.Parse(Console.ReadLine());
                bool si_es_par = es_par(numero_);
                if (si_es_par)
                {
                    Console.WriteLine("El numero es par");
                }
                else
                {
                    Console.WriteLine("El numero NO es par");
                }
                break;
            default:
                Console.WriteLine("¡adios!");
                break;
        }

    }
}
