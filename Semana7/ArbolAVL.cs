namespace Semana7;

using System.Text;
using System.Diagnostics;
class ArbolAVL
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

    private Nodo Insertar(Nodo nodo, int valor)
    {
        // 1. Insertar como ABB
        if (nodo == null)
            return new Nodo(valor);

        if (valor < nodo.Valor)
            nodo.Izquierdo = Insertar(nodo.Izquierdo, valor);

        else if (valor > nodo.Valor)
            nodo.Derecho = Insertar(nodo.Derecho, valor);

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
        if (balance > 1 && valor < nodo.Izquierdo.Valor)
        {
            // Graficar();
            return RotacionDerecha(nodo);
        }

        // 5. RR
        if (balance < -1 && valor > nodo.Derecho.Valor)
        {
            // Graficar();
            return RotacionIzquierda(nodo);
        }

        // 6. LR
        if (balance > 1 && valor > nodo.Izquierdo.Valor)
        {
            nodo.Izquierdo =
                RotacionIzquierda(nodo.Izquierdo);

            return RotacionDerecha(nodo);
        }

        // 7. RL
        if (balance < -1 && valor < nodo.Derecho.Valor)
        {
            nodo.Derecho =
                RotacionDerecha(nodo.Derecho);

            return RotacionIzquierda(nodo);
        }

        return nodo;
    }

    public void Insertar(int valor)
    {
        Raiz = Insertar(Raiz, valor);
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
            Console.Write($"{nodo_actual.Valor} ");
            InOrder(nodo_actual.Derecho);
        }
    }


    public void Graficar()
    {
        if (Raiz == null)
        {
            Console.WriteLine("El árbol está vacío.");
            return;
        }

        StringBuilder dot = new StringBuilder();
        string dotFilePath = "ArbolAVL.dot";
        string pngFilePath = "ArbolAVL.png";

        dot.AppendLine("digraph AVL {");
        dot.AppendLine("    node [shape=circle];");

        if (Raiz != null)
        {
            GenerarDot(Raiz, dot);
        }

        dot.AppendLine("}");

        File.WriteAllText(dotFilePath, dot.ToString());

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments = $"-Tpng {dotFilePath} -o {pngFilePath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    Console.WriteLine($"¡Gráfica generada exitosamente en: {pngFilePath}!");
                    Process.Start(new ProcessStartInfo(pngFilePath) { UseShellExecute = true });
                }
                else
                {
                    Console.WriteLine("Error en Graphviz: " + process.StandardError.ReadToEnd());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("No se pudo ejecutar Graphviz. " + ex.Message);
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
                $"    {nodo.Valor} -> {nodo.Izquierdo.Valor};"
            );

            GenerarDot(nodo.Izquierdo, dot);
        }

        // Nodo derecho
        if (nodo.Derecho != null)
        {
            dot.AppendLine(
                $"    {nodo.Valor} -> {nodo.Derecho.Valor};"
            );

            GenerarDot(nodo.Derecho, dot);
        }
    }
}