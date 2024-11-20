public interface IUsuarioRepository
{
    public UsuarioViewModel ObtenerUsuario(string nombreUsuario, string contrasena);
}