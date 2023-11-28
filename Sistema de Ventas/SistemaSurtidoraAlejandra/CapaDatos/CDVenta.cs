using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDVenta
    {
        public int obtnerCorrerlativo()
        {
            int idCorrelativo = 0;
            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select count(*)+1 from Venta");
                    SqlCommand terminal = new SqlCommand(query.ToString(), conection);
                    terminal.CommandType = CommandType.Text;
                    conection.Open();

                    idCorrelativo = Convert.ToInt32(terminal.ExecuteScalar());


                }
                catch (Exception ex)
                {
                    idCorrelativo = 0;

                }
            }
            return idCorrelativo;
        }

        public bool RestarStocK(int idproducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Update Producto set Stock=Stock-@cantidad where id_Producto=@id_Producto");
                    SqlCommand cmd = new SqlCommand(query.ToString(), conection);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@id_Producto", idproducto);
                    cmd.CommandType = CommandType.Text;
                    conection.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;

        }

        public bool SumarStocK(int idproducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Update Producto set Stock=Stock+@cantidad where id_Producto=@id_Producto");
                    SqlCommand cmd = new SqlCommand(query.ToString(), conection);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@id_Producto", idproducto);
                    cmd.CommandType = CommandType.Text;
                    conection.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;

        }

        public bool RegistrarVenta(Venta obj, DataTable DetalleVenta, out string mensaje)
        {
            bool respuesta = true;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("RegistrarVenta", conn);
                    terminal.Parameters.AddWithValue("@idusuario", obj.obj_user.id_Usuario);
                    terminal.Parameters.AddWithValue("@TipoDeDocumento", obj.TipoDocumento);
                    terminal.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento);
                    terminal.Parameters.AddWithValue("@idcliente", obj.obj_Cliente.id_Cliente);
                    terminal.Parameters.AddWithValue("@MontoPago", obj.MontoPago);
                    terminal.Parameters.AddWithValue("@Montocambio", obj.MontoCambio);
                    terminal.Parameters.AddWithValue("@MontoTotal", obj.MontoTotal);
                    terminal.Parameters.AddWithValue("@DetalleVenta", DetalleVenta);
                    terminal.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    terminal.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(terminal.Parameters["@Resultado"].Value);
                    mensaje = terminal.Parameters["@Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;

            }
            return respuesta;

        }
        public Venta obtenerVenta(string numero)
        {
            Venta objec = new Venta();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.id_Venta,");
                    query.AppendLine(" u.Nombre1,c.Cedula,c.Nombre1 as NombreCliente,");
                    query.AppendLine("v.TipoDocumento,v.NumeroDocumento,");
                    query.AppendLine("v.MontoPago,v.MontoCambio,");
                    query.AppendLine("v.MontoTotal,convert(char(10),v.FechaRegistro,103)[FechaCreacion]");
                    query.AppendLine(" from Venta v");
                    query.AppendLine("inner join Usuario u on u.id_Usuario=v.id_usuario");
                    query.AppendLine("inner join Cliente c on c.id_Cliente=v.id_cliente");
                    query.AppendLine(" where NumeroDocumento=@numero");


                    SqlCommand terminal = new SqlCommand(query.ToString(), conection);
                    terminal.Parameters.AddWithValue("@numero", numero);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();

                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objec = new Venta()
                            {
                                id_Venta = Convert.ToInt32(dr["id_Venta"]),
                                obj_user = new Usuario() { Nombre1 = dr["Nombre1"].ToString() },
                                obj_Cliente = new Cliente() { Cedula = dr["Cedula"].ToString(), Nombre1 = dr["NombreCliente"].ToString() },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaCreacion"].ToString()
                            };
                        }
                    }
                    string v = "";

                }
                catch (Exception ex)
                {
                    objec = new Venta();

                }
            }
            return objec;

        }
        public List<DetalleVenta> obtenerDetail(int idVenta)
        {
            List<DetalleVenta> olista = new List<DetalleVenta>();
            try
            {
                using (SqlConnection Conecion = new SqlConnection(Conexion.cadena))
                {
                    Conecion.Open();
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("Select p.Nombre_Producto,dv.PrecioVenta,dv.Cantidad,dv.Sub_Total");
                    consulta.AppendLine(" from Detalle_Venta dv");
                    consulta.AppendLine("inner Join Producto p on p.id_Producto=dv.id_producto");
                    consulta.AppendLine(" where dv.id_venta=@id_venta");

                    SqlCommand cmd = new SqlCommand(consulta.ToString(), Conecion);
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olista.Add(new DetalleVenta()
                            {
                                obj_pro = new Producto() { Nombre_Producto = dr["Nombre_Producto"].ToString() },
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                Sub_Total = Convert.ToDecimal(dr["Sub_Total"].ToString())
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                olista = new List<DetalleVenta>();
            }
            return olista;
        }
    }
}
