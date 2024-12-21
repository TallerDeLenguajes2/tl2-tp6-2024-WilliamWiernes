using Microsoft.Data.Sqlite;

public class ProductoRepository : IProductoRepository
{
    private readonly string _cadenaConexion;

    public ProductoRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    public void CrearProducto(Producto nuevoProducto)
    {
        if (nuevoProducto == null)
            throw new Exception("El producto no puede ser nulo!");

        using (var conexion = new SqliteConnection(_cadenaConexion))
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
        if (id < 0)
            throw new Exception("El id no puede ser negativo!");

        if (modProducto == null)
            throw new Exception("El producto no puede ser nulo!");

        using (var conexion = new SqliteConnection(_cadenaConexion))
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

        using (var conexion = new SqliteConnection(_cadenaConexion))
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

        if (listaProductos.Count == 0)
            throw new Exception("No se encontraron productos!");

        return listaProductos;
    }

    public Producto ObtenerDetalles(int id)
    {
        if (id < 0)
            throw new Exception("El id no puede ser negativo!");

        Producto producto = null;
        using (SqliteConnection conexion = new SqliteConnection(_cadenaConexion))
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

        if (producto == null)
            throw new Exception($"El producto con id {id} no fue encontrado!");

        return producto;
    }

    public void EliminarProducto(int id)
    {
        if (id < 0)
            throw new Exception("El id no puede ser negativo!");

        using (var conexion = new SqliteConnection(_cadenaConexion))
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