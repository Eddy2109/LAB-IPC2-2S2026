using Microsoft.Data.Sqlite;

namespace EjemploProyecto3.Data;

public class ConexionBD
{
    private readonly string _cadenaConexion;

    public ConexionBD(IConfiguration configuration)
    {
        _cadenaConexion = configuration.GetConnectionString("SQLite")
            ?? throw new InvalidOperationException(
                "No se encontró la cadena de conexión SQLite."
            );
    }

    public SqliteConnection ObtenerConexion()
    {
        var conexion = new SqliteConnection(_cadenaConexion);
        conexion.Open();

        using var comando = conexion.CreateCommand();
        comando.CommandText = "PRAGMA foreign_keys = ON;";
        comando.ExecuteNonQuery();

        return conexion;
    }
}