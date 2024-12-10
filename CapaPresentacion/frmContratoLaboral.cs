using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using Guna.UI.WinForms;
using Guna.UI2.WinForms;
using LayerPresentation.FormNotificaciones;
using LayerPresentation.Utils;
using Siticone.Desktop.UI.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static CapaNegocio.MetodosContratoLaboral;

namespace CapaPresentacion
{
    public partial class frmContratoLaboral : Form
    {
        //Variables de busqueda
        static Boolean pasoLoad;
       

        public frmContratoLaboral()
        {
            InitializeComponent();

            #region Creacion de las columnas de la tabla [dgvContratoLaboral]
            dgvContratoLaboral.Columns.Add("Numero", "N°");
            dgvContratoLaboral.Columns.Add("IdContratoLaboral", "ID del Contrato Laboral");
            dgvContratoLaboral.Columns.Add("fechaInicioContratoLaboral", "Fecha de Inicio");
            dgvContratoLaboral.Columns.Add("fechaFinContratoLaboral", "Fecha de Fin");
            dgvContratoLaboral.Columns.Add("horasTotalesContratoLaboral", "Horas Totales");
            dgvContratoLaboral.Columns.Add("EstadoContratoLaboral", "Estado");
            dgvContratoLaboral.Columns.Add("descripcionContratoLaboral", "Descripción");
            dgvContratoLaboral.Columns.Add("IdCargoContratoLaboral", "ID del Cargo");
            dgvContratoLaboral.Columns.Add("nombreCargo", "Nombre del Cargo");
            dgvContratoLaboral.Columns.Add("IdTipoContratoLaboral", "ID del Tipo de Contrato");
            dgvContratoLaboral.Columns.Add("nombreTipoContrato", "Tipo de Contrato");
            dgvContratoLaboral.Columns.Add("IdTrabajador", "ID del Trabajador");
            dgvContratoLaboral.Columns.Add("nombreTrabajador", "Trabajador");
            dgvContratoLaboral.Columns.Add("salarioContratoLaboral", "Salario");
            dgvContratoLaboral.Columns.Add("horasDiariasContratoLaboral", "Horas Diarias");
            dgvContratoLaboral.Columns.Add("AsignaciónFamiliarContratoLaboral", "Asignación Familia");
            #endregion


            #region Llenar ComboBox´s
            FuncionLlenarComboBoxCargo(cboCargo, 0, "", false);
            FuncionLlenarComboBoxTipoContratoLaboral(cboTipoContrato, 0, "", false);
            FuncionLlenarComboBoxTrabajador(cboTrabajador, 0, "", false);
            #endregion

        }
        private string mensajeError;
        private void frmContratoLaboral_Load(object sender, EventArgs e)
        {
            rbContratoDoceMeses.Checked = true;
            FuncionesValidaciones.EstablecerFechasMesActual(dtFechaInicio, dtFechaFin);

            bool EstadoActivarBotonRegistrar = FuncionesValidaciones.FuncionPropiedadesControles(btnRegistrarContratoLaboral, btnActualizarContratoLaboral, btnLimpiarContratoLaboral, FuncionValidarTextBox());
            btnRegistrarContratoLaboral.Enabled = EstadoActivarBotonRegistrar;


            #region Visibilidad de Columnas de la Tabla [dgvContratoLaboral]
            dgvContratoLaboral.Columns["IdContratoLaboral"].Visible = false;
            dgvContratoLaboral.Columns["IdCargoContratoLaboral"].Visible = false;
            dgvContratoLaboral.Columns["IdTrabajador"].Visible = false;
            dgvContratoLaboral.Columns["IdTipoContratoLaboral"].Visible = false;
            dgvContratoLaboral.Columns["descripcionContratoLaboral"].Visible = false;
            dgvContratoLaboral.Columns["AsignaciónFamiliarContratoLaboral"].Visible = false;
            #endregion

            //Variable de busqueda
            pasoLoad = true;
            FuncionBuscarContratoLaboral(dgvContratoLaboral, 0, out mensajeError);
        }

