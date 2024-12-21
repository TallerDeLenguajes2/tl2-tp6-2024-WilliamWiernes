using Microsoft.AspNetCore.Mvc;

public class ClienteController : Controller
{
    private IClienteRepository _clienteRepository;
    private ILogger<ClienteController> _logger;

    public ClienteController(ILogger<ClienteController> logger, IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            var listaClientes = _clienteRepository.Listar();
            var listaClientesViewModel = new List<ClienteViewModel>();

            foreach (var cliente in listaClientes)
            {
                var clienteViewModel = new ClienteViewModel(cliente.IdCliente, cliente.NombreCliente, cliente.Email, cliente.Telefono);
                listaClientesViewModel.Add(clienteViewModel);
            }

            return View(listaClientesViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }

    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(ClienteViewModel nuevoClienteViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Listar");

            var nuevoCliente = new Cliente(0, nuevoClienteViewModel.NombreCliente, nuevoClienteViewModel.Email, nuevoClienteViewModel.Telefono);
            _clienteRepository.CrearCliente(nuevoCliente);

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Modificar(int idCliente)
    {
        try
        {
            var cliente = _clienteRepository.ObtenerCliente(idCliente);
            var clienteViewModel = new ClienteViewModel(cliente.IdCliente, cliente.NombreCliente, cliente.Email, cliente.Telefono);

            return View(clienteViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult Modificar(ClienteViewModel modClienteViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Listar");

            var modCliente = new Cliente(modClienteViewModel.IdCliente, modClienteViewModel.NombreCliente, modClienteViewModel.Email, modClienteViewModel.Telefono);
            _clienteRepository.ModificarCliente(modCliente.IdCliente, modCliente);

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Eliminar(int idCliente)
    {
        try
        {
            var cliente = _clienteRepository.ObtenerCliente(idCliente);
            var clienteViewModel = new ClienteViewModel(cliente.IdCliente, cliente.NombreCliente, cliente.Email, cliente.Telefono);

            return View(clienteViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult EliminarCliente(int idCliente)
    {
        try
        {
            _clienteRepository.Eliminar(idCliente);

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }
}