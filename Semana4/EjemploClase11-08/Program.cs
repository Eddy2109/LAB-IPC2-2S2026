namespace EjemploClase11_08;

using System;
using System.IO;
using System.Text.Json;

class Program
{
    public class Persona
    {
        public string? Nombre { get; set; }
        public int Edad { get; set; }
        public bool EsActivo { get; set; }
    }

    static void Main(string[] args)
    {
        string rutaArchivo = "ArchivoJson.json";

        // 1. Leer el contenido completo del archivo como texto
        string jsonString = File.ReadAllText(rutaArchivo);

        // 2. Convertir el texto JSON a un objeto C#
        Persona? persona = JsonSerializer.Deserialize<Persona>(jsonString);

        // 3. Usar los datos
        Console.WriteLine($"Nombre: {persona?.Nombre}");
        Console.WriteLine($"Edad: {persona?.Edad}");
        Console.WriteLine($"EsActivo: {persona?.EsActivo}");

        Console.WriteLine("\n--- Guardar un objeto como JSON ---\n");
        // convertir un objeto a JSON
        Persona obj = new Persona
        {
            Nombre = "Juan",
            Edad = 30,
            EsActivo = true
        };
        string json = JsonSerializer.Serialize(obj);
        Console.WriteLine(json);

        // Guardar el JSON en un archivo
        File.WriteAllText("ArchivoJsonSalida.json", json);
    }
}
