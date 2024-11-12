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
        var listaClientes = clienteRepository.Listar();
        var listaClientesViewModel = new List<ClienteViewModel>();

        foreach (var cliente in listaClientes)
        {
            var clienteViewModel = new ClienteViewModel(cliente.IdCliente, cliente.NombreCliente, cliente.Email, cliente.Telefono);
            listaClientesViewModel.Add(clienteViewModel);
        }

        return View(listaClientesViewModel);
    }

    [HttpGet]
    public ActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Crear(ClienteViewModel nuevoClienteViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Listar");

        var nuevoCliente = new Cliente(0, nuevoClienteViewModel.NombreCliente, nuevoClienteViewModel.Email, nuevoClienteViewModel.Telefono);
        clienteRepository.CrearCliente(nuevoCliente);

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Modificar(int idCliente)
    {
        var cliente = clienteRepository.ObtenerCliente(idCliente);
        var clienteViewModel = new ClienteViewModel(cliente.IdCliente, cliente.NombreCliente, cliente.Email, cliente.Telefono);

        return View(clienteViewModel);
    }

    [HttpPost]
    public ActionResult Modificar(ClienteViewModel modClienteViewModel)
    {
        if(!ModelState.IsValid) return RedirectToAction("Listar");

        var modCliente = new Cliente(modClienteViewModel.IdCliente, modClienteViewModel.NombreCliente, modClienteViewModel.Email, modClienteViewModel.Telefono);
        clienteRepository.ModificarCliente(modCliente.IdCliente, modCliente);

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Eliminar(int idCliente)
    {
        var cliente = clienteRepository.ObtenerCliente(idCliente);
        var clienteViewModel = new ClienteViewModel(cliente.IdCliente, cliente.NombreCliente, cliente.Email, cliente.Telefono);

        return View(clienteViewModel);
    }

    [HttpPost]
    public ActionResult EliminarCliente(int idCliente)
    {
        clienteRepository.Eliminar(idCliente);

        return RedirectToAction("Listar");
    }
}