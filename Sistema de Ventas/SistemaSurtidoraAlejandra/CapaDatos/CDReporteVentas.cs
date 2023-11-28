using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDReporteVentas
    {
        public List<ReporteVenta> Listar(string fechaCreacion, string fechaFin, int idcliente)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand terminal = new SqlCommand("ReporteVentas", conection);
                    terminal.Parameters.AddWithValue("fechaCreacion", fechaCreacion);
                    terminal.Parameters.AddWithValue("fechaFin", fechaFin);
                    terminal.Parameters.AddWithValue("idcliente", idcliente);
                    terminal.CommandType = CommandType.StoredProcedure;


                    conection.Open();
                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                FechaCreacion = dr["fechaCreacion"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDcoumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                UsuarioRegistro = dr["NombreUsuario"].ToString(),
                                Cedula = dr["Cedula"].ToString(),
                                Nombre1 = dr["NombreCliente"].ToString(),
                                Apellido1 = dr["ApellidoCliente"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProduto = dr["NombreProducto"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                FechaVencimiento = dr["FechaVencimiento"].ToString(),
                                PrecioDeVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["Total"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ReporteVenta>();

                }
            }

            return lista;

        }

    }
}
