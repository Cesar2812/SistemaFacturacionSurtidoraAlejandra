namespace CapaEntidad
{
    public class Producto
    {
        public int id_Producto { get; set; }
        public string Codigo { get; set; }
        public string Nombre_Producto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public Categoria obj_categoria { get; set; }
        public Marca obj_marca { get; set; }
        public int Stock { get; set; }
        public string FechaVencimiento { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

    }
}
