using CapaEntidad;
using CapaNegocio;
using DocumentFormat.OpenXml.Spreadsheet;
using SistemaSurtidoraAlejandra.FormulariosModales;
using SistemaSurtidoraAlejandra.Utilidades;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class frmReporteVenta : Form
    {
        public frmReporteVenta()
        {
            InitializeComponent();
        }

        private void frmReporteVenta_Load(object sender, System.EventArgs e)
        {

            foreach (DataGridViewColumn columna in dataGridViewCompras.Columns)
            {
                comboBuscar.Items.Add(new OpcionesDeCombo() { valor = columna.Name, Texto = columna.HeaderText });
            }
            comboBuscar.DisplayMember = "Texto";
            comboBuscar.ValueMember = "Valor";
            comboBuscar.SelectedIndex = 0;
        }

        private void label4_Click(object sender, System.EventArgs e)
        {


        }

        private void pictureBox4_Click(object sender, System.EventArgs e)
        {
            using (var modal = new modalClient())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {

                    txtIdCliente.Text = modal._Cliente.id_Cliente.ToString();
                    txtNombreCliente.Text = modal._Cliente.Nombre1.ToString();
                    txtApellido.Text = modal._Cliente.Apellido1.ToString();

                }
                else
                {
                    txtNombreCliente.Select();
                }
            }
        }
        private void textBusquedas_TextChanged(object sender, EventArgs e)
        {
            string columnafiltro = ((OpcionesDeCombo)comboBuscar.SelectedItem).valor.ToString();
            if (dataGridViewCompras.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewCompras.Rows)
                {
                    if (row.Cells[columnafiltro].Value.ToString().Trim().ToUpper().Contains(textBusquedas.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;

                    }
                    else
                    {
                        row.Visible = false;
                    }

                }
            }
        }
        private void btnoExcel_Click(object sender, EventArgs e)
        {
            if (dataGridViewCompras.Rows.Count < 1)
            {
                MessageBox.Show("No hay Datos para Exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("¿Desea Guardar el Reporte en el ordenador?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                MessageBox.Show("Reporte Generado");
                SLDocument sl = new SLDocument();
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(@"C:\Users\Cesar Cerda\Desktop\Surtidora Alejandra.PNG");
                byte[] ba;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Close();
                    ba = ms.ToArray();
                }
                SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
                pic.SetPosition(0, 2);
                pic.ResizeInPixels(150, 100);
                sl.InsertPicture(pic);
                sl.SetCellValue("F5", "Reporte de Ventas");
                SLStyle syle = sl.CreateStyle();
                syle.Font.FontName = "Arial";
                syle.Font.FontSize = 14;
                syle.Font.Bold = true;

                sl.SetCellStyle("F5", syle);
                sl.MergeWorksheetCells("F5", "I5");
                int cabecera = 7;
                int celdainicial = 7;
                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Ventas");//asignando cabeceras
                sl.SetCellValue("B" + cabecera, "FechaCreacion");
                sl.SetCellValue("C" + cabecera, "TipoDeDocumento");
                sl.SetCellValue("D" + cabecera, "NumeroDocumento");
                sl.SetCellValue("E" + cabecera, "Sub_Total");
                sl.SetCellValue("F" + cabecera, "NombreUsuario");
                sl.SetCellValue("G" + cabecera, "Cedula");
                sl.SetCellValue("H" + cabecera, "NombreCliente");
                sl.SetCellValue("I" + cabecera, "ApellidoCliente");
                sl.SetCellValue("J" + cabecera, "CodigoProducto");
                sl.SetCellValue("K" + cabecera, "NombreProducto");
                sl.SetCellValue("L" + cabecera, "Marca");
                sl.SetCellValue("M" + cabecera, "Categoria");
                sl.SetCellValue("N" + cabecera, "FechaVencimiento");
                sl.SetCellValue("O" + cabecera, "PrecioVenta");
                sl.SetCellValue("P" + cabecera, "Cantidad");
                sl.SetCellValue("Q" + cabecera, "Total");

                SLStyle estiloColumna = sl.CreateStyle();
                estiloColumna.Font.FontName = "Arial";
                estiloColumna.Font.FontSize = 12;
                estiloColumna.Font.Bold = true;
                estiloColumna.Font.FontColor = System.Drawing.Color.Black;
                estiloColumna.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightPink, System.Drawing.Color.DeepPink);
                sl.SetCellStyle("B" + cabecera, "Q" + cabecera, estiloColumna);

                //recoriendo el Griv
                foreach (DataGridViewRow row in dataGridViewCompras.Rows)
                {
                    cabecera++;
                    sl.SetCellValue("B" + cabecera, row.Cells[0].Value.ToString());
                    sl.SetCellValue("C" + cabecera, row.Cells[1].Value.ToString());
                    sl.SetCellValue("D" + cabecera, row.Cells[2].Value.ToString());
                    sl.SetCellValue("E" + cabecera, row.Cells[3].Value.ToString());
                    sl.SetCellValue("F" + cabecera, row.Cells[4].Value.ToString());
                    sl.SetCellValue("G" + cabecera, row.Cells[5].Value.ToString());
                    sl.SetCellValue("H" + cabecera, row.Cells[6].Value.ToString());
                    sl.SetCellValue("I" + cabecera, row.Cells[7].Value.ToString());
                    sl.SetCellValue("J" + cabecera, row.Cells[8].Value.ToString());
                    sl.SetCellValue("K" + cabecera, row.Cells[9].Value.ToString());
                    sl.SetCellValue("L" + cabecera, row.Cells[10].Value.ToString());
                    sl.SetCellValue("M" + cabecera, row.Cells[11].Value.ToString());
                    sl.SetCellValue("N" + cabecera, row.Cells[12].Value.ToString());
                    sl.SetCellValue("O" + cabecera, row.Cells[13].Value.ToString());
                    sl.SetCellValue("P" + cabecera, row.Cells[14].Value.ToString());
                    sl.SetCellValue("Q" + cabecera, row.Cells[15].Value.ToString());
                }
                SLStyle estiloborde = sl.CreateStyle();
                estiloborde.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                estiloborde.Border.LeftBorder.Color = System.Drawing.Color.Black;
                estiloborde.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                estiloborde.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                estiloborde.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
                sl.SetCellStyle("B" + celdainicial, "Q" + cabecera, estiloborde);

                sl.AutoFitColumn("B", "Q");
                sl.SaveAs(@"C:\Users\Cesar Cerda\Desktop\ReporteVentas "+txtNombreCliente.Text+" "+txtApellido.Text+".xlsx");
            }
        }
        private void txtNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idcliente = Convert.ToInt32(txtIdCliente.Text.ToString());
            List<ReporteVenta> lista = new List<ReporteVenta>();

            lista = new CNReportes().Venta(
                dateTimeFechainicio.Value.ToString(),
                dateTimefechafin.Value.ToString(),
                idcliente
                );
            dataGridViewCompras.Rows.Clear();
            foreach (ReporteVenta rc in lista)
            {
                dataGridViewCompras.Rows.Add(new object[]
                {
                  rc.FechaCreacion,
                  rc.TipoDocumento,
                 rc.NumeroDcoumento,
                     rc.MontoTotal,
                     rc.UsuarioRegistro,
                     rc.Cedula,
                    rc.Nombre1,
                    rc.Apellido1,
                    rc.CodigoProducto,
                     rc.NombreProduto,
                     rc.Marca,
                    rc.Categoria,
                     rc.FechaVencimiento,
                     rc.PrecioDeVenta,
                         rc.Cantidad,
                     rc.SubTotal
                });
            }


        }
    }
}
