using CapaDatos;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Reportes
{
    public partial class Factura : Form
    {
        public Factura()
        {
            InitializeComponent();
        }
        private void Factura_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SqlConnection conexi = new SqlConnection(Conexion.cadena);
            SqlDataAdapter da = new SqlDataAdapter("select * from VistaVentaDetalle where " +
                "NumeroDocumento='" + textBox1.Text + "'", conexi);
            DataSet ds = new DataSet();
            da.Fill(ds, "DataTableFactura");
            ReportDataSource repor = new ReportDataSource("DS_Report", ds.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(repor);
            this.reportViewer1.RefreshReport();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es un número, suprimir el carácter
                e.Handled = true;
            }
        }
    }
}
