using Microsoft.Data.Sqlite;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string cadenaConexion = "Data Source=DataBase/Tienda.db;Cache=Shared";

    public UsuarioViewModel ObtenerUsuario(string nombreUsuario, string contraseña)
    {
        UsuarioViewModel usuario = null;

        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"SELECT * FROM Usuarios 
                            WHERE NombreUsuario = @nombreUsuario 
                            AND Contraseña = @contraseña";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
            comando.Parameters.AddWithValue("@contraseña", contraseña);

            using (var lectorDatos = comando.ExecuteReader())
            {
                lectorDatos.Read();
                if (!lectorDatos.HasRows) return null;

                usuario = new UsuarioViewModel(Convert.ToInt32(lectorDatos["idUsuario"]),
                                Convert.ToString(lectorDatos["Nombre"]),
                                Convert.ToString(lectorDatos["NombreUsuario"]),
                                Convert.ToString(lectorDatos["Contraseña"]),
                                (Rol)Convert.ToInt32(lectorDatos["Rol"]));

            }

            conexion.Close();
        }

        if (usuario == null)
            throw new Exception("El usuario no fue encontrado!");

        return usuario;
    }
}