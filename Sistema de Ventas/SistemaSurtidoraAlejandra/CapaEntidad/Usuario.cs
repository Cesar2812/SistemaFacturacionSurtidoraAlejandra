namespace CapaEntidad
{
    public class Usuario
    {
        public int id_Usuario { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string usuario { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public Rol objRolU { get; set; }
        public bool Estado { get; set; }


    }
}
