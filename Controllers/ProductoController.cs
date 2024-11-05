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
        return View(productoRepository.ListarProductos());
    }

    [HttpGet]
    public ActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Crear(Producto nuevoProducto)
    {
        productoRepository.CrearProducto(nuevoProducto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Modificar(int idProducto)
    {
        return View(productoRepository.ObtenerDetalles(idProducto));
    }

    [HttpPost]
    public ActionResult Modificar(Producto modProducto)
    {
        productoRepository.ModificarProducto(modProducto.IdProducto, modProducto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Eliminar(int idProducto)
    {
        return View(productoRepository.ObtenerDetalles(idProducto));
    }

    [HttpPost]
    public ActionResult EliminarProducto(int idProducto)
    {
        productoRepository.EliminarProducto(idProducto);
        return RedirectToAction("Listar");
    }
}