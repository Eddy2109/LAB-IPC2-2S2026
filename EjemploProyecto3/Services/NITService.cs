using System.Text.RegularExpressions;

namespace EjemploProyecto3.Services;

public class NitService
{
    public bool EsValido(string? nit)
    {
        if (string.IsNullOrWhiteSpace(nit))
            return false;

        nit = nit.Trim().ToUpperInvariant();

        // Entre 1 y 20 dígitos, seguidos de un dígito verificador
        // numérico o la letra K.
        if (!Regex.IsMatch(nit, @"^[0-9]{2,20}[0-9K]$"))
            return false;

        string cuerpo = nit[..^1];
        char verificador = nit[^1];

        int suma = 0;
        int posicion = 2;

        // Se recorren los dígitos del cuerpo desde la derecha,
        // comenzando por la posición 2.
        for (int i = cuerpo.Length - 1; i >= 0; i--)
        {
            suma += (cuerpo[i] - '0') * posicion;
            posicion++;
        }

        int resultado = (11 - (suma % 11)) % 11;

        char verificadorCalculado =
            resultado == 10 ? 'K' : resultado.ToString()[0];

        return verificador == verificadorCalculado;
    }
}