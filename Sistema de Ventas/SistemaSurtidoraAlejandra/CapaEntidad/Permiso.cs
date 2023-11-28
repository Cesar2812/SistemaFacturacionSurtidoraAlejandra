namespace CapaEntidad
{
    public class Permiso
    {
        public int id_Permiso { get; set; }
        public Rol objRol { get; set; }
        public string Nombre_Menu { get; set; }
        public string fechacreacion { get; set; }
    }
}
