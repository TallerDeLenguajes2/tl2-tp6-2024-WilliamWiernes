using Microsoft.AspNetCore.Mvc;

public class PresupuestoController : Controller
{
    private IPresupuestoRepository _presupuestoRepository;
    private IClienteRepository _clienteRepository;
    private IProductoRepository _productoRepository;
    private ILogger<PresupuestoController> _logger;

    public PresupuestoController(ILogger<PresupuestoController> logger, IPresupuestoRepository presupuestoRepository, IClienteRepository clienteRepository, IProductoRepository productoRepository)
    {
        _presupuestoRepository = presupuestoRepository;
        _clienteRepository = clienteRepository;
        _productoRepository = productoRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return View(_presupuestoRepository.ListarPresupuestos());
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
        try
        {
            var listaClientesViewModel = new PresupuestoViewModel(_clienteRepository.Listar());

            return View(listaClientesViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult Crear(int idCliente)
    {
        try
        {
            var cliente = _clienteRepository.ObtenerCliente(idCliente);
            var listaDetalles = new List<PresupuestoDetalle>();
            var nuevoPresupuesto = new Presupuesto(1, cliente, DateTime.Now.ToString("yyyy-MM-dd"), listaDetalles);

            _presupuestoRepository.CrearPresupuesto(nuevoPresupuesto);
            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Modificar(int idPresupuesto)
    {
        try
        {
            return View(_presupuestoRepository.ObtenerDetallesPresupuesto(idPresupuesto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult Modificar(Presupuesto modPresupuesto)
    {
        try
        {
            _presupuestoRepository.ModificarPresupuesto(modPresupuesto.IdPresupuesto, modPresupuesto);
            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult ModificarAgregarProducto(int idPresupuesto)
    {
        try
        {
            var listaProductosViewModel = new ProductoAltaViewModel(idPresupuesto, _productoRepository.ListarProductos());

            return View(listaProductosViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarAgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        try
        {
            _presupuestoRepository.AgregarPresupuestoDetalle(idPresupuesto, idProducto, cantidad);
            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Eliminar(int idPresupuesto)
    {
        try
        {
            return View(_presupuestoRepository.ObtenerDetallesPresupuesto(idPresupuesto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult EliminarPresupuesto(int idPresupuesto)
    {
        try
        {
            _presupuestoRepository.EliminarPresupuesto(idPresupuesto);
            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            return View("Error");
        }
    }
}