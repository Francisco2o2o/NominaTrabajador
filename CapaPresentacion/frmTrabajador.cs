using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using Guna.UI.WinForms;
using Guna.UI2.AnimatorNS;
using Guna.UI2.WinForms;
using LayerPresentation.FormNotificaciones;
using LayerPresentation.Utils;
using Siticone.Desktop.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static LayerPresentation.Utils.FuncionesValidaciones;

namespace CapaPresentacion
{
    public partial class frmTrabajador : Form
    {
        //Variables de busqueda
        static Boolean pasoLoad;
        public frmTrabajador()
        {
            InitializeComponent();

            #region Creacion de Columnas de la tabla
            dgvTrabajadores.Columns.Add("Numero", "N°");
            dgvTrabajadores.Columns.Add("IdTrabajador", "IdTrabajador");
            dgvTrabajadores.Columns.Add("nombreTrabajador", "Trabajador");
            dgvTrabajadores.Columns.Add("apellidoPaternoTrabajador", "Apellido Paterno");
            dgvTrabajadores.Columns.Add("apellidoMaternoTrabajador", "Apellido Materno");
            dgvTrabajadores.Columns.Add("documentoTrabajador", "Documento");
            dgvTrabajadores.Columns.Add("telefonoTrabajador", "Telefono");
            dgvTrabajadores.Columns.Add("direccionTrabajador", "Direccion");
            dgvTrabajadores.Columns.Add("correoTrabajador", "Correo");
            dgvTrabajadores.Columns.Add("EstadoTrabajador", "Estado");
            dgvTrabajadores.Columns.Add("descripcionTrabajador", "Descripcion");
            dgvTrabajadores.Columns.Add("IdEspecializacion", "IdEspecializacion");
            dgvTrabajadores.Columns.Add("nombreEspecializacion", "Especializacion");
            #endregion

            FuncionLlenarComboBoxEspecialidad(cboEspecialidad, 0, "", false);

            #region Agregar PlaceHolder a los Controles
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtNombreTrabajador, "Ingrese Nombre");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtDocumentoTrabajador, "Ingrese Documento ");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtApellidoPaternoTrabajador, "Ingrese Apellido Paterno");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtApellidoMaternoTrabajador, "Ingrese Apellido Materno");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtTelefonoTrabajador, "Ingrese Telefono");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtCorreoTrabajador, "Ingrese Correo");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtDireccionTrabajador, "Ingrese Direccion");
            #endregion

        }

        private void frmTrabajador_Load(object sender, EventArgs e)
        {
            #region Formato de Columnas de la Tabla [dgvTrabajador]
            dgvTrabajadores.Columns["IdTrabajador"].Visible = false;
            dgvTrabajadores.Columns["descripcionTrabajador"].Visible = false;
            dgvTrabajadores.Columns["IdEspecializacion"].Visible = false;

            dgvTrabajadores.Columns["Numero"].Width = 20;
            dgvTrabajadores.Columns["documentoTrabajador"].Width = 35;
            dgvTrabajadores.Columns["EstadoTrabajador"].Width = 35;
            #endregion

            FuncionesValidaciones.EstablecerFechasMesActual(dtFechaInicio, dtFechaFin);

            bool EstadoActivarBotonRegistrar = FuncionesValidaciones.FuncionPropiedadesControles(btnRegistrarTrabajador, btnActualizarTrabajador, btnLimpiarTrabajador, FuncionEstaVacioTextBox());
            btnRegistrarTrabajador.Enabled = EstadoActivarBotonRegistrar;

            //Variable de busqueda
            pasoLoad = true;
            FuncionBuscarTrabajador(dgvTrabajadores, 0);
        }

        #region Funcion Llenar ComboBox Especialidad
        public static List<Especializacion> FuncionLlenarComboBoxEspecialidad(Guna2ComboBox cbo, Int32 idEspecializacion, String nombreEspecializacion, Boolean buscar)
        {
            NegocioEspecializacion objeNegocioEspecializacion = new NegocioEspecializacion();
            List<Especializacion> lstEspecializacion = new List<Especializacion>();

            try
            {
                lstEspecializacion = objeNegocioEspecializacion.NegocioLlenarComboBoxEspecializacion(idEspecializacion, nombreEspecializacion, buscar);
                cbo.ValueMember = "IdEspecializacion";
                cbo.DisplayMember = "NombreEspecializacion";
                cbo.DataSource = lstEspecializacion;

                return lstEspecializacion;
            }
            catch (Exception ex)
            {
                Mbox.Show("Error: " + ex.Message);
                return lstEspecializacion;
            }
            finally
            {
                lstEspecializacion = null;
            }
        }
        #endregion

        #region  Validar Controles Vacios
        public bool FuncionEstaVacioTextBox()
        {
            if (txtNombreTrabajador.Text != "Ingrese Nombre" && txtDocumentoTrabajador.Text != "Ingrese Documento " && txtApellidoPaternoTrabajador.Text != "Ingrese Apellido Paterno" && txtApellidoMaternoTrabajador.Text != "Ingrese Apellido Materno" && txtTelefonoTrabajador.Text != "Ingrese Telefono" && cboEspecialidad.SelectedIndex != 0 && txtCorreoTrabajador.Text != "Ingrese Correo" && txtDireccionTrabajador.Text != "Ingrese Direccion" && txtDocumentoTrabajador.Text.Length == 8 && txtTelefonoTrabajador.Text.Length == 9)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        #endregion

        #region Validacion Evento Changed Controles
        public void FuncionEstaVacioChanged()
        {
            bool esValido = FuncionEstaVacioTextBox();
            btnLimpiarTrabajador.Visible = esValido;
            btnRegistrarTrabajador.Enabled = esValido;
        }

        private void CambiarBordeControl(object control, string texto)
        {
            if (control is GunaTextBox gunaControl)
            {
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    gunaControl.BorderColor = Color.FromArgb(192, 192, 192);
                }
                else
                {
                    gunaControl.BorderColor = Color.FromArgb(157, 31, 56);
                }
            }
            else if (control is SiticoneTextBox siticoneControl)
            {
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    siticoneControl.BorderColor = Color.FromArgb(192, 192, 192);
                }
                else
                {
                    siticoneControl.BorderColor = Color.FromArgb(157, 31, 56);
                }
            }
        }

        private void txtNombreTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionEstaVacioChanged();
            CambiarBordeControl(txtNombreTrabajador, txtNombreTrabajador.Text);
        }

        private void txtApellidoPaternoTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionEstaVacioChanged();
            CambiarBordeControl(txtApellidoPaternoTrabajador, txtApellidoPaternoTrabajador.Text);
        }

        private void txtApellidoMaternoTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionEstaVacioChanged();
            CambiarBordeControl(txtApellidoMaternoTrabajador, txtApellidoMaternoTrabajador.Text);
        }

        private void txtDireccionTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionEstaVacioChanged();
            CambiarBordeControl(txtDireccionTrabajador, txtDireccionTrabajador.Text);
        }

        private void txtDescripcionTrabajador_TextChanged(object sender, EventArgs e)
        {
            CambiarBordeControl(txtDescripcionTrabajador, txtDescripcionTrabajador.Text);
        }

        private void txtDocumentoTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionEstaVacioChanged();
            CambiarBordeControl(txtDocumentoTrabajador, txtDocumentoTrabajador.Text);
        }

        private void txtTelefonoTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionEstaVacioChanged();
            if (txtTelefonoTrabajador.Text.Length == 9 && txtIdTrabajador.Text != "")
            {
                btnActualizarTrabajador.Visible = true;
            }
            else
            {
                btnActualizarTrabajador.Visible = false;
                btnLimpiarTrabajador.Visible = true;
            }
            CambiarBordeControl(txtTelefonoTrabajador, txtTelefonoTrabajador.Text);
        }

        private void txtCorreoTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionEstaVacioChanged();
            CambiarBordeControl(txtCorreoTrabajador, txtCorreoTrabajador.Text);
        }

        private void cboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuncionEstaVacioChanged();

            if (cboEspecialidad.SelectedValue != null)
            {
                txtIdEspecializacion.Text = cboEspecialidad.SelectedValue.ToString();
            }

            if (cboEspecialidad.SelectedIndex != 0)
            {
                cboEspecialidad.ForeColor = Color.Black;
                cboEspecialidad.BorderColor = Color.Teal;
            }
            else
            {
                cboEspecialidad.ForeColor = Color.FromArgb(157, 31, 56);
                cboEspecialidad.BorderColor = Color.FromArgb(157, 31, 56);
            }
        }
        #endregion

        #region Funcion Limpiar Controles
        public void FuncionLimpiarControles()
        {
            bool EstadoActivarBotonRegistrar = FuncionesValidaciones.FuncionPropiedadesControles(btnRegistrarTrabajador, btnActualizarTrabajador, btnLimpiarTrabajador, FuncionEstaVacioTextBox());

            if (EstadoActivarBotonRegistrar || txtTelefonoTrabajador.Text != "")
            {
                LimpiarCamposTrabajador();
                ConfigurarControlesParaLimpiar();
            }
        }
        private void LimpiarCamposTrabajador()
        {
            txtNombreTrabajador.Text = "";
            txtIdTrabajador.Text = "";
            txtApellidoPaternoTrabajador.Text = "";
            txtApellidoMaternoTrabajador.Text = "";
            txtTelefonoTrabajador.Text = "";
            txtCorreoTrabajador.Text = "";
            txtDescripcionTrabajador.Text = "";
            cboEspecialidad.SelectedIndex = 0;
            chkEstadoTrabajador.Checked = true;
        }

        private void ConfigurarControlesParaLimpiar()
        {
            btnLimpiarTrabajador.Visible = false;
            PlaceholderHelper();
            btnRegistrarTrabajador.Visible = true;
            btnRegistrarTrabajador.Enabled = false;
            btnActualizarTrabajador.Visible = false;
            chkEstadoTrabajador.Enabled = false;
            txtDocumentoTrabajador.Enabled = true;
        }

        private void PlaceholderHelper()
        {
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtNombreTrabajador, "Ingrese Nombre");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtDocumentoTrabajador, "Ingrese Documento");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtApellidoPaternoTrabajador, "Ingrese Apellido Paterno");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtApellidoMaternoTrabajador, "Ingrese Apellido Materno");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtTelefonoTrabajador, "Ingrese Telefono");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtCorreoTrabajador, "Ingrese Correo");
            FuncionesValidaciones.PlaceholderHelper.fnPlaceholder(txtDireccionTrabajador, "Ingrese Direccion");
        }
        #endregion

        #region Función Registrar Trabajador
        public string FuncionRegistrarTrabajador()
        {
            var objNegocioTrabajador = new NegocioTrabajador();
            var objTrabajador = new Trabajador
            {
                IdTrabajador = string.IsNullOrWhiteSpace(txtIdTrabajador.Text) ? 0 : Convert.ToInt32(txtIdTrabajador.Text.Trim()),
                NombreTrabajador = txtNombreTrabajador.Text.ToUpper(),
                ApellidoPaternoTrabajador = txtApellidoPaternoTrabajador.Text.ToUpper(),
                ApellidoMaternoTrabajador = txtApellidoMaternoTrabajador.Text.ToUpper(),
                DescripcionTrabajador = string.IsNullOrEmpty(txtDescripcionTrabajador.Text) ? "Sin Descripcion" : txtDescripcionTrabajador.Text,
                DocumentoTrabajador = txtDocumentoTrabajador.Text,
                DireccionTrabajador = txtDireccionTrabajador.Text.ToUpper(),
                TelefonoTrabajador = txtTelefonoTrabajador.Text,
                CorreoTrabajador = txtCorreoTrabajador.Text,
                FechaRegistroTrabajador = DateTime.Now,
                EstadoTrabajador = chkEstadoTrabajador.Checked,
                IdEspecializacion = Convert.ToInt32(txtIdEspecializacion.Text)
            };

            string mensajeValidar;
            try
            {
                mensajeValidar = objNegocioTrabajador.NegocioRegistraTrabajador(objTrabajador).Trim();

                if (mensajeValidar == "OK")
                {
                    mensajeValidar = "Trabajador Registrado";
                }
                else if (mensajeValidar == "El trabajador ya está registrado.")
                {
                    mensajeValidar = "El trabajador ya está registrado.";
                }
                else
                {
                    mensajeValidar = "Error al registrar el Trabajador.";
                }
            }
            catch (Exception ex)
            {
                mensajeValidar = $"Error: {ex.Message}";
            }

            return mensajeValidar;
        }
        #endregion

        #region Funcion Actualizar Trabajador
        public string FuncionActualizarTrabajador()
        {
            var objNegocioTrabajador = new NegocioTrabajador();
            var objTrabajador = new Trabajador();
            string mensajeValidar;
            try
            {
                int idTrabajador = int.TryParse(txtIdTrabajador.Text.Trim(), out var id) ? id : 0;
                objTrabajador.IdTrabajador = idTrabajador;
                objTrabajador.NombreTrabajador = txtNombreTrabajador.Text.ToUpper();
                objTrabajador.ApellidoPaternoTrabajador = txtApellidoPaternoTrabajador.Text.ToUpper();
                objTrabajador.ApellidoMaternoTrabajador = txtApellidoMaternoTrabajador.Text.ToUpper();
                objTrabajador.DescripcionTrabajador = string.IsNullOrEmpty(txtDescripcionTrabajador.Text) ? "Sin Descripcion" : txtDescripcionTrabajador.Text;
                objTrabajador.DocumentoTrabajador = txtDocumentoTrabajador.Text;
                objTrabajador.DireccionTrabajador = txtDireccionTrabajador.Text.ToUpper();
                objTrabajador.TelefonoTrabajador = txtTelefonoTrabajador.Text;
                objTrabajador.CorreoTrabajador = txtCorreoTrabajador.Text;
                objTrabajador.FechaRegistroTrabajador = DateTime.Now;
                objTrabajador.EstadoTrabajador = chkEstadoTrabajador.Checked;
                objTrabajador.IdEspecializacion = Convert.ToInt32(txtIdEspecializacion.Text);

                mensajeValidar = objNegocioTrabajador.NegocioActualizarTrabajador(objTrabajador).Trim();

                switch (mensajeValidar)
                {
                    case "OK":
                        mensajeValidar = "Trabajador Actualizado";
                        break;
                    case "No existe un trabajador con el documento especificado.":
                        mensajeValidar = "No existe un trabajador con el documento especificado.";
                        break;
                    default:
                        mensajeValidar = "Error al Actualizar Trabajador";
                        break;
                }
            }
            catch (Exception ex)
            {
                mensajeValidar = $"Error: {ex.Message}";
            }

            return mensajeValidar;
        }
        #endregion

        #region Button Actualizar Trabajador
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            String pResult = FuncionActualizarTrabajador();
            if (pResult == "Trabajador Actualizado")
            {
                FuncionesGenerales.ShowAlert("Trabajador Actualizado", frmNotificacion.enmType.Info);
                FuncionLimpiarControles();
                FuncionTiposBusqueda(0);
            }
            else if (pResult == "No existe un trabajador con el  documento especificado.")
            {
                Mbox.Show("error:" + pResult, "Error de Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Mbox.Show("Error de Actualizacion. Comunicar al Administrador del Sistema", "Error de Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Button Registrar Trabajador
        private void btnRegistrarTrabajador_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtIdTrabajador.Text))
            {
                string pResult = FuncionRegistrarTrabajador();

                if (pResult == "Trabajador Registrado")
                {
                    FuncionesGenerales.ShowAlert("Trabajador Registrado", frmNotificacion.enmType.Info);
                    FuncionLimpiarControles();
                    FuncionTiposBusqueda(0);
                }
                else if (pResult == "El trabajador ya está registrado.")
                {
                    Mbox.Show("Error: " + pResult, "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Mbox.Show("Error al Registrar Trabajador. Comunicar al Administrador del Sistema", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        #endregion

        #region Boton Limpiar Controles
        private void btnLimpiarTrabajador_Click(object sender, EventArgs e)
        {
            FuncionLimpiarControles();
        }
        #endregion

        #region Validacion del Estado Trabajador
        private void chkEstadoTrabajador_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstadoTrabajador.Checked)
            {
                lblEstadoTrabajador.Text = "ACTIVO";
            }
            else
            {
                lblEstadoTrabajador.Text = "INACTIVO";
            }
        }
        #endregion

        #region Tipos de Busqueda 
        private void pboxBuscarTrabajador_Click(object sender, EventArgs e)
        {
            FuncionTiposBusqueda(1);
        }

        private void txtBusquedaTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;
            string mensajeError;

            int tipoCon = 1;

            if (!MetodoSoloSonNumeros.ValidarSoloNumeros(textBox.Text, tipoCon, e.KeyChar, out mensajeError))
            {
                e.Handled = true;
                Mbox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (e.KeyChar == (char)Key.Enter)
            {
                FuncionTiposBusqueda(2);
            }
        }

        private void cboPaginaTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPaginaTrabajador.Items.Count == 0)
            {
                MessageBox.Show("No hay elementos en el combo box. Por favor, verifique los datos disponibles.",
                                "Error de Paginación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else if (cboPaginaTrabajador.SelectedIndex < 0)
            {
                MessageBox.Show("No se ha seleccionado ningún elemento. Seleccione una opción.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                FuncionTiposBusqueda(3);
            }
        }
        #endregion

        #region Funcion Tipos de Busqueda
        public void FuncionTiposBusqueda(int tipoCon)
        {
            if (pasoLoad)
            {
                int pagina = tipoCon == 1 || tipoCon == 2 ? 0 : Convert.ToInt32(cboPaginaTrabajador.Text.ToString());
                bool bResult = FuncionBuscarTrabajador(dgvTrabajadores, pagina);
                if (bResult)
                {
                    FuncionesGenerales.ShowAlert("Registros encontrados", frmNotificacion.enmType.Info);
                }
                else
                {
                    Mbox.Show("Error al realizar busqueda. Comunicar al Administrador del Sistema", "Error de Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtBusquedaTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionBuscarTrabajador(dgvTrabajadores, 0);
        }
        #endregion

        #region Función Busqueda Trabajador
        private bool SegundaBusqueda = false;

        public bool FuncionBuscarTrabajador(DataGridView dgv, int numPagina)
        {
            NegocioTrabajador objNegocioTrabajador = new NegocioTrabajador();
            DataTable dtTrabajador;
            string documentoTrabajador = txtBusquedaTrabajador.Text;
            int filas = 18;
            DateTime fechaInicial = dtFechaInicio.Value;
            DateTime fechaFinal = dtFechaFin.Value;
            bool habilitarFechas = chkHabilitarFechasBusqueda.Checked;

            try
            {
                int tabInicio = (numPagina == 0) ? 0 : (numPagina - 1) * filas;
                dtTrabajador = objNegocioTrabajador.NegocioBuscarTrabajador(habilitarFechas, fechaInicial, fechaFinal, documentoTrabajador, numPagina);

                dgv.Visible = true;
                dgv.Rows.Clear();

                int totalResultados = dtTrabajador.Rows.Count;

                if (totalResultados > 0)
                {
                    int y = tabInicio;
                    foreach (DataRow item in dtTrabajador.Rows)
                    {
                        string estadoTrabajador = (bool)item["EstadoTrabajador"] ? "ACTIVO" : "INACTIVO";
                        y++;

                        dgv.Rows.Add(
                            y,
                            item["IdTrabajador"],
                            item["nombreTrabajador"],
                            item["apellidoPaternoTrabajador"],
                            item["apellidoMaternoTrabajador"],
                            item["documentoTrabajador"],
                            item["telefonoTrabajador"],
                            item["direccionTrabajador"],
                            item["correoTrabajador"],
                            estadoTrabajador,
                            item["descripcionTrabajador"],
                            item["IdEspecializacion"],
                            item["nombreEspecializacion"]
                        );
                    }

                    if (numPagina == 0 && !SegundaBusqueda)
                    {
                        int totalRegistros = Convert.ToInt32(dtTrabajador.Rows[0][0]);
                        FuncionesGenerales.CalcularPaginacion(totalRegistros, filas, totalResultados, cboPaginaTrabajador, btnTotalPaginasTrabajador, btnNumFilasTrabajador, btnTotalRegistrosTrabajador);
                        SegundaBusqueda = true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Mbox.Show("Error: " + ex.Message, "Error de Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        #endregion

        #region Enviar Datos de la Tabla a los Controles
        private void dgvTrabajadores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvTrabajadores.Rows[e.RowIndex];

                int idTrabajador = (int)selectedRow.Cells["IdTrabajador"].Value;
                string nombreTrabajador = selectedRow.Cells["nombreTrabajador"].Value.ToString();
                string apellidoPaterno = selectedRow.Cells["apellidoPaternoTrabajador"].Value.ToString();
                string apellidoMaterno = selectedRow.Cells["apellidoMaternoTrabajador"].Value.ToString();
                string documento = selectedRow.Cells["documentoTrabajador"].Value.ToString();
                string telefono = selectedRow.Cells["telefonoTrabajador"].Value.ToString();
                string direccion = selectedRow.Cells["direccionTrabajador"].Value.ToString();
                string correo = selectedRow.Cells["correoTrabajador"].Value.ToString();
                string estadoTrabajador = selectedRow.Cells["EstadoTrabajador"].Value.ToString();
                string descripcion = selectedRow.Cells["descripcionTrabajador"].Value.ToString();
                int idEspecializacion = (int)selectedRow.Cells["IdEspecializacion"].Value;
                string nombreEspecializacion = selectedRow.Cells["nombreEspecializacion"].Value.ToString();

                if (documento.Length != 8)
                {
                    Mbox.Show("El documento debe tener exactamente 8 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnLimpiarTrabajador.Visible = false;
                    btnActualizarTrabajador.Visible = false;
                    btnRegistrarTrabajador.Visible = true;
                    return;

                }

                if (telefono.Length != 9)
                {
                    Mbox.Show("El teléfono debe tener exactamente 9 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnLimpiarTrabajador.Visible = false;
                    btnActualizarTrabajador.Visible = false;
                    btnRegistrarTrabajador.Visible = true;
                    return;

                }
                txtIdTrabajador.Text = idTrabajador.ToString();
                txtNombreTrabajador.Text = nombreTrabajador;
                txtApellidoPaternoTrabajador.Text = apellidoPaterno;
                txtApellidoMaternoTrabajador.Text = apellidoMaterno;
                txtDocumentoTrabajador.Text = documento;
                txtTelefonoTrabajador.Text = telefono;
                txtDireccionTrabajador.Text = direccion;
                txtCorreoTrabajador.Text = correo;
                chkEstadoTrabajador.Checked = estadoTrabajador.Equals("ACTIVO", StringComparison.OrdinalIgnoreCase);
                txtDescripcionTrabajador.Text = descripcion;
                cboEspecialidad.SelectedValue = idEspecializacion;

                btnRegistrarTrabajador.Visible = false;
                btnActualizarTrabajador.Visible = true;
                chkEstadoTrabajador.Enabled = true;
                txtDocumentoTrabajador.Enabled = false;
            }

        }


        #endregion

        #region Validacion Habilitar Fechas de Busqueda
        private void chkHabilitarFechasBusqueda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHabilitarFechasBusqueda.Checked)
            {
                gbRangoFechas.Enabled = true;
            }
            else
            {
                gbRangoFechas.Enabled = false;
            }
        }
        #endregion

        #region Eliminar Registro Trabajador
        private void eliminarRegistroTrabajador_Click(object sender, EventArgs e)
        {
            if (dgvTrabajadores.SelectedRows.Count > 0)
            {
                DialogResult resultado = Mbox.Show("¿Estás seguro de que deseas eliminar el Registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Int32 IdTrabajador = Convert.ToInt32(dgvTrabajadores.SelectedRows[0].Cells["IdTrabajador"].Value);
                    Int32 DocumentoTrabajador = Convert.ToInt32(dgvTrabajadores.SelectedRows[0].Cells["documentoTrabajador"].Value);
                    NegocioTrabajador objNegocioTrabajador = new NegocioTrabajador();
                    try
                    {
                        String mensajeValidar = objNegocioTrabajador.NegocioEliminarTrabajador(IdTrabajador, DocumentoTrabajador);
                        if (mensajeValidar == "OK")
                        {
                            FuncionesGenerales.ShowAlert("Registro Eliminado", frmNotificacion.enmType.Info);
                            FuncionBuscarTrabajador(dgvTrabajadores, 0);
                        }
                        else if (mensajeValidar == "El trabajador no existe.")
                        {
                            Mbox.Show("Error: " + mensajeValidar, "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (mensajeValidar == "'El trabajador ya se encuentra inactivo")
                        {
                            Mbox.Show("Error: " + mensajeValidar, "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            Mbox.Show("Error: " + mensajeValidar, "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    catch (Exception ex)
                    {
                        Mbox.Show($"Error al Eliminar Registro de Trabajador: {ex.Message}", "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
        #endregion

        #region  Manejo de Eventos de Validación de Documentos
        private void txtDocumentoTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            GunaTextBox textBox = sender as GunaTextBox;
            string mensajeError;

            int tipoCon = 1;

            if (!MetodoSoloSonNumeros.ValidarSoloNumeros(textBox.Text, tipoCon, e.KeyChar, out mensajeError))
            {
                e.Handled = true;
                Mbox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region  Manejo de Eventos de Validación de Telefono
        private void txtTelefonoTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            GunaTextBox textBox = sender as GunaTextBox;
            string mensajeError;

            int tipoCon = 2;

            if (!MetodoSoloSonNumeros.ValidarSoloNumeros(textBox.Text, tipoCon, e.KeyChar, out mensajeError))
            {
                e.Handled = true;
                Mbox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region  Manejo de Eventos de Validación de NombreTrabajador
        private void txtNombreTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            GunaTextBox textBox = sender as GunaTextBox;

            if (!MetodoSoloSonLetrasYEspacios.ValidarSoloLetrasYEspacios(textBox.Text, e.KeyChar, out string mensajeError))
            {
                e.Handled = true;
                Mbox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region  Manejo de Eventos de Validación de ApellidoPaternoTrabajador
        private void txtApellidoPaternoTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            GunaTextBox textBox = sender as GunaTextBox;

            if (!MetodoSoloSonLetrasYEspacios.ValidarSoloLetrasYEspacios(textBox.Text, e.KeyChar, out string mensajeError))
            {
                e.Handled = true;
                Mbox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region  Manejo de Eventos de Validación de ApellidoMaternoTrabajador
        private void txtApellidoMaternoTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            GunaTextBox textBox = sender as GunaTextBox;

            if (!MetodoSoloSonLetrasYEspacios.ValidarSoloLetrasYEspacios(textBox.Text, e.KeyChar, out string mensajeError))
            {
                e.Handled = true;
                Mbox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

    }
}
