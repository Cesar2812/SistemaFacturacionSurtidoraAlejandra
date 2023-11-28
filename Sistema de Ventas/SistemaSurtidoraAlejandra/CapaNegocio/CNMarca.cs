using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CNMarca
    {
        CDMarcas obj_marca = new CDMarcas();
        //listar marca
        public List<Marca> Listar()
        {
            return obj_marca.Listar();
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreMarca == "")
            {
                Mensaje += "Es necesario el Nombre y la Categoria de la marca\n";

            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_marca.Registrar(obj, out Mensaje);

            }
        }
        public bool editar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreMarca == "")
            {
                Mensaje += "Es necesario el Nombre y Categoria de la Marca\n";

            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_marca.Editar(obj, out Mensaje);

            }
        }
        public bool eliminar(Marca obj, out string Mensaje)
        {
            return obj_marca.Eliminar(obj, out Mensaje);
        }
    }
}
