using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_WilliamWiernes.Controllers;

public class ProductoController : Controller
{
    private ProductoRepository productoRepository;
    private ILogger<ProductoController> _logger;

    public ProductoController(ILogger<ProductoController> logger)
    {
        productoRepository = new ProductoRepository();
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        var listaProductos = productoRepository.ListarProductos();
        var listaProductosViewModel = new List<ProductoViewModel>();

        foreach(var producto in listaProductos)
        {
            var productoViewModel = new ProductoViewModel(producto.IdProducto, producto.Descripcion, producto.Precio);
            listaProductosViewModel.Add(productoViewModel);
        }
        
        return View(listaProductosViewModel);
    }

    [HttpGet]
    public ActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Crear(ProductoViewModel nuevoProductoViewModel)
    {
        var nuevoProducto = new Producto(0, nuevoProductoViewModel.Descripcion, nuevoProductoViewModel.Precio);
        productoRepository.CrearProducto(nuevoProducto);

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Modificar(int idProducto)
    {
        var producto = productoRepository.ObtenerDetalles(idProducto); 
        var productoViewModel = new ProductoViewModel(producto.IdProducto, producto.Descripcion, producto.Precio);
        
        return View(productoViewModel);
    }

    [HttpPost]
    public ActionResult Modificar(ProductoViewModel modProductoViewModel)
    {
        var modProducto = new Producto(modProductoViewModel.IdProducto, modProductoViewModel.Descripcion, modProductoViewModel.Precio);
        productoRepository.ModificarProducto(modProducto.IdProducto, modProducto);

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Eliminar(int idProducto)
    {
        var producto = productoRepository.ObtenerDetalles(idProducto);
        var productoViewModel = new ProductoViewModel(producto.IdProducto, producto.Descripcion, producto.Precio);

        return View(productoViewModel);
    }

    [HttpPost]
    public ActionResult EliminarProducto(int idProducto)
    {
        productoRepository.EliminarProducto(idProducto);

        return RedirectToAction("Listar");
    }
}