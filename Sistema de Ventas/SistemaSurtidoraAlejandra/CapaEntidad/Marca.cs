namespace CapaEntidad
{
    public class Marca
    {
        public int id_Marca { get; set; }
        public string NombreMarca { get; set; }
        public Categoria obj_categoria { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

    }
}