        #region  Validar Controles Vacios
        public bool FuncionValidarTextBox()
        {
            if (cboTrabajador.SelectedIndex != 0 && cboTipoContrato.SelectedIndex != 0 && cboCargo.SelectedIndex != 0 && txtHorasTotalesContratoLaboral.Text != "")
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        #endregion

        #region Funcion Validar Changed
        public void FuncionValidarChanged()
        {
            bool esValido = FuncionValidarTextBox();
            btnLimpiarContratoLaboral.Visible = esValido;
            btnRegistrarContratoLaboral.Enabled = esValido;
            btnActualizarContratoLaboral.Visible = esValido;
        }
        #endregion

        #region Funcion Llenar ComboBox Cargo
        public static List<CargoContratoLaboral> FuncionLlenarComboBoxCargo(Guna2ComboBox cbo, Int32 idCargo, String nombreCargo, Boolean buscar)
        {
            NegocioCargoContratoLaboral objeNegocioCargo = new NegocioCargoContratoLaboral();
            List<CargoContratoLaboral> lstCargo = new List<CargoContratoLaboral>();

            try
            {
                lstCargo = objeNegocioCargo.NegocioLlenarComboBoxCargo(idCargo, nombreCargo, buscar);
                cbo.ValueMember = "IdCargoContratoLaboral";
                cbo.DisplayMember = "NombreCargoContratoLaboral";
                cbo.DataSource = lstCargo;

                return lstCargo;
            }
            catch (Exception ex)
            {
                Mbox.Show("Error al llenar el ComboBox: " + ex.Message);
                return lstCargo;
            }
           
        }
        #endregion

        #region Funcion Llenar ComboBox Tipo Contrato Laboral
        public static List<TipoContratoLaboral> FuncionLlenarComboBoxTipoContratoLaboral(Guna2ComboBox cbo, Int32 idTipoContratoLaboral, String nombreTipoContratoLaboral, Boolean buscar)
        {
            NegocioTipoContratoLaboral objNegocioTipoContratoLaboral = new NegocioTipoContratoLaboral();
            List<TipoContratoLaboral> lstTipoContratoLaboral = new List<TipoContratoLaboral>();

            try
            {
                lstTipoContratoLaboral = objNegocioTipoContratoLaboral.NegocioLlenarComboBoxTipoContratoLaboral(idTipoContratoLaboral, nombreTipoContratoLaboral, buscar);
                cbo.ValueMember = "IdTipoContratoLaboral";
                cbo.DisplayMember = "NombreTipoContratoLaboral";
                cbo.DataSource = lstTipoContratoLaboral;

                return lstTipoContratoLaboral;
            }
            catch (Exception ex)
            {
                Mbox.Show("Error al llenar el ComboBox: " + ex.Message);
                return lstTipoContratoLaboral;
            }
        
        }
        #endregion

        #region Funcion Llenar ComboBox Trabajador
        public static List<Trabajador> FuncionLlenarComboBoxTrabajador(Guna2ComboBox cbo, Int32 idTrabajador, String nombreTrabajador, Boolean buscar)
        {
            NegocioTrabajador objNegocioTrabajador = new NegocioTrabajador();
            List<Trabajador> lstTrabajador = new List<Trabajador>();

            try
            {
                lstTrabajador = objNegocioTrabajador.NegocioLlenarComboBoxTrabajador(idTrabajador, nombreTrabajador, buscar);

                cbo.ValueMember = "IdTrabajador";
                cbo.DisplayMember = "NombreCompletoTrabajador";
                cbo.DataSource = lstTrabajador;

                return lstTrabajador;
            }
            catch (Exception ex)
            {
                string mensajeError = $"Se produjo un error: {ex.Message}\nDetalles: {ex.StackTrace}";
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return lstTrabajador;
            }
        }

        #endregion

        #region Validacion Evento Changed de los Controles

        #region Mostrar Salario por Tipo de Contrato
        private void cboTipoContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoContratoLaboral selectedTipoContratoLaboral = (TipoContratoLaboral)cboTipoContrato.SelectedItem;

            if (selectedTipoContratoLaboral != null)
            {
                txtSalarioContratoLaboral.Text = selectedTipoContratoLaboral.SalarioBaseTipoContratoLaboral.ToString();

                if (selectedTipoContratoLaboral.IdTipoContratoLaboral == 0)
                {
                    txtIdTipoContratoLaboral.Text = selectedTipoContratoLaboral.IdTipoContratoLaboral.ToString();
                    txtHorasDiariasContratoLaboral.Text = "1";
                    txtHorasTotalesContratoLaboral.Text = "30";
                    txtSalarioContratoLaboral.Text = "800";
                }

                else if (selectedTipoContratoLaboral.IdTipoContratoLaboral == 1)
                {
                    txtIdTipoContratoLaboral.Text = selectedTipoContratoLaboral.IdTipoContratoLaboral.ToString();
                    txtHorasDiariasContratoLaboral.Enabled = false;
                    txtHorasTotalesContratoLaboral.Enabled = false;
                    txtSalarioContratoLaboral.Enabled = false;
                    txtHorasDiariasContratoLaboral.Text = "8";
                    txtHorasTotalesContratoLaboral.Text = "160";
                    btnActualizarContratoLaboral.Enabled = true;
                }
                else if (selectedTipoContratoLaboral.IdTipoContratoLaboral == 2)
                {
                    txtIdTipoContratoLaboral.Text = selectedTipoContratoLaboral.IdTipoContratoLaboral.ToString();
                    txtHorasDiariasContratoLaboral.Enabled = false;
                    txtHorasTotalesContratoLaboral.Enabled = false;
                    txtSalarioContratoLaboral.Enabled = false;
                    txtHorasDiariasContratoLaboral.Text = "4";
                    txtHorasTotalesContratoLaboral.Text = "80";
                    btnActualizarContratoLaboral.Enabled = true;
                }
                else if (selectedTipoContratoLaboral.IdTipoContratoLaboral == 3)
                {
                    txtIdTipoContratoLaboral.Text = selectedTipoContratoLaboral.IdTipoContratoLaboral.ToString();
                    txtHorasDiariasContratoLaboral.Enabled = true;
                    txtHorasTotalesContratoLaboral.Enabled = true;
                    txtSalarioContratoLaboral.Enabled = true;
                    txtHorasDiariasContratoLaboral.Text = "1";
                    txtHorasTotalesContratoLaboral.Text = "30";
                    btnLimpiarContratoLaboral.Visible = true;
                }
            }
            else
            {
                txtIdTipoContratoLaboral.Text = string.Empty;
                txtHorasDiariasContratoLaboral.Enabled = false;
                txtHorasTotalesContratoLaboral.Enabled = false;
                txtSalarioContratoLaboral.Enabled = false;
                txtSalarioContratoLaboral.Text = string.Empty;
                txtHorasDiariasContratoLaboral.Text = string.Empty;
                txtHorasTotalesContratoLaboral.Text = string.Empty;
                btnLimpiarContratoLaboral.Visible = false;
            }

            FuncionValidarChanged();
            if (cboTipoContrato.SelectedValue != null)
            {
                cboTipoContrato.BorderColor = Color.FromArgb(192, 192, 192);
            }
            else
            {
                cboTipoContrato.BorderColor = Color.FromArgb(157, 31, 56);
            }
        }

