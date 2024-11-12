public class ProductoAltaViewModel
{
    private int idPresupuesto;
    private List<Producto> listaProductos;

    public ProductoAltaViewModel(int idPresupuesto, List<Producto> listaProductos)
    {
        this.idPresupuesto = idPresupuesto;
        this.listaProductos = listaProductos;
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public List<Producto> ListaProductos { get => listaProductos; set => listaProductos = value; }
}