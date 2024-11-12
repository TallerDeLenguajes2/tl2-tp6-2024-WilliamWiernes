using System.ComponentModel.DataAnnotations;

public class ClienteViewModel
{
    private int idCliente;
    private string nombreCliente;
    private string email;
    private string telefono;

    public ClienteViewModel() { }

    public ClienteViewModel(int idCliente, string nombreCliente, string email, string telefono)
    {
        this.idCliente = idCliente;
        this.nombreCliente = nombreCliente;
        this.email = email;
        this.telefono = telefono;
    }

    public int IdCliente { get => idCliente; set => idCliente = value; }
    [Required(ErrorMessage = "Debe ingresar un nombre!")]
    public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
    [StringLength(50, ErrorMessage = "Debe ingresar un email válido!")]
    public string Email { get => email; set => email = value; }
    [StringLength(20,ErrorMessage = "Debe ingresar un teléfono válido!")]
    public string Telefono { get => telefono; set => telefono = value; }
}