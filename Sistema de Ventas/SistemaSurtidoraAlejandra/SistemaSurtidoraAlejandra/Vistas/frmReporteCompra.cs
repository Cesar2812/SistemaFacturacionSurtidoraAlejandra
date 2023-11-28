using CapaEntidad;
using CapaNegocio;
using DocumentFormat.OpenXml.Spreadsheet;
using SistemaSurtidoraAlejandra.Utilidades;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class frmReporteCompra : Form
    {
        public frmReporteCompra()
        {
            InitializeComponent();
        }

        private void frmReporteCompra_Load(object sender, System.EventArgs e)
        {
            List<Proveedor> lista = new CNProveedor().Listar();
            comboBoxBuscarProv.Items.Add(new OpcionesDeCombo() { valor = 0, Texto = "TODOS" });

            foreach (Proveedor item in lista)
            {
                comboBoxBuscarProv.Items.Add(new OpcionesDeCombo() { valor = item.id_Proveedor, Texto = item.RazonSocial });
            }
            comboBoxBuscarProv.DisplayMember = "Texto";
            comboBoxBuscarProv.ValueMember = "Valor";
            comboBoxBuscarProv.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dataGridViewCompras.Columns)
            {
                comboBuscar.Items.Add(new OpcionesDeCombo() { valor = columna.Name, Texto = columna.HeaderText });
            }
            comboBuscar.DisplayMember = "Texto";
            comboBuscar.ValueMember = "Valor";
            comboBuscar.SelectedIndex = 0;
        }

        private void pictureBox3_Click(object sender, System.EventArgs e)
        {
            int idproveedor = Convert.ToInt32(((OpcionesDeCombo)comboBoxBuscarProv.SelectedItem).valor.ToString());
            List<ReporteCompra> lista = new List<ReporteCompra>();

            lista = new CNReportes().Compra(
                dateTimeFechainicio.Value.ToString(),
                dateTimefechafin.Value.ToString(),
                idproveedor
                );
            string v = "";
            dataGridViewCompras.Rows.Clear();
            foreach (ReporteCompra rc in lista)
            {
                dataGridViewCompras.Rows.Add(new object[]
                {
                    rc.FechaCreacion,
                    rc.TipoDocumento,
                    rc.NumeroDcoumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.RazonSocial,
                    rc.CodigoProducto,
                    rc.NombreProduto,
                    rc.Marca,
                    rc.Categoria,
                    rc.FechaVencimiento,
                    rc.PrecioDeCompra,
                    rc.PrecioDeVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });
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
                sl.SetCellValue("F5", "Reporte de Compras");
                SLStyle syle = sl.CreateStyle();
                syle.Font.FontName = "Arial";
                syle.Font.FontSize = 14;
                syle.Font.Bold = true;

                sl.SetCellStyle("F5", syle);
                sl.MergeWorksheetCells("F5", "I5");
                int cabecera = 7;
                int celdainicial = 7;
                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Compras");//asignando cabeceras
                sl.SetCellValue("B" + cabecera, "FechaCreacion");
                sl.SetCellValue("C" + cabecera, "TipoDeDocumento");
                sl.SetCellValue("D" + cabecera, "NumeroDocumento");
                sl.SetCellValue("E" + cabecera, "Sub_Total");
                sl.SetCellValue("F" + cabecera, "NombreUsuario");
                sl.SetCellValue("G" + cabecera, "DocumentoProveedor");
                sl.SetCellValue("H" + cabecera, "RazonSocial");
                sl.SetCellValue("I" + cabecera, "CodigoProducto");
                sl.SetCellValue("J" + cabecera, "NombreProducto");
                sl.SetCellValue("K" + cabecera, "Marca");
                sl.SetCellValue("L" + cabecera, "Categoria");
                sl.SetCellValue("M" + cabecera, "FechaVencimiento");
                sl.SetCellValue("N" + cabecera, "PrecioCompra");
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
                sl.SaveAs(@"C:\Users\Cesar Cerda\Desktop\ReporteCompras.xlsx");


            }

        }
    }
}
