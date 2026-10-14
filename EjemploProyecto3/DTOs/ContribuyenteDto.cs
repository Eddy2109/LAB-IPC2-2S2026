using System.Text.Json.Serialization;

namespace EjemploProyecto3.DTOs;

public class ContribuyenteDto
{
    [JsonPropertyName("NIT")]
    public string Nit { get; set; } = string.Empty;

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;
}