using CapaEntidad;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDNegocio
    {
        public Negocio AdquirirDatos()
        {
            Negocio objNegocio = new Negocio();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string consult = "Select id_Negocio, Nombre, RUC, Direccion from Negocio Where id_Negocio = 1";
                    SqlCommand cmd = new SqlCommand(consult, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objNegocio = new Negocio()
                            {
                                id_Negocio = int.Parse(reader["id_Negocio"].ToString()),
                                Nombre = reader["Nombre"].ToString(),
                                RUC = reader["RUC"].ToString(),
                                Direccion = reader["Direccion"].ToString()

                            };
                        }
                    }
                }


            }
            catch
            {
                objNegocio = new Negocio();
            }
            return objNegocio;
        }
        public bool GuardarDatos(Negocio obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool respusta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("update Negocio set Nombre = @nombre,");
                    consulta.AppendLine("RUC = @RUC,");
                    consulta.AppendLine("Direccion = @Direccion");
                    consulta.AppendLine("Where id_Negocio = 1;");


                    SqlCommand cmd = new SqlCommand(consulta.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@RUC", obj.RUC);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudieron guardar los Datos";
                        respusta = false;

                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respusta = false;
            }
            return respusta;
        }

        //obtener imagen para la base de datos en el frm de negocio
        public byte[] obtenerLogo(out bool obtenido)
        {

            obtenido = true;
            byte[] logobyte = new byte[0];

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string consult = "Select Logo from Negocio Where id_Negocio = 1";



                    SqlCommand cmd = new SqlCommand(consult, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logobyte = (byte[])reader["Logo"];

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obtenido = false;
                logobyte = new byte[0];
            }
            return logobyte;

        }

        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("update Negocio set Logo =  @imagen");
                    consulta.AppendLine("Where id_Negocio = 1;");


                    SqlCommand cmd = new SqlCommand(consulta.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@imagen", image);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";
                        respuesta = false;

                    }

                }


            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }

    }
}
