using Microsoft.Data.Sqlite;

public class PresupuestoRepository
{
    private readonly string cadenaConexion = "Data Source=DataBase/Tienda.db;Cache=Shared";

    public void CrearPresupuesto(Presupuesto nuevoPresupuesto)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) 
                            VALUES (@nombreDestinatario, @fechaCreacion)";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@nombreDestinatario", nuevoPresupuesto.NombreDestinatario));
            comando.Parameters.Add(new SqliteParameter("@fechaCreacion", nuevoPresupuesto.FechaCreacion));
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }

    public List<Presupuesto> ListarPresupuestos()
    {
        var listaPresupuestos = new List<Presupuesto>();

        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = "SELECT * FROM Presupuestos";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            using (var lectorDatos = comando.ExecuteReader())
            {
                while (lectorDatos.Read())
                {
                    var presupuesto = new Presupuesto(Convert.ToInt32(lectorDatos["idPresupuesto"]),
                                                    Convert.ToString(lectorDatos["NombreDestinatario"]),
                                                    Convert.ToString(lectorDatos["FechaCreacion"]));

                    listaPresupuestos.Add(presupuesto);
                }
            }

            conexion.Close();
        }

        return listaPresupuestos;
    }

    public Presupuesto ObtenerDetallesPresupuesto(int idPresupuesto)
    {
        Presupuesto presupuesto = null;

        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"
            SELECT 
                Presupuestos.idPresupuesto, 
                Presupuestos.nombreDestinatario, 
                Presupuestos.fechaCreacion, 
                Productos.idProducto, 
                Productos.Descripcion, 
                Productos.Precio, 
                PresupuestosDetalle.Cantidad
            FROM 
                Presupuestos
            INNER JOIN 
                PresupuestosDetalle ON Presupuestos.idPresupuesto = PresupuestosDetalle.idPresupuesto
            INNER JOIN 
                Productos ON PresupuestosDetalle.idProducto = Productos.idProducto
            WHERE 
                Presupuestos.idPresupuesto = @idPresupuesto";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);

            using (var lectorDatos = comando.ExecuteReader())
            {
                while (lectorDatos.Read())
                {
                    if (presupuesto == null)
                    {
                        presupuesto = new Presupuesto(
                            Convert.ToInt32(lectorDatos["idPresupuesto"]),
                            Convert.ToString(lectorDatos["nombreDestinatario"]),
                            Convert.ToString(lectorDatos["fechaCreacion"])
                        );
                    }

                    var producto = new Producto(Convert.ToInt32(lectorDatos["idProducto"]),
                                                Convert.ToString(lectorDatos["Descripcion"]),
                                                Convert.ToInt32(lectorDatos["Precio"]));

                    var detalle = new PresupuestoDetalle(producto, Convert.ToInt32(lectorDatos["Cantidad"]));

                    presupuesto.ListaDetalles.Add(detalle);
                }
            }

            conexion.Close();
        }

        return presupuesto;
    }

    public void AgregarPresupuestoDetalle(int idPresupuesto, PresupuestoDetalle nuevoPresupuestoDetalle)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) 
                            VALUES (@idPresupuesto, @idProducto, @cantidad)";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
            comando.Parameters.AddWithValue("@idProducto", nuevoPresupuestoDetalle.Producto.IdProducto);
            comando.Parameters.AddWithValue("@cantidad", nuevoPresupuestoDetalle.Cantidad);

            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }

    public void EliminarPresupuesto(int id)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"DELETE FROM Presupuestos 
                            WHERE idPresupuesto = (@id)";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }
}