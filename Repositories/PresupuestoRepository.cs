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
                    var presupuesto = ObtenerDetallesPresupuesto(Convert.ToInt32(lectorDatos["idPresupuesto"]));

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
            var consulta = @"SELECT 
                                Presupuestos.idPresupuesto, 
                                Presupuestos.nombreDestinatario, 
                                Presupuestos.fechaCreacion, 
                                Productos.idProducto, 
                                Productos.Descripcion, 
                                Productos.Precio, 
                                PresupuestosDetalle.Cantidad
                            FROM 
                                Presupuestos
                            LEFT JOIN 
                                PresupuestosDetalle ON Presupuestos.idPresupuesto = PresupuestosDetalle.idPresupuesto
                            LEFT JOIN 
                                Productos ON PresupuestosDetalle.idProducto = Productos.idProducto
                            WHERE 
                                Presupuestos.idPresupuesto = @idPresupuesto
                            ";

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
                            Convert.ToString(lectorDatos["fechaCreacion"]),
                            new List<PresupuestoDetalle>()
                        );
                    }

                    if (!lectorDatos.IsDBNull(lectorDatos.GetOrdinal("idProducto")))
                    {
                        var producto = new Producto(
                            Convert.ToInt32(lectorDatos["idProducto"]),
                            Convert.ToString(lectorDatos["Descripcion"]),
                            Convert.ToInt32(lectorDatos["Precio"])
                        );

                        var detalle = new PresupuestoDetalle(producto, Convert.ToInt32(lectorDatos["Cantidad"]));
                        presupuesto.ListaDetalles.Add(detalle);
                    }
                }
            }

            conexion.Close();
        }

        if (presupuesto != null && presupuesto.ListaDetalles == null)
        {
            presupuesto.ListaDetalles = new List<PresupuestoDetalle>();
        }

        return presupuesto;
    }

    public void AgregarPresupuestoDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) 
                            VALUES (@idPresupuesto, @idProducto, @cantidad)";

            conexion.Open();

            using (var comando = new SqliteCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                comando.Parameters.AddWithValue("@idProducto", idProducto);
                comando.Parameters.AddWithValue("@cantidad", cantidad);

                comando.ExecuteNonQuery();
            }

            conexion.Close();
        }
    }

    public void EliminarPresupuesto(int idPresupuesto)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            conexion.Open();

            var eliminarDetallesCmd = conexion.CreateCommand();
            eliminarDetallesCmd.CommandText = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @idPresupuesto";
            eliminarDetallesCmd.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
            eliminarDetallesCmd.ExecuteNonQuery();

            var eliminarPresupuestoCmd = conexion.CreateCommand();
            eliminarPresupuestoCmd.CommandText = "DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";
            eliminarPresupuestoCmd.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
            eliminarPresupuestoCmd.ExecuteNonQuery();

            conexion.Close();
        }
    }


    public void ModificarPresupuesto(int id, Presupuesto modPresupuesto)
    {
        using (var conexion = new SqliteConnection(cadenaConexion))
        {
            var consulta = @"UPDATE Presupuestos
                            SET NombreDestinatario = @nombreDestinatario, FechaCreacion = @fechaCreacion
                            WHERE idPresupuesto = @id";

            conexion.Open();

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.Parameters.Add(new SqliteParameter("@nombreDestinatario", modPresupuesto.NombreDestinatario));
            comando.Parameters.Add(new SqliteParameter("@fechaCreacion", modPresupuesto.FechaCreacion));
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }
}