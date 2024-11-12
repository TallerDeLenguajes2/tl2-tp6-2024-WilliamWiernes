using Microsoft.Data.Sqlite;

public class ClienteRepository
{
    private readonly string cadenaConexion = "Data Source=DataBase/Tienda.db;Cache=Shared";

    public void CrearCliente(Cliente nuevoCliente)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"INSERT INTO Clientes (Nombre, Email, Telefono) 
                            VALUES (@nombre, @email, @telefono)";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@nombre", nuevoCliente.NombreCliente));
            comando.Parameters.Add(new SqliteParameter("@email", nuevoCliente.Email));
            comando.Parameters.Add(new SqliteParameter("@telefono", nuevoCliente.Telefono));
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }

    public void ModificarCliente(int id, Cliente modCliente)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"UPDATE Clientes
                            SET Nombre = @nombre, Email = @email, Telefono = @telefono
                            WHERE idCliente = @id";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.Parameters.Add(new SqliteParameter("@nombre", modCliente.NombreCliente));
            comando.Parameters.Add(new SqliteParameter("@email", modCliente.Email));
            comando.Parameters.Add(new SqliteParameter("@telefono", modCliente.Telefono));
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }

    public List<Cliente> Listar()
    {
        var listaClientes = new List<Cliente>();

        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"SELECT * FROM Clientes";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            using (var lectorDatos = comando.ExecuteReader())
            {
                while (lectorDatos.Read())
                {
                    var cliente = new Cliente(Convert.ToInt32(lectorDatos["idCliente"]),
                        Convert.ToString(lectorDatos["Nombre"]),
                        Convert.ToString(lectorDatos["Email"]),
                        Convert.ToString(lectorDatos["Telefono"]));

                    listaClientes.Add(cliente);
                }
            }

            conexion.Close();
        }

        return listaClientes;
    }

    public Cliente ObtenerCliente(int id)
    {
        Cliente cliente;
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"SELECT * FROM Clientes
                            WHERE idCliente = @id";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@id", id));
            using (var lectorDatos = comando.ExecuteReader())
            {
                lectorDatos.Read();
                cliente = new Cliente(Convert.ToInt32(lectorDatos["idCliente"]),
                    Convert.ToString(lectorDatos["Nombre"]),
                    Convert.ToString(lectorDatos["Email"]),
                    Convert.ToString(lectorDatos["Telefono"]));
            }

            conexion.Close();
        }

        return cliente;
    }

    public void Eliminar(int id)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"DELETE FROM Clientes
                            WHERE idCliente = @id";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }
}