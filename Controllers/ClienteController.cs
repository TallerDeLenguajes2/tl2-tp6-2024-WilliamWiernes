using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_WilliamWiernes.Controllers;

public class ClienteController : Controller
{
    private ClienteRepository clienteRepository;
    private ILogger<ClienteController> _logger;

    public ClienteController(ILogger<ClienteController> logger)
    {
        clienteRepository = new ClienteRepository();
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        return View(clienteRepository.Listar());
    }

    [HttpGet]
    public ActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Crear(Cliente nuevoCliente)
    {
        clienteRepository.CrearCliente(nuevoCliente);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Modificar(int idCliente)
    {
        return View(clienteRepository.ObtenerCliente(idCliente));
    }

    [HttpPost]
    public ActionResult Modificar(Cliente modCliente)
    {
        clienteRepository.ModificarCliente(modCliente.IdCliente, modCliente);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Eliminar(int idCliente)
    {
        return View(clienteRepository.ObtenerCliente(idCliente));
    }

    [HttpPost]
    public ActionResult EliminarCliente(int idCliente)
    {
        clienteRepository.Eliminar(idCliente);
        return RedirectToAction("Listar");
    }
}