using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDCliente
    {
        //Listar Cliente
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Select id_Cliente,Cedula,Nombre1,Nombre2,Apellido1,Apellido2,Telefono,Estado from Cliente");


                    SqlCommand terminal = new SqlCommand(query.ToString(), conection);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();
                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                id_Cliente = Convert.ToInt32(dr["id_Cliente"]),
                                Cedula = dr["Cedula"].ToString(),
                                Nombre1 = dr["Nombre1"].ToString(),
                                Nombre2 = dr["Nombre2"].ToString(),
                                Apellido1 = dr["Apellido1"].ToString(),
                                Apellido2 = dr["Apellido2"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Cliente>();
                }
            }
            return lista;
        }
        //Registrar Cliente
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idClienteGenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Registrar_Cliente", conection);
                    terminal.Parameters.AddWithValue("@Cedula", obj.Cedula);
                    terminal.Parameters.AddWithValue("@Nombre1", obj.Nombre1);
                    terminal.Parameters.AddWithValue("@Nombre2", obj.Nombre2);
                    terminal.Parameters.AddWithValue("@Apellido1", obj.Apellido1);
                    terminal.Parameters.AddWithValue("@Apellido2", obj.Apellido2);
                    terminal.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    terminal.Parameters.AddWithValue("@Estado", obj.Estado);
                    terminal.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    idClienteGenerado = Convert.ToInt32(terminal.Parameters["Resultado"].Value);
                    Mensaje = terminal.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                idClienteGenerado = 0;
                Mensaje = ex.Message;

            }
            return idClienteGenerado;
        }
        //Editar Cliente
        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Editar_Cliente", conection);
                    terminal.Parameters.AddWithValue("@idCliente", obj.id_Cliente);
                    terminal.Parameters.AddWithValue("@Cedula", obj.Cedula);
                    terminal.Parameters.AddWithValue("@Nombre1", obj.Nombre1);
                    terminal.Parameters.AddWithValue("@Nombre2", obj.Nombre2);
                    terminal.Parameters.AddWithValue("@Apellido1", obj.Apellido1);
                    terminal.Parameters.AddWithValue("@Apellido2", obj.Apellido2);
                    terminal.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    terminal.Parameters.AddWithValue("@Estado", obj.Estado);
                    terminal.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(terminal.Parameters["Resultado"].Value);
                    Mensaje = terminal.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;

            }
            return respuesta;
        }
        //Eliminar Cliente
        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Eliminar_Cliente", conection);
                    terminal.Parameters.AddWithValue("@idCliente", obj.id_Cliente);
                    terminal.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(terminal.Parameters["@Resultado"].Value);
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