        #endregion

        #region Mostrar Especialidad por Trabajador
        private void cboTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trabajador selectedTrabajador = (Trabajador)cboTrabajador.SelectedItem;

            if (selectedTrabajador != null)
            {
                txtEspecialidadtrabajador.Text = selectedTrabajador.NombreEspecializacion.ToString();
                txtIdTrabajador.Text = selectedTrabajador.IdTrabajador.ToString();

            }
            else
            {
                txtSalarioContratoLaboral.Text = string.Empty;


            }
            FuncionValidarChanged();
            if (cboTrabajador.SelectedValue != null)
            {
                cboTrabajador.BorderColor = Color.FromArgb(192, 192, 192);
            }
            else
            {
                cboTrabajador.BorderColor = Color.FromArgb(157, 31, 56);
            }
        }
        #endregion

        //Evento Changed txtEspecialidadtrabajador
        private void txtEspecialidadtrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionValidarChanged();
            if (!string.IsNullOrWhiteSpace(txtEspecialidadtrabajador.Text))
            {
                txtEspecialidadtrabajador.BorderColor = Color.FromArgb(192, 192, 192);
            }
            else
            {
                txtEspecialidadtrabajador.BorderColor = Color.FromArgb(157, 31, 56);
            }
        }

        //Evento Changed cboCargo
        private void cboCargo_SelectedIndexChanged(object sender, EventArgs e)
            {
            FuncionValidarChanged();
            CargoContratoLaboral selectedCargoContratoLaboral = (CargoContratoLaboral)cboCargo.SelectedItem;

            if (selectedCargoContratoLaboral != null)
            {
                txtIdCargo.Text = selectedCargoContratoLaboral.IdCargoContratoLaboral.ToString();
                btnActualizarContratoLaboral.Visible = true;
               
            }
            else
            {
                txtIdCargo.Text = string.Empty;
            }

            if (cboCargo.SelectedValue != null)
            {
                cboCargo.BorderColor = Color.FromArgb(192, 192, 192);
            }
            else
            {
                cboCargo.BorderColor = Color.FromArgb(157, 31, 56);
            }
        }

        //Evento Changed txtHorasTotalesContratoLaboral - Validar txtHorasDiariasContratoLaboral vacio
        private void txtHorasTotalesContratoLaboral_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHorasDiariasContratoLaboral.Text) ||
    string.IsNullOrWhiteSpace(txtHorasTotalesContratoLaboral.Text))
            {
                btnActualizarContratoLaboral.Enabled = false;
            }
            else
            {
                btnActualizarContratoLaboral.Enabled = true;
            }

            FuncionValidarChanged();
            ActualizarSalarioPorHora();
        }

        //Evento Changed txtSalarioContratoLaboral
        private void txtSalarioContratoLaboral_TextChanged(object sender, EventArgs e)
        {
            if (txtSalarioContratoLaboral.Text == "0")
            {
                txtSalarioContratoLaboral.Text = "800";
            }
            FuncionValidarChanged();
            ActualizarSalarioPorHora();
        }

        //Evento Changed txtSalarioxHoraContratoLaboral
        private void txtSalarioxHoraContratoLaboral_TextChanged(object sender, EventArgs e)
        {
            FuncionValidarChanged();
            if (!string.IsNullOrWhiteSpace(txtSalarioxHoraContratoLaboral.Text))
            {
                txtSalarioxHoraContratoLaboral.BorderColor = Color.FromArgb(192, 192, 192);
            }
            else
            {
                txtSalarioxHoraContratoLaboral.BorderColor = Color.FromArgb(157, 31, 56);
            }
        }

        // Evento changed chkEstadoTrabajador
        private void chkEstadoTrabajador_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstadoContratoLaboral.Checked)
            {
                lblEstadoContratoLaboral.Text = "ACTIVO";
            }
            else
            {
                lblEstadoContratoLaboral.Text = "INACTIVO";
            }
        }
        #endregion

        #region Funcion para Actualizar el Salario por Horas
        private void ActualizarColorBordesSalario(bool salarioEsValido, bool horasSonValidas)
        {
            txtHorasTotalesContratoLaboral.BorderColor = horasSonValidas
                ? Color.FromArgb(192, 192, 192)
                : Color.FromArgb(157, 31, 56);

            txtSalarioContratoLaboral.BorderColor = salarioEsValido
                ? Color.FromArgb(192, 192, 192)
                : Color.FromArgb(157, 31, 56);
        }

        #endregion

        #region Método para Actualizar y Calcular Salario por Hora
        private void ActualizarSalarioPorHora()
        {

            if (string.IsNullOrWhiteSpace(txtHorasTotalesContratoLaboral.Text) || string.IsNullOrWhiteSpace(txtHorasDiariasContratoLaboral.Text))
            {
                txtSalarioxHoraContratoLaboral.Text = "0.00";
                return;
            }

            bool salarioEsValido = decimal.TryParse(txtSalarioContratoLaboral.Text, out decimal salarioTotal);
            bool horasSonValidas = int.TryParse(txtHorasTotalesContratoLaboral.Text, out int horasTotales);

            CalcularSalarioContratoLaboral calcularSalario = new CalcularSalarioContratoLaboral();

            if (salarioEsValido && horasSonValidas && calcularSalario.CalcularSalarioPorHora(salarioTotal, horasTotales, out decimal salarioPorHora))
            {
                if (salarioPorHora == 0)
                {
                    salarioPorHora = 1;
                }
                txtSalarioxHoraContratoLaboral.Text = salarioPorHora.ToString("F2");
            }

            ActualizarColorBordesSalario(salarioEsValido, horasSonValidas);
        }
        #endregion

        #region Button Registrar ContratoLaboral
        private void btnRegistrarContratoLaboral_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtIdContratoLaboral.Text) && chkEstadoContratoLaboral.Checked)
            {
                String pResult = funcionRegistrarContratoLaboral();

                if (pResult == "Contrato Registrado")
                {
                    FuncionesGenerales.ShowAlert("Contrato Registrado", frmNotificacion.enmType.Info);
                    FuncionLimpiarControles();
                    FuncionTiposBusqueda(0);
                }
                else if (pResult == "Ya tiene un contrato Vigente")
                {
                    Mbox.Show("Ya tiene un contrato Vigente. No se puede registrar un contrato nuevo hasta que el contrato actual finalice.", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Mbox.Show("Error al Registrar Contrato. Comunicar al Administrador del Sistema", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (!chkEstadoContratoLaboral.Checked)
            {
                Mbox.Show("No se puede guardar un Contrato Inactivo. Comunicar al Administrador del Sistema", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Función Registrar ContratoLaboral
        public String funcionRegistrarContratoLaboral()
        {
            NegocioContratoLaboral objNegocioContratoLaboral = new NegocioContratoLaboral();
            ContratoLaboral objContratoLaboral = new ContratoLaboral();

            if (txtDescripcionContratoLaboral.Text == "Ingrese una breve descripcion")
            {
                txtDescripcionContratoLaboral.Text = "";
            }

            try
            {
                objContratoLaboral.IdContratoLaboral = Convert.ToInt32(txtIdContratoLaboral.Text.Trim() == "" ? "0" : txtIdContratoLaboral.Text.Trim());
                objContratoLaboral.FechaInicioContratoLaboral = dtFechaInicioContrato.Value;
                objContratoLaboral.FechaFinContratoLaboral = dtFechaFinContrato.Value;
                objContratoLaboral.HorasTotalesContratoLaboral = Convert.ToInt32(txtHorasTotalesContratoLaboral.Text);
                objContratoLaboral.FechaRegistroContratoLaboral = DateTime.Now;
                objContratoLaboral.EstadoContratoLaboral = chkEstadoContratoLaboral.Checked;
                objContratoLaboral.DescripcionContratoLaboral = txtDescripcionContratoLaboral.Text;
                objContratoLaboral.IdCargoContratoLaboral = Convert.ToInt32(txtIdCargo.Text.Trim());
                objContratoLaboral.IdTipoContratoLaboral = Convert.ToInt32(txtIdTipoContratoLaboral.Text.Trim());
                objContratoLaboral.IdTrabajador = Convert.ToInt32(txtIdTrabajador.Text.Trim());
                objContratoLaboral.SalarioContratoLaboral = decimal.TryParse(txtSalarioContratoLaboral.Text, out decimal salario) ? salario : 0.00m;
                objContratoLaboral.HorasDiariasContratoLaboral = Convert.ToInt32(txtHorasDiariasContratoLaboral.Text);
                objContratoLaboral.AsignaciónFamiliarContratoLaboral = decimal.TryParse(txtMontoAsignacionFamiliar.Text, out decimal AsignaciónFamiliar) ? AsignaciónFamiliar : 0.00m;

                string mensajeValidar = objNegocioContratoLaboral.NegocioRegistrarContratoLaboral(objContratoLaboral).Trim();

                if (mensajeValidar == "OK")
                {
                    return "Contrato Registrado";
                }
                else if (mensajeValidar == "El trabajador ya tiene un contrato vigente." || mensajeValidar == "El trabajador ya tiene un contrato activo que se solapa con las fechas proporcionadas.")
                {
                    return "Ya tiene un contrato Vigente";
                }
                else
                {
                    return "Error al registrar el contrato.";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        #endregion

        #region Funcion Actualizar ContratoLaboral
        public String funcionActualizarContratoLaboral()
        {
            NegocioContratoLaboral objNegocioContratoLaboral = new NegocioContratoLaboral();
            ContratoLaboral objContratoLaboral = new ContratoLaboral();
            try
            {
                objContratoLaboral.IdContratoLaboral = Convert.ToInt32(txtIdContratoLaboral.Text.Trim() == "" ? "0" : txtIdContratoLaboral.Text.Trim());
                objContratoLaboral.FechaInicioContratoLaboral = dtFechaInicioContrato.Value;
                objContratoLaboral.FechaFinContratoLaboral = dtFechaFinContrato.Value;
                objContratoLaboral.HorasTotalesContratoLaboral = Convert.ToInt32(txtHorasTotalesContratoLaboral.Text);
                objContratoLaboral.FechaRegistroContratoLaboral = DateTime.Now;
                objContratoLaboral.EstadoContratoLaboral = chkEstadoContratoLaboral.Checked;
                objContratoLaboral.DescripcionContratoLaboral = txtDescripcionContratoLaboral.Text;
                objContratoLaboral.IdCargoContratoLaboral = Convert.ToInt32(txtIdCargo.Text.Trim());
                objContratoLaboral.IdTipoContratoLaboral = Convert.ToInt32(txtIdTipoContratoLaboral.Text.Trim());
                objContratoLaboral.IdTrabajador = Convert.ToInt32(txtIdTrabajador.Text.Trim());
                objContratoLaboral.SalarioContratoLaboral = decimal.TryParse(txtSalarioContratoLaboral.Text, out decimal salario) ? salario : 0.00m;
                objContratoLaboral.HorasDiariasContratoLaboral = Convert.ToInt32(txtHorasDiariasContratoLaboral.Text);
                objContratoLaboral.AsignaciónFamiliarContratoLaboral = decimal.TryParse(txtMontoAsignacionFamiliar.Text, out decimal AsignaciónFamiliar) ? AsignaciónFamiliar : 0.00m;

                string mensajeValidar = objNegocioContratoLaboral.NegocioActuailzarContratoLaboral(objContratoLaboral).Trim();

                if (mensajeValidar == "OK")
                {
                    return "Contrato Actualizado";
                }
                else
                {
                    return "Error al Actualizar el Contrato.";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }



        #endregion

        #region Button Actualizar ContratoLaboral
        private void btnActualizarContratoLaboral_Click(object sender, EventArgs e)
        {
            if (cboCargo.SelectedIndex != 0)
            {
                String pResult = funcionActualizarContratoLaboral();

                if (pResult == "Contrato Actualizado")
                {
                    FuncionesGenerales.ShowAlert("Contrato Actualizado", frmNotificacion.enmType.Info);
                    FuncionLimpiarControles();
                    FuncionTiposBusqueda(0);
                }
                else
                {
                    Mbox.Show(pResult, "Error de Actualizacion. Comunicar al Administrador del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                Mbox.Show("Error al Actualizar Contrato. Comunicar al Administrador del Sistema", "Error de Actualzacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region Funcion Limpiar Controles
        public void FuncionLimpiarControles()
        {
            bool EstadoActivarBotonRegistrar = FuncionesValidaciones.FuncionPropiedadesControles(btnRegistrarContratoLaboral, btnActualizarContratoLaboral, btnLimpiarContratoLaboral, FuncionValidarTextBox());

            if (EstadoActivarBotonRegistrar)
            {
                cboTrabajador.SelectedIndex = 0;
                txtIdContratoLaboral.Text = "";
                txtIdTrabajador.Text = "";
                txtEspecialidadtrabajador.Text = "";
                cboCargo.SelectedIndex = 0;
                txtIdCargo.Text = "";
                cboTipoContrato.SelectedIndex = 0;
                txtIdTipoContratoLaboral.Text = "";
                txtHorasTotalesContratoLaboral.Text = "30";
                txtHorasDiariasContratoLaboral.Text = "1";
                txtSalarioContratoLaboral.Text = "800";
                txtSalarioxHoraContratoLaboral.Text = "0.01";
                txtDescripcionContratoLaboral.Text = "";
                chkEstadoContratoLaboral.Checked = true;
                btnLimpiarContratoLaboral.Visible = false;
                btnRegistrarContratoLaboral.Visible = true;
                btnRegistrarContratoLaboral.Enabled = false;
                btnActualizarContratoLaboral.Visible = false;
                chkEstadoContratoLaboral.Enabled = false;
                chkAsignaciónFamiliarContratoLaboral.Checked = false;
            }
        }
        #endregion

        #region Button Limpiar Controles
        private void btnLimpiarContratoLaboral_Click(object sender, EventArgs e)
        {
            FuncionLimpiarControles();
        }
        #endregion

        #region Funcion Tipos de Busqueda
        public void FuncionTiposBusqueda(Int32 tipoCon)
        {
            Boolean bResult;

            if (tipoCon == 1)
            {
                if (pasoLoad)
                {
                    bResult = FuncionBuscarContratoLaboral(dgvContratoLaboral, 0, out mensajeError);
                    if (bResult)
                    {
                        FuncionesGenerales.ShowAlert("Registros encontrados", frmNotificacion.enmType.Info);
                    }
                    else
                    {
                        Mbox.Show(mensajeError, "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else if (tipoCon == 2)
            {
                if (pasoLoad)
                {
                    bResult = FuncionBuscarContratoLaboral(dgvContratoLaboral, 0, out mensajeError);
                    if (bResult)
                    {
                        FuncionesGenerales.ShowAlert("Registros encontrados", frmNotificacion.enmType.Info);
                    }
                    else
                    {
                        Mbox.Show(mensajeError, "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                if (pasoLoad)
                {
                    if (Int32.TryParse(cboPaginaContratoLaboral.Text, out int numPagina))
                    {
                        bResult = FuncionBuscarContratoLaboral(dgvContratoLaboral, numPagina, out mensajeError);
                        if (bResult)
                        {
                            FuncionesGenerales.ShowAlert("Registros encontrados", frmNotificacion.enmType.Info);
                        }
                        else
                        {
                            Mbox.Show(mensajeError, "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        Mbox.Show("Por favor, ingrese un número válido en la página.", "Entrada no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        #endregion

        #region Función Búsqueda Contrato Laboral
        private bool SegundaBusqueda = false;
        static Int32 tabInicio = 0;
        public Boolean FuncionBuscarContratoLaboral(DataGridView dgv, Int32 numPagina, out string mensajeError)
        {
            mensajeError = string.Empty;
            NegocioContratoLaboral objNegocioContratoLaboral = new NegocioContratoLaboral();
           
            String documentoTrabajador;
            Int32 filas = 18;
            DateTime fechaInicial = dtFechaInicio.Value;
            DateTime fechaFinal = dtFechaFin.Value;
            Boolean habilitarFechas = chkHabilitarFechasBusqueda.Checked;

            try
            {
                DataTable dtContratoLaboral;
                documentoTrabajador = txtBusquedaContratoLaboralTrabajador.Text;
                dtContratoLaboral = objNegocioContratoLaboral.NegocioBuscarContratoLaboral(habilitarFechas, fechaInicial, fechaFinal, documentoTrabajador, numPagina);

                dgvContratoLaboral.Visible = true;
                dgvContratoLaboral.Rows.Clear();
                Int32 totalResultados = dtContratoLaboral.Rows.Count;

                #region Tamaño de Columnas de la Tabla [dgvContratoLaboral]
                dgvContratoLaboral.Columns["Numero"].Width = 25;
                dgvContratoLaboral.Columns["EstadoContratoLaboral"].Width = 60;
                dgvContratoLaboral.Columns["nombreTrabajador"].Width = 170;
                #endregion

                Int32 y;
                if (numPagina == 0)
                {
                    y = 0;
                }
                else
                {
                    tabInicio = (numPagina - 1) * filas;
                    y = tabInicio;
                }

                foreach (DataRow item in dtContratoLaboral.Rows)
                {
                    y++;
                    string estado = (bool)item["EstadoContratoLaboral"] ? "ACTIVO" : "INACTIVO";
                    string nombreCompleto = $"{item["nombreTrabajador"]} {item["apellidoPaternoTrabajador"]} {item["apellidoMaternoTrabajador"]}";
                    string fechaInicio = Convert.ToDateTime(item["fechaInicioContratoLaboral"]).ToString("dd/MM/yyyy");
                    string fechaFin = Convert.ToDateTime(item["fechaFinContratoLaboral"]).ToString("dd/MM/yyyy");

                    dgvContratoLaboral.Rows.Add(
                        y,
                        item["IdContratoLaboral"],
                        fechaInicio,
                        fechaFin,
                        item["horasTotalesContratoLaboral"],
                        estado,
                        item["descripcionContratoLaboral"],
                        item["IdCargoContratoLaboral"],
                        item["nombreCargo"],
                        item["IdTipoContratoLaboral"],
                        item["nombreTipoContrato"],
                        item["IdTrabajador"],
                        nombreCompleto,
                        item["salarioContratoLaboral"],
                        item["horasDiariasContratoLaboral"],
                        item["AsignaciónFamiliarContratoLaboral"]
                    );
                }

                if (numPagina == 0 && !SegundaBusqueda)
                {
                    Int32 totalRegistros = Convert.ToInt32(dtContratoLaboral.Rows[0][0]);
                    FuncionesGenerales.CalcularPaginacion(totalRegistros, filas, totalResultados, cboPaginaContratoLaboral, btnTotalPaginasContratoLaboral, btnNumFilasContratoLaboral, btnTotalRegistrosContratoLaboral);
                    SegundaBusqueda = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No hay ninguna fila en la posición 0"))
                {
                    mensajeError = "Error: No hay Contratos Registrados.";
                }
                else if (ex.Message.Contains("Cannot drop the table '#TempTrabajador"))
                {
                    mensajeError = "No se encontraron registros de Contratos que coincidan con el documento ingresado.";
                    dgv.Rows.Clear(); 
                }
                else if (ex.Message.Contains("No se encontraron registros. Por favor ingrese nuevos registros"))
                {
                    mensajeError = "No se encontraron registros. Por favor ingrese nuevos registros";
                    dgv.Rows.Clear(); 
                }
                else
                {
                    mensajeError = "Error desconocido: " + ex.Message;
                }
                return false;
            }
           
        }
        #endregion

        #region Tipos de Busqueda 
        private void pboxBuscarTrabajador_Click(object sender, EventArgs e)
        {
            FuncionTiposBusqueda(1);
        }

        private void txtBusquedaContratoLaboralTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;
            int tipoCon = 1;

            if (!MetodosValidaciones.ValidarSoloNumeros2(textBox.Text, tipoCon, e.KeyChar, out this.mensajeError)) // Usar 'this' si es un campo de clase
            {
                e.Handled = true;
                Mbox.Show(this.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                FuncionTiposBusqueda(2);
            }
        }
        private void txtBusquedaContratoLaboralTrabajador_TextChanged(object sender, EventArgs e)
        {
            FuncionBuscarContratoLaboral(dgvContratoLaboral, 0, out mensajeError);
        }
        private void cboPaginaContratoLaboral_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuncionTiposBusqueda(3);
        }

        #endregion

        #region Eliminar Registro Contrato Laboral
        private void eliminarRegistroContratoLaboral_Click(object sender, EventArgs e)
        {

            if (dgvContratoLaboral.SelectedRows.Count > 0)
            {
                DialogResult resultado = Mbox.Show("¿Estás seguro de que deseas eliminar el CONTRATO?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Int32 IdContratoLaboral = Convert.ToInt32(dgvContratoLaboral.SelectedRows[0].Cells["IdContratoLaboral"].Value);
                    NegocioContratoLaboral objNegocioContratoLaboral = new NegocioContratoLaboral();
                    try
                    {
                        string mensajeValidar = objNegocioContratoLaboral.NegocioEliminarContratoLaboral(IdContratoLaboral);
                        if (mensajeValidar == "OK")
                        {
                            FuncionesGenerales.ShowAlert("Registro Eliminado", frmNotificacion.enmType.Info);
                            FuncionBuscarContratoLaboral(dgvContratoLaboral, 0, out mensajeError);
                        }
                        else if (mensajeValidar == "No se puede eliminar porque el contrato se encuentra activo.")
                        {
                            Mbox.Show("Error: " + mensajeValidar, "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    catch (Exception ex)
                    {
                        Mbox.Show($"Error al Eliminar el Registro de Contrato Laboral: {ex.Message}", "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
        #endregion

        #region Enviar valores de la tabla a los Controles
        private void dgvContratoLaboral_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvContratoLaboral.Rows[e.RowIndex];

                int idContratoLaboral = (int)selectedRow.Cells["IdContratoLaboral"].Value;

               
                try
                {
                    if (selectedRow.Cells["fechaInicioContratoLaboral"].Value != DBNull.Value &&
                        selectedRow.Cells["fechaFinContratoLaboral"].Value != DBNull.Value)
                    {

                        bool fechaInicioValida = DateTime.TryParseExact(
                            selectedRow.Cells["fechaInicioContratoLaboral"].Value.ToString(),
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime fechaInicioContrato
                        );

                        bool fechaFinValida = DateTime.TryParseExact(
                            selectedRow.Cells["fechaFinContratoLaboral"].Value.ToString(),
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime fechaFinContrato
                        );

                        if (fechaInicioValida && fechaFinValida)
                        {
                            dtFechaInicioContrato.Value = fechaInicioContrato;
                            dtFechaFinContrato.Value = fechaFinContrato;

                            TimeSpan diferencia = fechaFinContrato - fechaInicioContrato;
                            int totalDias = diferencia.Days;

                            if (totalDias == 365 || totalDias == 366)
                            {
                                rbContratoDoceMeses.Checked = true;
                            }
                            else if (totalDias == 181 || totalDias == 182)
                            {
                                rbContratoSeisMeses.Checked = true;
                            }
                            else
                            {
                                rbContratoTresMeses.Checked = true;
                            }
                        }
                        else
                        {
                            MostrarErrorFechaInvalida();
                        }
                    }
                    else
                    {
                        MostrarErrorFechaVacia();
                    }
                }
                catch (Exception ex)
                {
                    Mbox.Show($"Ocurrió un error al procesar las fechas: {ex.Message}", "Error de Formato de Fechas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    AsignarFechasPorDefecto();
                }

                void MostrarErrorFechaInvalida()
                {
                    Mbox.Show("Una o ambas fechas no tienen un formato válido. Se asignaron las fechas actuales.");
                    AsignarFechasPorDefecto();
                }

                void MostrarErrorFechaVacia()
                {
                    Mbox.Show("Una o ambas fechas están vacías.");
                    AsignarFechasPorDefecto();
                }

                void AsignarFechasPorDefecto()
                {
                    dtFechaInicioContrato.Value = DateTime.Now;
                    dtFechaFinContrato.Value = DateTime.Now;
                }

                int horasTotalesContrato = (int)selectedRow.Cells["horasTotalesContratoLaboral"].Value;
                string estadoContrato = selectedRow.Cells["EstadoContratoLaboral"].Value.ToString();
                string descripcionContrato = selectedRow.Cells["descripcionContratoLaboral"].Value.ToString();
                int idCargoContrato = (int)selectedRow.Cells["IdCargoContratoLaboral"].Value;
                int idTipoContrato = (int)selectedRow.Cells["IdTipoContratoLaboral"].Value;
                int idTrabajador = (int)selectedRow.Cells["IdTrabajador"].Value;
                decimal salarioContrato = (decimal)selectedRow.Cells["salarioContratoLaboral"].Value;
                int horasDiariasContrato = (int)selectedRow.Cells["horasDiariasContratoLaboral"].Value;

                decimal asignacionFamiliar = (decimal)selectedRow.Cells["AsignaciónFamiliarContratoLaboral"].Value;
                chkAsignaciónFamiliarContratoLaboral.Checked = asignacionFamiliar != 0.00m;
                txtMontoAsignacionFamiliar.Text = asignacionFamiliar.ToString("F2");

                txtIdContratoLaboral.Text = idContratoLaboral.ToString();
                txtHorasTotalesContratoLaboral.Text = horasTotalesContrato.ToString();
                chkEstadoContratoLaboral.Checked = estadoContrato.Equals("ACTIVO", StringComparison.OrdinalIgnoreCase);
                txtDescripcionContratoLaboral.Text = descripcionContrato;
                cboCargo.SelectedIndex = idCargoContrato;
                cboTipoContrato.SelectedIndex = idTipoContrato;
                cboTrabajador.SelectedIndex = idTrabajador;
                txtSalarioContratoLaboral.Text = salarioContrato.ToString("F2");
                txtHorasDiariasContratoLaboral.Text = horasDiariasContrato.ToString();

                btnRegistrarContratoLaboral.Visible = false;
                btnActualizarContratoLaboral.Visible = true;
                chkEstadoContratoLaboral.Enabled = true;

            }

        }
        #endregion

        #region Validacion de Habilitar Fechas de Busqueda
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

        #region Evento checked changed de los  radio Button Rango Fechas
        private void rbContratoTresMeses_CheckedChanged(object sender, EventArgs e)
        {
            FuncionCambiarFechasContrato();
        }

        private void rbContratoSeisMeses_CheckedChanged(object sender, EventArgs e)
        {
            FuncionCambiarFechasContrato();
        }
        private void rbContratoDoceMeses_CheckedChanged(object sender, EventArgs e)
        {
            FuncionCambiarFechasContrato();
        }
        #endregion

        #region Metodo Cambiar Fechas de Contrato
        private void FuncionCambiarFechasContrato()
        {
            Int32 intMesesDuracion = 0;

            IEnumerable<SiticoneRadioButton> grbRadioBtuttons = gbRangoFechasContrato.Controls.OfType<SiticoneRadioButton>();

            foreach (SiticoneRadioButton item in grbRadioBtuttons)
            {
                if (item.Checked)
                {
                    if (item.Name == "rbContratoTresMeses")
                    {
                        intMesesDuracion = 3;
                    }
                    if (item.Name == "rbContratoSeisMeses")
                    {
                        intMesesDuracion = 6;
                    }
                    if (item.Name == "rbContratoDoceMeses")
                    {
                        intMesesDuracion = 12;
                    }
                }
            }

        MetodosValidaciones.EstablecerFechasContrato(out DateTime fechaInicial, out DateTime fechaFinal, intMesesDuracion);

            dtFechaInicioContrato.Value = fechaInicial;
            dtFechaFinContrato.Value = fechaFinal;
        }
        #endregion

        #region Asignación Familiar ContratoLaboral
        private void chkAsignaciónFamiliarContratoLaboral_CheckedChanged(object sender, EventArgs e)
        {
            decimal salarioBase = Convert.ToDecimal(txtSalarioContratoLaboral.Text);
            CalcularMontoAsignacionFamiliar objCalcularMontoAsignacionFamiliar = new CalcularMontoAsignacionFamiliar();

            if (chkAsignaciónFamiliarContratoLaboral.Checked)
            {
                decimal asignacionFamiliar = objCalcularMontoAsignacionFamiliar.CalcularAsignacionFamiliar(salarioBase);
                txtMontoAsignacionFamiliar.Text = asignacionFamiliar.ToString("F2");
            }
            else
            {
                txtMontoAsignacionFamiliar.Text = "0.00";
            }
        }
        #endregion

        #region Validar txtHorasDiariasContratoLaboral vacio
        private void txtHorasDiariasContratoLaboral_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHorasDiariasContratoLaboral.Text) ||
    string.IsNullOrWhiteSpace(txtHorasTotalesContratoLaboral.Text))
            {
                btnActualizarContratoLaboral.Enabled = false;
            }
            else
            {
                btnActualizarContratoLaboral.Enabled = true;
            }
        }
        #endregion

    }
}
