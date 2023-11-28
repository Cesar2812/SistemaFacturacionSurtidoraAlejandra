using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDCompra
    {
        public int obtnerCorrerlativo()
        {
            int idCorrelativo = 0;
            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select count(*)+1 from Compra");
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
        //Registrar la Compra
        public bool Registrar(Compra objcompra, DataTable DetalleCompra, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    SqlCommand terminal = new SqlCommand("RegistrarCompraa", conection);
                    terminal.Parameters.AddWithValue("idusuario", objcompra.od_usuario.id_Usuario);
                    terminal.Parameters.AddWithValue("idproveed", objcompra.od_Proveedor.id_Proveedor);
                    terminal.Parameters.AddWithValue("TipoDeDocumento", objcompra.TipoDeDocumento);
                    terminal.Parameters.AddWithValue("NumeroDocumento", objcompra.NumeroDocumento);
                    terminal.Parameters.AddWithValue("MontoTotal", objcompra.MontoTotal);
                    terminal.Parameters.AddWithValue("DetalleCompra", DetalleCompra);
                    terminal.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;

                    conection.Open();

                    terminal.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(terminal.Parameters["Resultado"].Value);
                    Mensaje = terminal.Parameters["Mensaje"].Value.ToString();


                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Mensaje = ex.Message;

                }
            }
            return respuesta;

        }

        public Compra ObtnerCompra(string numero)
        {
            Compra objec = new Compra();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" Select c.id_Compra,");
                    query.AppendLine(" u.Nombre1,");
                    query.AppendLine(" pr.Documento,pr.RazonSocial,");
                    query.AppendLine(" c.TipoDocumento,c.NumeroDocumento,c.Sub_Total,convert(char(10),c.FechaRegistro,103)[FechaCreacion]");
                    query.AppendLine("from Compra c");
                    query.AppendLine("inner join Usuario u on u.id_Usuario=c.id_usuario");
                    query.AppendLine(" inner join Proveedor pr on pr.id_Proveedor=c.id_Proveedor");
                    query.AppendLine(" where c.NumeroDocumento=@numero");


                    SqlCommand terminal = new SqlCommand(query.ToString(), conection);
                    terminal.Parameters.AddWithValue("@numero", numero);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();

                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objec = new Compra()
                            {
                                id_Compra = Convert.ToInt32(dr["id_Compra"]),
                                od_usuario = new Usuario() { Nombre1 = dr["Nombre1"].ToString() },
                                od_Proveedor = new Proveedor() { Documento = dr["Documento"].ToString(), RazonSocial = dr["RazonSocial"].ToString() },
                                TipoDeDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["Sub_Total"].ToString()),
                                FechaCreacion = dr["FechaCreacion"].ToString()

                            };
                        }
                    }
                    string v = "";

                }
                catch (Exception ex)
                {
                    objec = new Compra();

                }
            }

            return objec;
        }

        public List<DetalleCompra> obtenerDetail(int idCompra)
        {
            List<DetalleCompra> olista = new List<DetalleCompra>();
            try
            {
                using (SqlConnection Conecion = new SqlConnection(Conexion.cadena))
                {
                    Conecion.Open();
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select Nombre_Producto,Detalle_Compra.PrecioCompra,Cantidad,Total");
                    consulta.AppendLine(" from Detalle_Compra");
                    consulta.AppendLine("inner join Producto on Detalle_Compra.id_producto=Producto.id_Producto");
                    consulta.AppendLine(" where id_compra=@id_compra");

                    SqlCommand cmd = new SqlCommand(consulta.ToString(), Conecion);
                    cmd.Parameters.AddWithValue("@id_compra", idCompra);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olista.Add(new DetalleCompra()
                            {
                                o_idProducto = new Producto() { Nombre_Producto = dr["Nombre_Producto"].ToString() },
                                PrecioDeCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                Montototal = Convert.ToDecimal(dr["Total"].ToString())
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                olista = new List<DetalleCompra>();
            }
            return olista;
        }

    }
}
