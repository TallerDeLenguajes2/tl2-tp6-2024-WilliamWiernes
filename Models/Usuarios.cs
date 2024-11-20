public enum Rol
{
    Administrador = 0,
    Cliente = 1,
    NoLogueado = 2
}

public class Usuario
{
    private int idUsuario;
    private string nombre;
    private string nombreUsuario;
    private string contraseña;
    private Rol rol;

    public Usuario() { }

    public Usuario(int idUsuario, string nombre, string nombreUsuario, string contraseña, Rol rol)
    {
        this.idUsuario = idUsuario;
        this.nombre = nombre;
        this.nombreUsuario = nombreUsuario;
        this.contraseña = contraseña;
        this.rol = rol;
    }

    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public string Contraseña { get => contraseña; set => contraseña = value; }
    public Rol Rol { get => rol; set => rol = value; }
}