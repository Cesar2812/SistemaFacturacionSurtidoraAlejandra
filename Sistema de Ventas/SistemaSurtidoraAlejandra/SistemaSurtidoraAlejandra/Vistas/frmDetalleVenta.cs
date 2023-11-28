﻿using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using SistemaSurtidoraAlejandra.Reportes;
using System;
using System.IO;
using System.Windows.Forms;


namespace SistemaSurtidoraAlejandra
{
    public partial class frmDetalleVenta : Form
    {
        public frmDetalleVenta()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, System.EventArgs e)
        {
            Venta oVenta = new CNVenta().obtnerVenta(textBusquedas.Text);
            if (oVenta.id_Venta != 0)
            {
                txtNdocumentoCompra.Text = oVenta.NumeroDocumento;
                textFecha.Text = oVenta.FechaRegistro;
                txtTipoDocumentoCompra.Text = oVenta.TipoDocumento;
                textUser.Text = oVenta.obj_user.Nombre1;

                txtNuDocProve.Text = oVenta.obj_Cliente.Cedula;
                textRazonSocial.Text = oVenta.obj_Cliente.Nombre1;

                dataGridViewDetalle.Rows.Clear();

                foreach (DetalleVenta dv in oVenta.obDetalle)
                {
                    dataGridViewDetalle.Rows.Add(new object[] { dv.obj_pro.Nombre_Producto, dv.PrecioVenta, dv.Cantidad, dv.Sub_Total });
                }
                textMonto.Text = oVenta.MontoTotal.ToString("0.00");
                txtPago.Text = oVenta.MontoPago.ToString("0.00");
                txtCambio.Text = oVenta.MontoCambio.ToString("0.00");
            }
        }

        private void pictureBox2_Click(object sender, System.EventArgs e)
        {
            textFecha.Text = "";
            textRazonSocial.Text = "";
            dataGridViewDetalle.Rows.Clear();
            textMonto.Text = "";
            textUser.Text = "";
            txtNuDocProve.Text = "";
            textBusquedas.Text = "";
            txtTipoDocumentoCompra.Text = "";
        }

        private void frmDetalleVenta_Load(object sender, System.EventArgs e)
        {
            textBusquedas.Select();
        }
        private void btnDescargarPDF_Click(object sender, System.EventArgs e)
        {
            if (txtTipoDocumentoCompra.Text == "")
            {
                MessageBox.Show("No se encontraron los datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string tex_html = Properties.Resources.PlantillaVenta.ToString();
            Negocio odatos = new CNNegocio().obtenerdatos();
            tex_html = tex_html.Replace("@nombrenegocio", odatos.Nombre.ToUpper());
            tex_html = tex_html.Replace("@docnegocio", odatos.RUC.ToUpper());
            tex_html = tex_html.Replace("@direcnegocio", odatos.Direccion);

            tex_html = tex_html.Replace("@tipodocumento", txtTipoDocumentoCompra.Text.ToUpper());
            tex_html = tex_html.Replace("@numerodocumento", txtNdocumentoCompra.Text);

            tex_html = tex_html.Replace("@doccliente", txtNuDocProve.Text);
            tex_html = tex_html.Replace("@nombrecliente", textRazonSocial.Text);
            tex_html = tex_html.Replace("@fecharegistro", textFecha.Text);
            tex_html = tex_html.Replace("@usuarioregistro", textUser.Text);

            string Filas = string.Empty;
            foreach (DataGridViewRow row in dataGridViewDetalle.Rows)
            {
                Filas += "<tr>";
                Filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                Filas += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                Filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                Filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                Filas += "</tr>";
            }
            tex_html = tex_html.Replace("@filas", Filas);
            tex_html = tex_html.Replace("@montototal", textMonto.Text);
            tex_html = tex_html.Replace("@pagocon", txtPago.Text);
            tex_html = tex_html.Replace("@cambio", txtCambio.Text);

            SaveFileDialog save = new SaveFileDialog();
            save.FileName = string.Format("FacturaDeVenta_{0}.pdf", DateTime.Now.ToString(textUser.Text));
            save.Filter = "Pdf Files |*.pdf";

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(save.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();


                    bool obtenido = true;
                    byte[] byteImagen = new CNNegocio().obtenerLogo(out obtenido);

                    if (obtenido == true)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImagen);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;//poner la imagen encima del texto
                        img.SetAbsolutePosition(pdfdoc.Left, pdfdoc.GetTop(51));
                        pdfdoc.Add(img);
                    }

                    using (StringReader s = new StringReader(tex_html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfdoc, s);
                    }
                    pdfdoc.Close();
                    stream.Close();
                    MessageBox.Show("Espere", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Reporte de Factura Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        private void textBusquedas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es un número, suprimir el carácter
                e.Handled = true;
            }
        }

        private void textFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtTipoDocumentoCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void textUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtNuDocProve_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void textRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void textMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtCambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Por Favor Espere");
            Factura fac = new Factura();
            fac.ShowDialog();

        }
    }
}
