using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.FormulariosModales
{
    public partial class modalProd : Form
    {
        public Producto _producto { get; set; }
        public modalProd()
        {
            InitializeComponent();
        }

        private void modalProd_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn colum in dataProdModal.Columns)
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
            //mostrar en el gric de Productos
            List<Producto> listaprod = new CNProducto().Listar();
            foreach (Producto item in listaprod)
            {
                try
                {
                    if (item.Estado == true)
                    {
                        dataProdModal.Rows.Add(new object[] {item.id_Producto,item.Codigo,item.Nombre_Producto,
                                        item.obj_categoria.Nombre,
                                        item.obj_marca.NombreMarca,
                                        item.Descripcion,
                                        item.Stock,
                                        item.PrecioCompra,
                                        item.PrecioVenta,
                                        item.FechaVencimiento,
                        });

                    }

                }
                catch
                {

                }
            }
        }
        private void btnBuscarProv_Click(object sender, EventArgs e)
        {
        }

        private void dataProdModal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icolumn = e.ColumnIndex;

            if (irow >= 0 && icolumn > 0)
            {
                _producto = new Producto()
                {
                    id_Producto = Convert.ToInt32(dataProdModal.Rows[irow].Cells["id"].Value.ToString()),
                    Codigo = dataProdModal.Rows[irow].Cells["Cod"].Value.ToString(),
                    Nombre_Producto = dataProdModal.Rows[irow].Cells["Nombre"].Value.ToString(),
                    Stock = Convert.ToInt32(dataProdModal.Rows[irow].Cells["Stock"].Value.ToString()),
                    PrecioCompra = Convert.ToDecimal(dataProdModal.Rows[irow].Cells["PrecioCompra"].Value.ToString()),
                    PrecioVenta = Convert.ToDecimal(dataProdModal.Rows[irow].Cells["PrecioVenta"].Value.ToString()),
                    FechaVencimiento = dataProdModal.Rows[irow].Cells["FechaVencimiento"].Value.ToString()
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void txtBusquedas_TextChanged(object sender, EventArgs e)
        {
            string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
            if (dataProdModal.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataProdModal.Rows)
                {
                    if (row.Cells[filtro].Value.ToString().Trim().ToUpper().Contains(txtBusquedas.Text.Trim().ToUpper()))
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
