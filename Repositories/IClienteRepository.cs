public interface IClienteRepository
{
    void CrearCliente(Cliente cliente);
    void ModificarCliente(int id, Cliente cliente);
    List<Cliente> Listar();
    Cliente ObtenerCliente(int id);
    void Eliminar(int id);
}