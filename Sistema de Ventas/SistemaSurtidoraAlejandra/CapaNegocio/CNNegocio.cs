using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CNNegocio
    {

        CDNegocio objnegocio = new CDNegocio();
        public Negocio obtenerdatos()
        {
            return objnegocio.AdquirirDatos();
        }

        public bool GuardarDatos(Negocio obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el Nombre del Negocio\n";

            }
            if (obj.RUC == "")
            {
                Mensaje += "Es necesario el Numero RUC del Negocio\n";
            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es necesaria la Direccion del Negocio\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objnegocio.GuardarDatos(obj, out Mensaje);

            }

        }
        public byte[] obtenerLogo(out bool obtenido)
        {
            return objnegocio.obtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            return objnegocio.ActualizarLogo(imagen, out mensaje);
        }

    }
}
