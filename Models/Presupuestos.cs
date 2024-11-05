public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private string fechaCreacion;
    private List<PresupuestoDetalle> listaDetalles;

    public Presupuesto() { }

    public Presupuesto(int idPresupuesto, string nombreDestinatario, string fechaCreacion, List<PresupuestoDetalle> listaDetalles)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.fechaCreacion = fechaCreacion;
        this.listaDetalles = listaDetalles;
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
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