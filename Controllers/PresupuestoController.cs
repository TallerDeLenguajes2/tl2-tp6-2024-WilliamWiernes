using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_WilliamWiernes.Models;

public class PresupuestoController : Controller
{
    private PresupuestoRepository presupuestoRepository;
    private ILogger<PresupuestoController> _logger;

    public PresupuestoController(ILogger<PresupuestoController> logger)
    {
        presupuestoRepository = new PresupuestoRepository();
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        return View(presupuestoRepository.ListarPresupuestos());
    }

    [HttpGet]
    public ActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Crear(Presupuesto nuevoPresupuesto)
    {
        presupuestoRepository.CrearPresupuesto(nuevoPresupuesto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Modificar(int idPresupuesto)
    {
        return View(presupuestoRepository.ObtenerDetallesPresupuesto(idPresupuesto));
    }

    [HttpPost]
    public ActionResult Modificar(Presupuesto modPresupuesto)
    {
        presupuestoRepository.ModificarPresupuesto(modPresupuesto.IdPresupuesto, modPresupuesto);
        return RedirectToAction("Listar");
    }
    
    [HttpGet]
    public ActionResult ModificarAgregarProducto(int idPresupuesto)
    {
        return View(idPresupuesto);
    }

    [HttpPost]
    public ActionResult ModificarAgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        presupuestoRepository.AgregarPresupuestoDetalle(idPresupuesto, idProducto, cantidad);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Eliminar(int idPresupuesto)
    {
        return View(presupuestoRepository.ObtenerDetallesPresupuesto(idPresupuesto));
    }

    [HttpPost]
    public ActionResult EliminarPresupuesto(int idPresupuesto)
    {
        presupuestoRepository.EliminarPresupuesto(idPresupuesto);
        return RedirectToAction("Listar");
    }
}