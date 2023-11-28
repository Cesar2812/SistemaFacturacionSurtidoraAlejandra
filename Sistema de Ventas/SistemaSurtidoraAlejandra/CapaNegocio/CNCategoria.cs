using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CNCategoria
    {

        CDCategorias obj_categ = new CDCategorias();
        public List<Categoria> Listar()
        {
            return obj_categ.Listar();
        }
        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el  nombre de la Categoria\n";

            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_categ.Registrar(obj, out Mensaje);

            }
        }

        public bool editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre de la Categoria\n";

            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_categ.Editar(obj, out Mensaje);

            }
        }
        public bool eliminar(Categoria obj, out string Mensaje)
        {
            return obj_categ.Eliminar(obj, out Mensaje);
        }

    }
}
