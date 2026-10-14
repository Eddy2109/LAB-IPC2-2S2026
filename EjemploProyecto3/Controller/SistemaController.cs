using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using EjemploProyecto3.Data;

namespace EjemploProyecto3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SistemaController : ControllerBase
{
    private readonly ConexionBD _conexionBD;

    public SistemaController(ConexionBD conexionBD)
    {
        _conexionBD = conexionBD;
    }

    [HttpGet("estado")]
    public IActionResult ObtenerEstado()
    {
        try
        {
            using var conexion = _conexionBD.ObtenerConexion();

            var tablas = new List<string>();

            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                SELECT name
                FROM sqlite_master
                WHERE type = 'table'
                  AND name NOT LIKE 'sqlite_%'
                ORDER BY name;
            ";

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                tablas.Add(lector.GetString(0));
            }

            return Ok(new
            {
                mensaje = "Conexión a SQLite exitosa.",
                baseDatos = conexion.DataSource,
                tablas = tablas
            });
        }
        catch (SqliteException ex)
        {
            return StatusCode(500, new
            {
                mensaje = "Error al consultar la base de datos SQLite.",
                detalle = ex.Message
            });
        }
    }
}