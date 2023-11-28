namespace CapaEntidad
{
    public class Cliente
    {
        public int id_Cliente { get; set; }
        public string Cedula { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

    }
}
