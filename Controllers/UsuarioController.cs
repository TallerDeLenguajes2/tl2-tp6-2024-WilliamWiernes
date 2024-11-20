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
        if(!ModelState.IsValid) return RedirectToAction("FormularioLogin");

        var usuario = _usuarioRepository.ObtenerUsuario(nombreUsuario, contraseña);
        if(usuario == null) return RedirectToAction("FormularioLogin");

        HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
        HttpContext.Session.SetString("Rol", usuario.Rol.ToString());

        return RedirectToAction("Listar", "Producto");
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthCookie");
        HttpContext.Session.Clear();
        
        return RedirectToAction("FormularioLogin");
    }
}