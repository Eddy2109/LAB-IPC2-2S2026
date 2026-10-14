using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using EjemploProyecto3.Data;
using EjemploProyecto3.DTOs;
using EjemploProyecto3.Services;

namespace EjemploProyecto3.Controllers;

[ApiController]
[Route("api/rtu")]
public class RtuController : ControllerBase
{
    // Conexión para la base de datos
    private readonly ConexionBD _conexionBD;
    // El servicio que nos validara con las expresiones regulares
    private readonly NitService _nitService;

    public RtuController(
        ConexionBD conexionBD,
        NitService nitService)
    {
        _conexionBD = conexionBD;
        _nitService = nitService;
    }

    [HttpPost]
    public IActionResult ActualizarRTU(
        [FromBody] List<ContribuyenteDto> contribuyentes)
    {
        // validamos que el mensaje venga acompañado un la lista de contribuyentes
        if (contribuyentes == null)
            return BadRequest(new
            {
                mensaje = "Debe enviar una lista de contribuyentes."
            });

        // Contadores que nos serviran para enviar de respuesta en el endpoint
        int nuevos = 0;
        int actualizados = 0;
        int invalidos = 0;

        // conexion a la base de datos
        using var conexion = _conexionBD.ObtenerConexion(); 
        // en esta variable guardaremos la transaccion que se realizara en la base de datos
        using var transaccion = conexion.BeginTransaction();

        try
        {   
            // recorremos la lista de contribuyentes que nos enviaron en el body del endpoint
            foreach (var contribuyente in contribuyentes)
            {
                // validamos que el NIT y el nombre no vengan vacios
                string nit = contribuyente.Nit.Trim()
                    .ToUpperInvariant();

                string nombre = contribuyente.Nombre.Trim();

                // validamos que el NIT sea valido (con el servicio que ya creamos), si no lo es, aumentamos el contador de invalidos y continuamos con el siguiente contribuyente
                if (!_nitService.EsValido(nit))
                {
                    invalidos++;
                    continue;
                }

                // consultamos si el contribuyente ya existe en la base de datos
                using var consulta = conexion.CreateCommand();
                consulta.Transaction = transaccion;
                consulta.CommandText = @"
                    SELECT IdContribuyente
                    FROM Contribuyente
                    WHERE Nit = $nit;
                ";

                consulta.Parameters.AddWithValue("$nit", nit);

                object? resultado = consulta.ExecuteScalar();

                // si el resultado es null, significa que el contribuyente no existe, por lo que lo insertamos, si no es null, significa que el contribuyente ya existe, por lo que lo actualizamos
                if (resultado == null)
                {
                    using var insertar = conexion.CreateCommand();
                    insertar.Transaction = transaccion;
                    insertar.CommandText = @"
                        INSERT INTO Contribuyente (Nit, Nombre)
                        VALUES ($nit, $nombre);
                    ";

                    insertar.Parameters.AddWithValue("$nit", nit);
                    insertar.Parameters.AddWithValue("$nombre", nombre);
                    insertar.ExecuteNonQuery();

                    nuevos++;
                }
                else
                {
                    using var actualizar = conexion.CreateCommand();
                    actualizar.Transaction = transaccion;
                    actualizar.CommandText = @"
                        UPDATE Contribuyente
                        SET Nombre = $nombre
                        WHERE Nit = $nit;
                    ";

                    actualizar.Parameters.AddWithValue("$nit", nit);
                    actualizar.Parameters.AddWithValue("$nombre", nombre);
                    actualizar.ExecuteNonQuery();

                    actualizados++;
                }
            }

            // si todo salio bien, hacemos commit de la transaccion
            transaccion.Commit();

            // devolvemos la respuesta con los contadores de nuevos, actualizados e invalidos
            return Ok(new
            {
                CONTRIBUYENTES_NUEVOS = nuevos,
                CONTRIBUYENTES_ACTUALIZADOS = actualizados,
                NITS_INVALIDOS = invalidos
            });
        }
        catch (SqliteException)
        {
            transaccion.Rollback();

            return StatusCode(500, new
            {
                mensaje = "Ocurrió un error al actualizar el RTU."
            });
        }
    }
}