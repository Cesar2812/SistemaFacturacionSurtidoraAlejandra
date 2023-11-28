using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.FormulariosModales
{
    public partial class modalClient : Form
    {
        public Cliente _Cliente { get; set; }
        public modalClient()
        {
            InitializeComponent();
        }

        private void modalClient_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn colum in dataGridView.Columns)
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

            //Listar Clientes en el griv
            List<Cliente> listclient = new CNCliente().Listar();
            foreach (Cliente item in listclient)
            {
                try
                {
                    if (item.Estado == true)
                    {
                        dataGridView.Rows.Add(new object[] {item.id_Cliente,item.Cedula,item.Nombre1,item.Apellido1,item.Telefono

                        });

                    }

                }
                catch
                {
                }
            }

        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icolumn = e.ColumnIndex;

            if (irow >= 0 && icolumn > 0)
            {
                _Cliente = new Cliente()
                {
                    id_Cliente = Convert.ToInt32(dataGridView.Rows[irow].Cells["Id"].Value.ToString()),
                    Cedula = dataGridView.Rows[irow].Cells["Cedula"].Value.ToString(),
                    Nombre1 = dataGridView.Rows[irow].Cells["PrimerNombre"].Value.ToString(),
                    Apellido1 = dataGridView.Rows[irow].Cells["PrimerApellido"].Value.ToString()
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void textBusquedas_TextChanged(object sender, EventArgs e)
        {
            string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
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
