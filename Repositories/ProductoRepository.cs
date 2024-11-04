using Microsoft.Data.Sqlite;

public class ProductoRepository
{
    private readonly string cadenaConexion = "Data Source=DataBase/Tienda.db;Cache=Shared";

    public void CrearProducto(Producto nuevoProducto)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"INSERT INTO Productos (Descripcion, Precio) 
                            VALUES (@descripcion, @precio)";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@descripcion", nuevoProducto.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", nuevoProducto.Precio));
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }

    public void ModificarProducto(int id, Producto modProducto)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"UPDATE Productos
                            SET Descripcion = @descripcion, Precio = @precio
                            WHERE idProducto = @id";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.Parameters.Add(new SqliteParameter("@descripcion", modProducto.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", modProducto.Precio));
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }

    public List<Producto> ListarProductos()
    {
        var listaProductos = new List<Producto>();

        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = "SELECT * FROM Productos";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            using (var lectorDatos = comando.ExecuteReader())
            {
                while (lectorDatos.Read())
                {
                    var producto = new Producto(Convert.ToInt32(lectorDatos["idProducto"]),
                                                Convert.ToString(lectorDatos["Descripcion"]),
                                                Convert.ToInt32(lectorDatos["Precio"]));

                    listaProductos.Add(producto);
                }
            }

            conexion.Close();
        }

        return listaProductos;
    }

    public Producto ObtenerDetalles(int id)
    {
        Producto producto;
        using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"SELECT * FROM Productos 
                            WHERE idProducto = @id";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader lectorDatos = comando.ExecuteReader())
            {
                lectorDatos.Read();
                producto = new Producto(Convert.ToInt32(lectorDatos[0]),
                                        Convert.ToString(lectorDatos[1]),
                                        Convert.ToInt32(lectorDatos[2]));
            }

            conexion.Close();
        }

        return producto;
    }

    public void EliminarProducto(int id)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"DELETE FROM Productos 
                            WHERE idProducto = (@id)";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }
}