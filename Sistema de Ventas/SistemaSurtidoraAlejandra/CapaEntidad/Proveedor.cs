namespace CapaEntidad
{
    public class Proveedor
    {
        public int id_Proveedor { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
