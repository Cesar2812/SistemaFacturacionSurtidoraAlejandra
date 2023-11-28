using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.FormulariosModales
{
    public partial class modalProv : Form
    {
        public Proveedor _proveedor { get; set; }
        public modalProv()
        {
            InitializeComponent();
        }

        private void modalProv_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn colum in dataGridModalProv.Columns)
                {
                    if (colum.Visible == true)
                    {
                        comboBusqueda.Items.Add(new OpcionesDeCombo() { valor = colum.Name, Texto = colum.HeaderText });
                    }

                }
                comboBusqueda.DisplayMember = "Texto";
                comboBusqueda.ValueMember = "Valor";
                comboBusqueda.SelectedIndex = 0;
            }
            catch
            {
            }
            //Listar Proveedores en el griv
            List<Proveedor> listProv = new CNProveedor().Listar();
            foreach (Proveedor item in listProv)
            {
                try
                {
                    if (item.Estado == true)
                    {
                        dataGridModalProv.Rows.Add(new object[] {item.id_Proveedor,item.Documento,item.RazonSocial,item.Nombre

                        });

                    }

                }
                catch
                {
                }
            }
        }
        private void dataGridModalProv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icolumn = e.ColumnIndex;

            if (irow >= 0 && icolumn > 0)
            {
                _proveedor = new Proveedor()
                {
                    id_Proveedor = Convert.ToInt32(dataGridModalProv.Rows[irow].Cells["id_Prov"].Value.ToString()),
                    Documento = dataGridModalProv.Rows[irow].Cells["Documento"].Value.ToString(),
                    RazonSocial = dataGridModalProv.Rows[irow].Cells["RazonSocial"].Value.ToString(),
                    Nombre = dataGridModalProv.Rows[irow].Cells["Nombre"].Value.ToString()
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void textBusquedas_TextChanged(object sender, EventArgs e)
        {
            string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
            if (dataGridModalProv.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridModalProv.Rows)
                {
                    if (row.Cells[filtro].Value.ToString().Trim().ToUpper().Contains(textBusquedas.Text.Trim().ToUpper()))
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
    }
}
