using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class frmMarcas : Form
    {
        public frmMarcas()
        {
            InitializeComponent();
        }

        private void comboActivo_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void frmMarcas_Load(object sender, System.EventArgs e)
        {
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 1, Texto = "Activo" });
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 0, Texto = "No Activo" });

            comboActivo.DisplayMember = "Texto";
            comboActivo.ValueMember = "Valor";
            comboActivo.SelectedIndex = 0;

            List<Categoria> listacateg = new CNCategoria().Listar();
            foreach (Categoria item in listacateg)
            {
                comboCategoria.Items.Add(new OpcionesDeCombo() { valor = item.id_Categoria, Texto = item.Nombre });


            }
            comboCategoria.DisplayMember = "Texto";
            comboCategoria.ValueMember = "Valor";
            comboCategoria.SelectedIndex = 0;
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

            //mostrar en el gric marcas
            List<Marca> listamarca = new CNMarca().Listar();
            foreach (Marca item in listamarca)
            {
                try
                {
                    dataGridUsers.Rows.Add(new object[] {"",item.id_Marca,item.NombreMarca,
                       item.obj_categoria.id_Categoria,
                       item.obj_categoria.Nombre,
                       item.Estado==true ?1 : 0,
                        item.Estado== true ? "Activo" : "No Activo"
                    });
                }
                catch
                {

                }
            }
        }
        private void butregistrar_Click(object sender, System.EventArgs e)
        {
            string mensaje = string.Empty;

            Marca objmarca = new Marca()
            {
                id_Marca = Convert.ToInt32(txtID.Text),
                NombreMarca = txtMarca.Text,
                obj_categoria = new Categoria() { id_Categoria = Convert.ToInt32(((OpcionesDeCombo)comboCategoria.SelectedItem).valor) },
                Estado = Convert.ToInt32(((OpcionesDeCombo)comboActivo.SelectedItem).valor) == 1 ? true : false
            };

            if (objmarca.id_Marca == 0)
            {
                int marcacreada = new CNMarca().Registrar(objmarca, out mensaje);

                if (marcacreada != 0)
                {
                    try
                    {
                        MessageBox.Show("Marca Registrada");
                        dataGridUsers.Rows.Add(new object[] {"",marcacreada,txtMarca.Text,
                               ((OpcionesDeCombo)comboCategoria.SelectedItem).valor.ToString(),
                               ((OpcionesDeCombo)comboCategoria.SelectedItem).Texto.ToString(),
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
                bool resultado = new CNMarca().editar(objmarca, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataGridUsers.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["id_Marca"].Value = txtID.Text;
                    row.Cells["Nombre"].Value = txtMarca.Text;
                    row.Cells["id_Categ"].Value = ((OpcionesDeCombo)comboCategoria.SelectedItem).valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionesDeCombo)comboCategoria.SelectedItem).Texto.ToString();
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
            txtMarca.Text = "";

            txtMarca.Select();

        }
        private void dataGridUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridUsers.Columns[e.ColumnIndex].Name == "btnSelec")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtID.Text = dataGridUsers.Rows[indice].Cells["id_Marca"].Value.ToString();
                    txtMarca.Text = dataGridUsers.Rows[indice].Cells["Nombre"].Value.ToString();


                    foreach (OpcionesDeCombo oc in comboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridUsers.Rows[indice].Cells["id_Categ"].Value))
                        {
                            int indice_combo = comboCategoria.Items.IndexOf(oc);
                            comboCategoria.SelectedIndex = indice_combo;
                            break;

                        }
                    }

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
        private void pictureBox3_Click(object sender, EventArgs e)
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
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBusquedas.Text = "";
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                row.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtID.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el la Marca?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Marca objmarca = new Marca()
                    {
                        id_Marca = Convert.ToInt32(txtID.Text)
                    };
                    bool respuesta = new CNMarca().eliminar(objmarca, out mensaje);

                    if (respuesta)
                    {
                        dataGridUsers.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                }
            }
            Limpiar();
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
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }

        }
        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
    }
}
