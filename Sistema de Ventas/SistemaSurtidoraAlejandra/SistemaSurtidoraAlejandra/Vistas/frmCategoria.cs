using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }
        private void frmCategoria_Load(object sender, System.EventArgs e)
        {
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 1, Texto = "Activo" });
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 0, Texto = "No Activo" });

            comboActivo.DisplayMember = "Texto";
            comboActivo.ValueMember = "Valor";
            comboActivo.SelectedIndex = 0;

            try
            {
                foreach (DataGridViewColumn colum in dataGridUsers.Columns)
                {
                    if (colum.Visible == true && colum.Name != "btnSelec" && colum.Name != "Estado")
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
            //mostrar en el gric de categoria
            List<Categoria> listaCategorias = new CNCategoria().Listar();
            foreach (Categoria item in listaCategorias)
            {
                try
                {
                    dataGridUsers.Rows.Add(new object[] {"",item.id_Categoria,item.Nombre,
                       item.Estado==true ?1 : 0,
                        item.Estado== true ? "Activo" : "No Activo"
                    });
                }
                catch
                {

                }
            }
        }
        private void butregistrar_Click(object sender, System.EventArgs e)//boton de registar
        {
            string mensaje = string.Empty;

            Categoria objcateg = new Categoria()
            {
                id_Categoria = Convert.ToInt32(txtID.Text),
                Nombre = txtNombre.Text,
                Estado = Convert.ToInt32(((OpcionesDeCombo)comboActivo.SelectedItem).valor) == 1 ? true : false
            };
            if (objcateg.id_Categoria == 0)
            {
                int idcategoria = new CNCategoria().Registrar(objcateg, out mensaje);

                if (idcategoria != 0)
                {
                    try
                    {
                        MessageBox.Show("Categoria Registrada");
                        dataGridUsers.Rows.Add(new object[] {"",idcategoria,txtNombre.Text,
                              ((OpcionesDeCombo)comboActivo.SelectedItem).valor.ToString(),
                               ((OpcionesDeCombo)comboActivo.SelectedItem).Texto.ToString()
                        });

                    }
                    catch
                    {
                    }
                    Limpiar();

                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CNCategoria().editar(objcateg, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataGridUsers.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["id_Categoria"].Value = txtID.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionesDeCombo)comboActivo.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionesDeCombo)comboActivo.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }
        public void Limpiar()
        {
            txtIndice.Text = "-1";
            txtID.Text = "0";
            txtNombre.Text = "";

            txtNombre.Select();

        }
        private void dataGridUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridUsers.Columns[e.ColumnIndex].Name == "btnSelec")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtID.Text = dataGridUsers.Rows[indice].Cells["id_Categoria"].Value.ToString();
                    txtNombre.Text = dataGridUsers.Rows[indice].Cells["Nombre"].Value.ToString();

                    foreach (OpcionesDeCombo oc in comboActivo.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridUsers.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = comboActivo.Items.IndexOf(oc);
                            comboActivo.SelectedIndex = indice_combo;
                            break;

                        }
                    }

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)//boton eliminar
        {
            if (Convert.ToInt32(txtID.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar la Categoria?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Categoria objcateg = new Categoria()
                    {
                        id_Categoria = Convert.ToInt32(txtID.Text)
                    };
                    bool respuesta = new CNCategoria().eliminar(objcateg, out mensaje);

                    if (respuesta)
                    {
                        dataGridUsers.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                }
            }
            Limpiar();

        }
        private void pictureBox3_Click(object sender, EventArgs e)//bton buscar
        {
            string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
            if (dataGridUsers.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridUsers.Rows)
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
        private void pictureBox2_Click(object sender, EventArgs e)//boton limpiar busqueda
        {
            textBusquedas.Text = "";
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                row.Visible = true;
            }
        }
        private void textBusquedas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
                if (dataGridUsers.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridUsers.Rows)
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
            catch
            {

            }
        }
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
    }
}
