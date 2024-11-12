public class PresupuestoViewModel
{
    private List<Cliente> listaClientes;

    public PresupuestoViewModel(List<Cliente> listaClientes)
    {
        this.listaClientes = listaClientes;
    }

    public List<Cliente> ListaClientes { get => listaClientes; set => listaClientes = value; }
}