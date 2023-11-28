namespace CapaEntidad
{
    public class DetalleCompra
    {
        public int id_DetalleCompra { get; set; }
        public Compra o_idCompra { get; set; }

        public Producto o_idProducto { get; set; }
        public decimal PrecioDeCompra { get; set; }
        public decimal PrecioDeVenta { get; set; }
        public string FechaVencimiento { get; set; }
        public int Cantidad { get; set; }
        public decimal Montototal { get; set; }
        public string FechaCreacion { get; set; }

    }
}
