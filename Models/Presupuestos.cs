public class Presupuesto
{
    private int idPresupuesto;
    private Cliente cliente;
    private string fechaCreacion;
    private List<PresupuestoDetalle> listaDetalles;

    public Presupuesto() { }

    public Presupuesto(int idPresupuesto, Cliente cliente, string fechaCreacion, List<PresupuestoDetalle> listaDetalles)
    {
        this.idPresupuesto = idPresupuesto;
        this.Cliente = cliente;
        this.fechaCreacion = fechaCreacion;
        this.listaDetalles = listaDetalles;
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestoDetalle> ListaDetalles { get => listaDetalles; set => listaDetalles = value; }

    public int MontoPresupuesto()
    {
        return listaDetalles.Sum(detalle => detalle.Producto.Precio * detalle.Cantidad);
    }

    public int MontoPresupuestoConIva()
    {
        return (int)(MontoPresupuesto() * 0.21);
    }

    public int CantidadProductos()
    {
        return listaDetalles.Sum(detalle => detalle.Cantidad);
    }
}