using System.ComponentModel.DataAnnotations;

public class ProductoViewModel
{
    private int idProducto;
    private string descripcion;
    private int precio;

    public ProductoViewModel() { }

    public ProductoViewModel(int idProducto, string descripcion, int precio)
    {
        this.idProducto = idProducto;
        this.descripcion = descripcion;
        this.precio = precio;
    }

    public int IdProducto { get => idProducto; set => idProducto = value; }
    [StringLength(250, ErrorMessage = "La longitud máxima de la descripción es de 250 caracteres!")]
    public string Descripcion { get => descripcion; set => descripcion = value; }
    [Required(ErrorMessage = "Debe ingresar un precio!")]
    [Range(0, int.MaxValue, ErrorMessage = "El precio debe ser positivo!")]
    public int Precio { get => precio; set => precio = value; }
}