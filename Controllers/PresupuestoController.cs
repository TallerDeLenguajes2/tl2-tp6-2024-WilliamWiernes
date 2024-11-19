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
        return View(_presupuestoRepository.ListarPresupuestos());
    }

    [HttpGet]
    public IActionResult Crear()
    {
        var listaClientesViewModel = new PresupuestoViewModel(_clienteRepository.Listar());

        return View(listaClientesViewModel);
    }

    [HttpPost]
    public IActionResult Crear(int idCliente)
    {
        var cliente = _clienteRepository.ObtenerCliente(idCliente);
        var listaDetalles = new List<PresupuestoDetalle>();
        var nuevoPresupuesto = new Presupuesto(1, cliente, DateTime.Now.ToString("yyyy-MM-dd"), listaDetalles);

        _presupuestoRepository.CrearPresupuesto(nuevoPresupuesto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Modificar(int idPresupuesto)
    {
        return View(_presupuestoRepository.ObtenerDetallesPresupuesto(idPresupuesto));
    }

    [HttpPost]
    public IActionResult Modificar(Presupuesto modPresupuesto)
    {
        _presupuestoRepository.ModificarPresupuesto(modPresupuesto.IdPresupuesto, modPresupuesto);
        return RedirectToAction("Listar");
    }
    
    [HttpGet]
    public IActionResult ModificarAgregarProducto(int idPresupuesto)
    {
        var listaProductosViewModel = new ProductoAltaViewModel(idPresupuesto, _productoRepository.ListarProductos());

        return View(listaProductosViewModel);
    }

    [HttpPost]
    public IActionResult ModificarAgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        _presupuestoRepository.AgregarPresupuestoDetalle(idPresupuesto, idProducto, cantidad);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Eliminar(int idPresupuesto)
    {
        return View(_presupuestoRepository.ObtenerDetallesPresupuesto(idPresupuesto));
    }

    [HttpPost]
    public IActionResult EliminarPresupuesto(int idPresupuesto)
    {
        _presupuestoRepository.EliminarPresupuesto(idPresupuesto);
        return RedirectToAction("Listar");
    }
}