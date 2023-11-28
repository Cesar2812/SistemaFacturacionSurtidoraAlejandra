using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace CapaDatos
{
    public class CDUsuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Select u.id_Usuario,u.Nombre1,u.Nombre2,u.Apellido1,u.Apellido2,u.usuario,u.Correo,u.Clave,u.Estado,r.id_Rol,r.Descripcion from Usuario u");
                    query.AppendLine("inner join Rol r on r.id_Rol = u.id_rol");

                    SqlCommand terminal = new SqlCommand(query.ToString(), conection);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();
                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                id_Usuario = Convert.ToInt32(dr["id_Usuario"]),
                                Nombre1 = dr["Nombre1"].ToString(),
                                Nombre2 = dr["Nombre2"].ToString(),
                                Apellido1 = dr["Apellido1"].ToString(),
                                Apellido2 = dr["Apellido2"].ToString(),
                                usuario = dr["usuario"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                objRolU = new Rol() { id_Rol = Convert.ToInt32(dr["id_Rol"]), Descripcion = dr["Descripcion"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }


                }
                catch (Exception ex)
                {
                    lista = new List<Usuario>();

                }
            }

            return lista;

        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idusuarioGenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Insertar_Usuarios", conection);
                    terminal.Parameters.AddWithValue("@Nombre1", obj.Nombre1);
                    terminal.Parameters.AddWithValue("@Nombre2", obj.Nombre2);
                    terminal.Parameters.AddWithValue("@Apellido1", obj.Apellido1);
                    terminal.Parameters.AddWithValue("@Apellido2", obj.Apellido2);
                    terminal.Parameters.AddWithValue("@Usuario", obj.usuario);
                    terminal.Parameters.AddWithValue("@Correo", obj.Correo);
                    terminal.Parameters.AddWithValue("@Clave", obj.Clave);
                    terminal.Parameters.AddWithValue("@id_Rol", obj.objRolU.id_Rol);
                    terminal.Parameters.AddWithValue("@Estado", obj.Estado);
                    terminal.Parameters.Add("@idUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    idusuarioGenerado = Convert.ToInt32(terminal.Parameters["@idUsuarioResultado"].Value);
                    Mensaje = terminal.Parameters["@Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                idusuarioGenerado = 0;
                Mensaje = ex.Message;

            }


            return idusuarioGenerado;
        }
        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Editar_Usuarios", conection);
                    terminal.Parameters.AddWithValue("@idUsuario", obj.id_Usuario);
                    terminal.Parameters.AddWithValue("@Nombre1", obj.Nombre1);
                    terminal.Parameters.AddWithValue("@Nombre2", obj.Nombre2);
                    terminal.Parameters.AddWithValue("@Apellido1", obj.Apellido1);
                    terminal.Parameters.AddWithValue("@Apellido2", obj.Apellido2);
                    terminal.Parameters.AddWithValue("@Usuario", obj.usuario);
                    terminal.Parameters.AddWithValue("@Correo", obj.Correo);
                    terminal.Parameters.AddWithValue("@Clave", obj.Clave);
                    terminal.Parameters.AddWithValue("@id_Rol", obj.objRolU.id_Rol);
                    terminal.Parameters.AddWithValue("@Estado", obj.Estado);
                    terminal.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(terminal.Parameters["@Respuesta"].Value);
                    Mensaje = terminal.Parameters["@Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;

            }
            return respuesta;
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Eliminar_Usuarios", conection);
                    terminal.Parameters.AddWithValue("@idUsuario", obj.id_Usuario);
                    terminal.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(terminal.Parameters["@Respuesta"].Value);
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;

        }
    }
}
