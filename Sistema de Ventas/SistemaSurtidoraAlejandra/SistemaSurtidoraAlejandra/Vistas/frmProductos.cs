using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using DocumentFormat.OpenXml.Spreadsheet;
using SistemaSurtidoraAlejandra.Utilidades;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class frmProductos : Form
    {
        CapaNegocio.CNProducto _prod = new CapaNegocio.CNProducto();
        //variables para almacenar codigo
        string producto = "";
        string codigo;
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, System.EventArgs e)
        {
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 1, Texto = "Activo" });
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 0, Texto = "No Activo" });

            comboActivo.DisplayMember = "Texto";
            comboActivo.ValueMember = "Valor";
            comboActivo.SelectedIndex = 0;

            try
            {
                foreach (DataGridViewColumn colum in dataGridProv.Columns)
                {
                    if (colum.Visible == true && colum.Name != "btnSelec" && colum.Name != "Estado")
                    {
                        comboOptions.Items.Add(new OpcionesDeCombo() { valor = colum.Name, Texto = colum.HeaderText });
                    }

                }
                comboOptions.DisplayMember = "Texto";
                comboOptions.ValueMember = "Valor";
                comboOptions.SelectedIndex = 0;
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
                    dataGridProv.Rows.Add(new object[] {"",item.id_Producto,item.Codigo,item.Nombre_Producto,item.obj_categoria.id_Categoria,
                        item.obj_categoria.Nombre,
                        item.obj_marca.id_Marca,
                        item.obj_marca.NombreMarca,item.Descripcion,
                        item.Stock,item.PrecioCompra,
                        item.PrecioVenta,
                        item.FechaVencimiento,
                        item.Estado==true ?1 : 0,
                        item.Estado== true ? "Activo" : "No Activo"
                    });
                }
                catch
                {

                }
            }
            ListarCategorias();
        }
        public void ListarCategorias()//metodoparaListarCategoria en su combo correspondiente
        {
            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder quuery = new StringBuilder();
                    quuery.AppendLine("Select id_Categoria,Nombre as Categoria from Categoria");
                    conection.Open();
                    SqlCommand cmd = new SqlCommand(quuery.ToString(), conection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conection.Close();

                    DataRow fila = dt.NewRow();
                    fila["Categoria"] = "Seleciona una Categoria";
                    dt.Rows.InsertAt(fila, 0);

                    cmbCategoria.ValueMember = "id_Categoria";
                    cmbCategoria.DisplayMember = "Categoria";
                    cmbCategoria.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void ListarMarcas(string idCategoria)//listar marca en combo
        {
            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder quuery = new StringBuilder();
                    quuery.AppendLine("select id_Marca,Nombre as Marca");
                    quuery.AppendLine("from Marca where id_categoria=@id_categoria");
                    conection.Open();
                    SqlCommand cmd = new SqlCommand(quuery.ToString(), conection);
                    cmd.Parameters.AddWithValue("id_categoria", idCategoria);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conection.Close();

                    DataRow fila = dt.NewRow();
                    fila["Marca"] = "Seleciona una Marca";
                    dt.Rows.InsertAt(fila, 0);

                    cmbMarca.ValueMember = "id_Marca";
                    cmbMarca.DisplayMember = "Marca";
                    cmbMarca.DataSource = dt;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());

                }

            }
        }
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)//cargar  marca en Categoria
        {
            try
            {
                if (cmbCategoria.SelectedValue.ToString() != null)
                {
                    string idCateg = cmbCategoria.SelectedValue.ToString();
                    ListarMarcas(idCateg);
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.ToString());
            }
        }
        public string generarCodigo(string product)//metodo para generar codigo
        {
            try
            {
                string[] cadena = product.Split(' ').ToArray();
                codigo = "";
                for (int i = 0; i < cadena.Length; i++)
                {
                    codigo += cadena[i].Substring(0, 1).ToUpper();

                }

                codigo = codigo + "-";
                Random codigo2 = new Random();
                int valor = codigo2.Next(1, 3500);
                codigo += valor.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return codigo;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProd.Text == " ")
                {
                    MessageBox.Show("Escriba el Producto");
                }
                else
                {
                    txtCodigo.Text = "";
                    producto = txtProd.Text + " " + cmbCategoria.Text + " " + cmbMarca.Text;
                    txtCodigo.Text = generarCodigo(producto);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void butregistrar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Producto objprod = new Producto()
            {
                id_Producto = Convert.ToInt32(txtID.Text),
                Codigo = txtCodigo.Text,
                Nombre_Producto = txtProd.Text,
                obj_categoria = new Categoria { id_Categoria = Convert.ToInt32(cmbCategoria.SelectedValue) },
                obj_marca = new Marca { id_Marca = Convert.ToInt32(cmbMarca.SelectedValue) },
                Descripcion = txtDescripcion.Text,
                Estado = Convert.ToInt32(((OpcionesDeCombo)comboActivo.SelectedItem).valor) == 1 ? true : false
            };
            if (objprod.id_Producto == 0)
            {
                int idproductoCreado = new CNProducto().Registrar(objprod, out mensaje);

                if (idproductoCreado != 0)
                {
                    try
                    {
                        MessageBox.Show("Producto insertado");

                        //mostrar en el gric de Productos

                        dataGridProv.Rows.Add(new object[] {"",idproductoCreado,txtCodigo.Text,txtProd.Text,
                               Convert.ToInt32(cmbCategoria.SelectedValue),
                               cmbCategoria.SelectedItem.ToString(),
                               Convert.ToInt32(cmbMarca.SelectedValue),
                               cmbMarca.SelectedItem.ToString(),
                               txtDescripcion.Text,
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
                bool resultado = new CNProducto().editar(objprod, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataGridProv.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["id_Producto"].Value = txtID.Text;
                    row.Cells["Cod"].Value = txtCodigo.Text;
                    row.Cells["Nombre"].Value = txtProd.Text;
                    row.Cells["id_Categoria"].Value = cmbCategoria.SelectedValue;
                    row.Cells["Categoria"].Value = cmbCategoria.SelectedItem.ToString();
                    row.Cells["id_Marca"].Value = cmbMarca.SelectedValue;
                    row.Cells["Marca"].Value = cmbMarca.SelectedItem.ToString();
                    row.Cells["Descripcion"].Value = txtDescripcion.Text;
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
            txtCodigo.Clear();
            txtProd.Clear();
            txtDescripcion.Clear();
            txtProd.Select();

        }
        private void pictureBox3_Click(object sender, EventArgs e)///bton de buscar
        {
            try
            {
                string filtro = ((OpcionesDeCombo)comboOptions.SelectedItem).valor.ToString();
                if (dataGridProv.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridProv.Rows)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void pictureBox2_Click(object sender, EventArgs e)//eliminar busqueda
        {
            txtBusquedas.Clear();
            foreach (DataGridViewRow row in dataGridProv.Rows)
            {
                row.Visible = true;
            }
        }
        private void dataGridProv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridProv.Columns[e.ColumnIndex].Name == "btnSelec")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtID.Text = dataGridProv.Rows[indice].Cells["id_Producto"].Value.ToString();
                    txtCodigo.Text = dataGridProv.Rows[indice].Cells["Cod"].Value.ToString();
                    txtProd.Text = dataGridProv.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Text = dataGridProv.Rows[indice].Cells["Descripcion"].Value.ToString();


                    foreach (OpcionesDeCombo oc in comboActivo.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridProv.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = comboActivo.Items.IndexOf(oc);
                            comboActivo.SelectedIndex = indice_combo;
                            break;

                        }
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)//btn Eliminar
        {
            if (Convert.ToInt32(txtID.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Producto objusuario = new Producto()
                    {
                        id_Producto = Convert.ToInt32(txtID.Text)
                    };
                    bool respuesta = new CNProducto().eliminar(objusuario, out mensaje);

                    if (respuesta)
                    {
                        dataGridProv.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                }
            }
            Limpiar();
        }
        private void btnoExcel_Click(object sender, EventArgs e)//boton para generar execl
        {
            if (dataGridProv.Rows.Count < 1)
            {
                MessageBox.Show("No hay Datos para Exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("¿Desea Guardar el Reporte en el ordenador?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                MessageBox.Show("Reporte Generado Exitosamente");
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
                sl.SetCellValue("H5", "Reporte de Productos");
                SLStyle syle = sl.CreateStyle();
                syle.Font.FontName = "Arial";
                syle.Font.FontSize = 14;
                syle.Font.Bold = true;

                sl.SetCellStyle("H5", syle);
                sl.MergeWorksheetCells("H5", "J5");
                int cabecera = 7;
                int celdainicial = 7;
                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Productos");//asignando cabeceras
                sl.SetCellValue("B" + cabecera, "ID");
                sl.SetCellValue("C" + cabecera, "Codigo");
                sl.SetCellValue("D" + cabecera, "Nombre");
                sl.SetCellValue("E" + cabecera, "Categoria");
                sl.SetCellValue("F" + cabecera, "Marca");
                sl.SetCellValue("G" + cabecera, "Descripcion");
                sl.SetCellValue("H" + cabecera, "Stock");
                sl.SetCellValue("I" + cabecera, "PrecioDeCompra");
                sl.SetCellValue("J" + cabecera, "PrecioDeVenta");
                sl.SetCellValue("K" + cabecera, "FechaVencimiento");
                sl.SetCellValue("L" + cabecera, "Estado");

                SLStyle estiloColumna = sl.CreateStyle();
                estiloColumna.Font.FontName = "Arial";
                estiloColumna.Font.FontSize = 12;
                estiloColumna.Font.Bold = true;
                estiloColumna.Font.FontColor = System.Drawing.Color.Black;
                estiloColumna.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightPink, System.Drawing.Color.DeepPink);
                sl.SetCellStyle("B" + cabecera, "L" + cabecera, estiloColumna);

                //recoriendo el Griv
                foreach (DataGridViewRow row in dataGridProv.Rows)
                {
                    cabecera++;
                    sl.SetCellValue("B" + cabecera, row.Cells[1].Value.ToString());
                    sl.SetCellValue("C" + cabecera, row.Cells[2].Value.ToString());
                    sl.SetCellValue("D" + cabecera, row.Cells[3].Value.ToString());
                    sl.SetCellValue("E" + cabecera, row.Cells[5].Value.ToString());
                    sl.SetCellValue("F" + cabecera, row.Cells[7].Value.ToString());
                    sl.SetCellValue("G" + cabecera, row.Cells[8].Value.ToString());
                    sl.SetCellValue("H" + cabecera, row.Cells[9].Value.ToString());//stock
                    sl.SetCellValue("I" + cabecera, row.Cells[10].Value.ToString());
                    sl.SetCellValue("J" + cabecera, row.Cells[11].Value.ToString());//precio venta
                    sl.SetCellValue("K" + cabecera, row.Cells[12].Value.ToString());
                    sl.SetCellValue("L" + cabecera, row.Cells[14].Value.ToString());

                }
                SLStyle estiloborde = sl.CreateStyle();
                estiloborde.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                estiloborde.Border.LeftBorder.Color = System.Drawing.Color.Black;
                estiloborde.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                estiloborde.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                estiloborde.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
                sl.SetCellStyle("B" + celdainicial, "L" + cabecera, estiloborde);


                sl.AutoFitColumn("B", "L");
                sl.SaveAs(@"C:\Users\Cesar Cerda\Desktop\ReporteInvenatrio.xlsx");

            }
        }
        private void txtBusquedas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = ((OpcionesDeCombo)comboOptions.SelectedItem).valor.ToString();
                if (dataGridProv.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridProv.Rows)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void txtProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
    }
}
