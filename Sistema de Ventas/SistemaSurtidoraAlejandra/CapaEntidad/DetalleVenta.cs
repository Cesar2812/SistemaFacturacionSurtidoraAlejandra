namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int id_DetalleVenta { get; set; }
        public Venta obj_venta { get; set; }
        public Producto obj_pro { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Sub_Total { get; set; }
        public string FechaRegistro { get; set; }

    }
}
