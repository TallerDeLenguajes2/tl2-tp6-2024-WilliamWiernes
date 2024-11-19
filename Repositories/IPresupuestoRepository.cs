public interface IPresupuestoRepository
{
    void CrearPresupuesto(Presupuesto nuevoPresupuesto);
    List<Presupuesto> ListarPresupuestos();
    Presupuesto ObtenerDetallesPresupuesto(int idPresupuesto);
    void AgregarPresupuestoDetalle(int idPresupuesto, int idProducto, int cantidad);
    void EliminarPresupuesto(int idPresupuesto);
    void ModificarPresupuesto(int idPresupuesto, Presupuesto modPresupuesto);
}