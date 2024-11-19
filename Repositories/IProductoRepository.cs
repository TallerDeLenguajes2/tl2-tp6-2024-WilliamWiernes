public interface IProductoRepository
{
    void CrearProducto(Producto nuevoProducto);
    void ModificarProducto(int id, Producto modProducto);
    List<Producto> ListarProductos();
    Producto ObtenerDetalles(int id);
    void EliminarProducto(int id);
}