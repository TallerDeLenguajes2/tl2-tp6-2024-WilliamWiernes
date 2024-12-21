using Microsoft.AspNetCore.Mvc;

public class UsuarioController : Controller
{
    private IUsuarioRepository _usuarioRepository;
    private ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public IActionResult FormularioLogin()
    {
        return View();
    }

    public IActionResult Login(string nombreUsuario, string contraseña)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("FormularioLogin");

            var usuario = _usuarioRepository.ObtenerUsuario(nombreUsuario, contraseña);

            HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
            HttpContext.Session.SetString("Rol", usuario.Rol.ToString());

            _logger.LogInformation("El usuario {NombreUsuario} ingresó correctamente!", usuario.NombreUsuario);

            return RedirectToAction("Listar", "Producto");
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Intento de acceso inválido - Usuario: {nombreUsuario} Clave ingresada: {contraseña}", nombreUsuario, contraseña);
            _logger.LogError(ex.ToString());

            return RedirectToAction("FormularioLogin");
        }
    }

    public IActionResult Logout()
    {
        try
        {
            Response.Cookies.Delete("AuthCookie");
            HttpContext.Session.Clear();

            return RedirectToAction("FormularioLogin");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }
}