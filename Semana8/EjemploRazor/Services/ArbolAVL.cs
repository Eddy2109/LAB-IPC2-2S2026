namespace EjemploRazor.Services;

using System.Text;
using System.Diagnostics;
public class ArbolAVL
{
    public Nodo Raiz;

    private int ObtenerAltura(Nodo nodo)
    {
        if (nodo == null)
            return 0;

        return nodo.Altura;
    }

    private int FactorBalance(Nodo nodo)
    {
        if (nodo == null)
            return 0;

        return ObtenerAltura(nodo.Izquierdo)
             - ObtenerAltura(nodo.Derecho);
    }

    private Nodo RotacionDerecha(Nodo y)
    {
        // Graficar();
        Nodo x = y.Izquierdo;
        Nodo temporal = x.Derecho;

        x.Derecho = y;
        y.Izquierdo = temporal;

        y.Altura = 1 + Math.Max(
            ObtenerAltura(y.Izquierdo),
            ObtenerAltura(y.Derecho)
        );

        x.Altura = 1 + Math.Max(
            ObtenerAltura(x.Izquierdo),
            ObtenerAltura(x.Derecho)
        );

        return x;
    }

    private Nodo RotacionIzquierda(Nodo x)
    {
        // Graficar();
        Nodo y = x.Derecho;
        Nodo temporal = y.Izquierdo;

        y.Izquierdo = x;
        x.Derecho = temporal;

        x.Altura = 1 + Math.Max(
            ObtenerAltura(x.Izquierdo),
            ObtenerAltura(x.Derecho)
        );

        y.Altura = 1 + Math.Max(
            ObtenerAltura(y.Izquierdo),
            ObtenerAltura(y.Derecho)
        );

        return y;
    }

    private Nodo Insertar(Nodo nodo, int codigo, string curso)
    {
        // 1. Insertar como ABB
        if (nodo == null)
            return new Nodo(codigo, curso);

        if (codigo < nodo.Codigo)
            nodo.Izquierdo = Insertar(nodo.Izquierdo, codigo, curso);

        else if (codigo > nodo.Codigo)
            nodo.Derecho = Insertar(nodo.Derecho, codigo, curso);

        else
            return nodo;

        // 2. Actualizar altura
        nodo.Altura = 1 + Math.Max(
            ObtenerAltura(nodo.Izquierdo),
            ObtenerAltura(nodo.Derecho)
        );

        // 3. Calcular balance
        int balance = FactorBalance(nodo);

        // 4. LL
        if (balance > 1 && codigo < nodo.Izquierdo.Codigo)
        {
            // Graficar();
            return RotacionDerecha(nodo);
        }

        // 5. RR
        if (balance < -1 && codigo > nodo.Derecho.Codigo)
        {
            // Graficar();
            return RotacionIzquierda(nodo);
        }

        // 6. LR
        if (balance > 1 && codigo > nodo.Izquierdo.Codigo)
        {
            nodo.Izquierdo =
                RotacionIzquierda(nodo.Izquierdo);

            return RotacionDerecha(nodo);
        }

        // 7. RL
        if (balance < -1 && codigo < nodo.Derecho.Codigo)
        {
            nodo.Derecho =
                RotacionDerecha(nodo.Derecho);

            return RotacionIzquierda(nodo);
        }

        return nodo;
    }

    public void Insertar(int codigo, string curso)
    {
        Raiz = Insertar(Raiz, codigo, curso);
    }

    public void ImprimirInOrder()
    {
        InOrder(Raiz);
        Console.WriteLine();
    }

    private void InOrder(Nodo? nodo_actual)
    {
        if (nodo_actual != null)
        {
            InOrder(nodo_actual.Izquierdo);
            Console.Write($"{nodo_actual.Codigo} - {nodo_actual.Curso} ");
            InOrder(nodo_actual.Derecho);
        }
    }


    public bool Graficar(string carpetaSalida)
    {
        if (Raiz == null)
        {
            return false;
        }

        StringBuilder dot = new StringBuilder();

        dot.AppendLine("digraph AVL {");
        dot.AppendLine("    node [shape=circle];");

        GenerarDot(Raiz, dot);

        dot.AppendLine("}");

        string dotFilePath =
            Path.Combine(carpetaSalida, "ArbolAVL.dot");

        string pngFilePath =
            Path.Combine(carpetaSalida, "ArbolAVL.png");

        File.WriteAllText(dotFilePath, dot.ToString());

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments =
                    $"\"{dotFilePath}\" -Tpng -o \"{pngFilePath}\"",

                RedirectStandardOutput = true,
                RedirectStandardError = true,

                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process =
                Process.Start(startInfo);

            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                Console.WriteLine(
                    $"¡Gráfica generada exitosamente en: {pngFilePath}!"
                );
                return true;
            }

            Console.WriteLine(
                "Error en Graphviz: " +
                process.StandardError.ReadToEnd()
            );
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR GRAPHVIZ: " + ex.Message);
            return false;
        }
    }

    private void GenerarDot(Nodo nodo, StringBuilder dot)
    {
        if (nodo == null)
            return;

        // Nodo izquierdo
        if (nodo.Izquierdo != null)
        {
            dot.AppendLine(
                $"    {nodo.Codigo} -> {nodo.Izquierdo.Codigo};"
            );

            GenerarDot(nodo.Izquierdo, dot);
        }

        // Nodo derecho
        if (nodo.Derecho != null)
        {
            dot.AppendLine(
                $"    {nodo.Codigo} -> {nodo.Derecho.Codigo};"
            );

            GenerarDot(nodo.Derecho, dot);
        }
    }
}