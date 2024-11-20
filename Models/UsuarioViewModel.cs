using System.ComponentModel.DataAnnotations;

public class UsuarioViewModel
{
    private int idUsuario;
    private string nombre;
    private string nombreUsuario;
    private string contraseña;
    private Rol rol;

    public UsuarioViewModel() { }

    public UsuarioViewModel(int idUsuario, string nombre, string nombreUsuario, string contraseña, Rol rol)
    {
        this.idUsuario = idUsuario;
        this.nombre = nombre;
        this.nombreUsuario = nombreUsuario;
        this.contraseña = contraseña;
        this.rol = rol;
    }

    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    [Required(ErrorMessage = "Debe ingresar el Nombre!")]
    [StringLength(20, MinimumLength =3, ErrorMessage = "El Nombre debe tener entre 3 y 20 caracteres!")]
    public string Nombre { get => nombre; set => nombre = value; }
    [Required(ErrorMessage = "Debe ingresar el Nombre de Usuario!")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "El Nombre de Usuario debe tener entre 3 y 20 caracteres!")]
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    [Required(ErrorMessage = "Debe ingresar la Contraseña!")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "La Contraseña debe tener entre 3 y 20 caracteres!")]
    public string Contraseña { get => contraseña; set => contraseña = value; }
    [Required(ErrorMessage = "Debe ingresar el Rol!")]
    public Rol Rol { get => rol; set => rol = value; }
}