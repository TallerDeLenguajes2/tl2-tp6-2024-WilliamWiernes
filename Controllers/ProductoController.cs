using Microsoft.AspNetCore.Mvc;

public class ProductoController : Controller
{
    private IProductoRepository _productoRepository;
    private ILogger<ProductoController> _logger;

    public ProductoController(ILogger<ProductoController> logger, IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            var listaProductos = _productoRepository.ListarProductos();
            var listaProductosViewModel = new List<ProductoViewModel>();

            foreach (var producto in listaProductos)
            {
                var productoViewModel = new ProductoViewModel(producto.IdProducto, producto.Descripcion, producto.Precio);
                listaProductosViewModel.Add(productoViewModel);
            }

            return View(listaProductosViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(ProductoViewModel nuevoProductoViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Listar");

            var nuevoProducto = new Producto(0, nuevoProductoViewModel.Descripcion, nuevoProductoViewModel.Precio);
            _productoRepository.CrearProducto(nuevoProducto);

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Modificar(int idProducto)
    {
        try
        {
            var producto = _productoRepository.ObtenerDetalles(idProducto);
            var productoViewModel = new ProductoViewModel(producto.IdProducto, producto.Descripcion, producto.Precio);

            return View(productoViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult Modificar(ProductoViewModel modProductoViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Listar");

            var modProducto = new Producto(modProductoViewModel.IdProducto, modProductoViewModel.Descripcion, modProductoViewModel.Precio);
            _productoRepository.ModificarProducto(modProducto.IdProducto, modProducto);

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Eliminar(int idProducto)
    {
        try
        {
            var producto = _productoRepository.ObtenerDetalles(idProducto);
            var productoViewModel = new ProductoViewModel(producto.IdProducto, producto.Descripcion, producto.Precio);

            return View(productoViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult EliminarProducto(int idProducto)
    {
        try
        {
            _productoRepository.EliminarProducto(idProducto);

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return View("Error");
        }
    }
}