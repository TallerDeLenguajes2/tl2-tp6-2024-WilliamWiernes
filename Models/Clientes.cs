public class Cliente
{
    private int idCliente;
    private string nombreCliente;
    private string email;
    private string telefono;

    public Cliente() { }

    public Cliente(int idCliente, string nombreCliente, string email, string telefono)
    {
        this.IdCliente = idCliente;
        this.NombreCliente = nombreCliente;
        this.Email = email;
        this.Telefono = telefono;
    }

    public int IdCliente { get => idCliente; set => idCliente = value; }
    public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
    public string Email { get => email; set => email = value; }
    public string Telefono { get => telefono; set => telefono = value; }
}