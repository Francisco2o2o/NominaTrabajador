using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using Guna.UI2.WinForms;
using LayerPresentation.FormNotificaciones;
using LayerPresentation.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Xml.Linq;

namespace CapaPresentacion
{
    public partial class frmPagos : Form
    {
        //Variables de busqueda
        static Boolean pasoLoad;
   
        public frmPagos()
        {
            InitializeComponent();

            #region Creacion de columnas para la tabla [dgvContratoLaboral]
            dgvContratoLaboral.Columns.Add("NumeroContratoLaboral", "N°");
            dgvContratoLaboral.Columns.Add("IdContratoLaboral", "IdContratoLaboral");
            dgvContratoLaboral.Columns.Add("documentoTrabajador", "Documento");
            dgvContratoLaboral.Columns.Add("nombreTrabajador", "Nombre del Trabajador");
            dgvContratoLaboral.Columns.Add("fechaInicioContratoLaboral", "Fecha de Inicio");
            dgvContratoLaboral.Columns.Add("fechaFinContratoLaboral", "Fecha de Fin");
            dgvContratoLaboral.Columns.Add("nombreTipoContratoLaboral", "Tipo de Contrato");
            dgvContratoLaboral.Columns.Add("salarioBaseTipoContratoLaboral", "Salario Base");
            dgvContratoLaboral.Columns.Add("AsignaciónFamiliarContratoLaboral", "Asignación Familiar");
            #endregion

            #region Creacion de columnas para la tabla [dgvPagos]
            dgvPagos.Columns.Add("NumeroPago", "N°");
            dgvPagos.Columns.Add("IdPago", "ID Pago");
            dgvPagos.Columns.Add("IdContratoLaboral", "IdContratoLaboral");
            dgvPagos.Columns.Add("documentoTrabajador", "Documento");
            dgvPagos.Columns.Add("nombreTrabajador", "Nombre del Trabajador");
            dgvPagos.Columns.Add("fechaPago", "Fecha de Pago");
            dgvPagos.Columns.Add("SalarioContrato", "Salario");

            dgvPagos.Columns.Add("DescuentoSeguroSalud", "Deducción Salud");
            dgvPagos.Columns.Add("DescuentoSeguroVida", "Deducción Vida");
            dgvPagos.Columns.Add("DescuentoSeguroAccidentes", "Deducción Accidentes");
            dgvPagos.Columns.Add("DescuentoAFP", "AFP");
            dgvPagos.Columns.Add("DescuentoDeduccionImpuestos", "Deducción Impuestos");
            dgvPagos.Columns.Add("DescuentosTotales", "Descuentos Totales");

            dgvPagos.Columns.Add("Bonificacion", "Bonificacion");

            dgvPagos.Columns.Add("SalarioNeto", "Salario Neto");


            #endregion

            #region Creacion de columnas para la tabla [dgvPeriodoPagos]
            dgvPeriodoPagos.Columns.Add("Numero", "N°");
            dgvPeriodoPagos.Columns.Add("nombrePeriodo", "Periodo");
            dgvPeriodoPagos.Columns.Add("montoTotalPago", "Monto Total del Pago");
            dgvPeriodoPagos.Columns.Add("DescuentosTotales", "Descuentos Totales");
            dgvPeriodoPagos.Columns.Add("DescuentoDeduccionImpuestos", "Deducción de Impuestos");
            dgvPeriodoPagos.Columns.Add("DescuentoSeguroAccidentes", "Seguro de Accidentes");
            dgvPeriodoPagos.Columns.Add("DescuentoSeguroSalud", "Seguro de Salud");
            dgvPeriodoPagos.Columns.Add("DescuentoSeguroVida", "Seguro de Vida");
            dgvPeriodoPagos.Columns.Add("DescuentoAFP", "AFP");
            dgvPeriodoPagos.Columns.Add("fechaPago", "Fecha de Pago");
            #endregion


            FuncionLlenarComboBoxPeriodo(cboPeriodo, 0, "", false);
        }

        private void frmPagos_Load(object sender, EventArgs e)
        {
            bool EstadoActivarBotonRegistrar = FuncionesValidaciones.FuncionPropiedadesControlesPagos(btnRegistrarPagos, btnLimpiarControlesPagos, FuncionValidarTextBox());
            btnRegistrarPagos.Enabled = EstadoActivarBotonRegistrar;

            cboMetodoPago.SelectedIndex = 0;
            //Variable de busqueda
            pasoLoad = true;

            FuncionBuscarContratoLaboral(dgvContratoLaboral, 0);

            #region Visibilidad de Columnas de las tablas [dgvContratoLaboral - dgvPagos]
            dgvContratoLaboral.Columns["IdContratoLaboral"].Visible = false;
            dgvPagos.Columns["IdPago"].Visible = false;
            dgvPagos.Columns["DescuentoSeguroSalud"].Visible = false;
            dgvPagos.Columns["DescuentoSeguroVida"].Visible = false;
            dgvPagos.Columns["DescuentoSeguroAccidentes"].Visible = false;
            dgvPagos.Columns["DescuentoAFP"].Visible = false;
            dgvPagos.Columns["DescuentoDeduccionImpuestos"].Visible = false;
            dgvPagos.Columns["IdContratoLaboral"].Visible = false;
            dgvPagos.Columns["fechaPago"].Visible = false;
            dgvPagos.Columns["SalarioContrato"].Visible = false;
            #endregion

            #region Tamaño de Columnas de la Tabla [dgvContratoLaboral]
            dgvContratoLaboral.Columns["NumeroContratoLaboral"].Width = 21;
            dgvContratoLaboral.Columns["documentoTrabajador"].Width = 50;
            dgvContratoLaboral.Columns["salarioBaseTipoContratoLaboral"].Width = 50;
            dgvContratoLaboral.Columns["AsignaciónFamiliarContratoLaboral"].Width = 50;
            dgvContratoLaboral.Columns["nombreTipoContratoLaboral"].Width = 90;
            dgvContratoLaboral.Columns["nombreTrabajador"].Width = 110;
            #endregion
        }

        #region  Validar Controles Vacios
        public bool FuncionValidarTextBox()
        {
            if (txtDescuentosTotales.Text != "0.00" && cboPeriodo.SelectedIndex != 0)
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
            btnLimpiarControlesPagos.Visible = esValido;
            btnRegistrarPagos.Enabled = esValido;
        }
        #endregion

        #region Evento Changed  cboMetodoPago
        private void cboMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdMetodoPago = cboMetodoPago.SelectedIndex;
            txtIdMetodoPago.Text = IdMetodoPago.ToString();
            //FuncionValidarChanged();
        }
        #endregion

        #region Funcion para llenar el ComboBox Periodo
        public static List<Periodo> FuncionLlenarComboBoxPeriodo(Guna2ComboBox cbo, Int32 idPeriodo, String nombrePeriodo, Boolean buscar)
        {
            NegocioPeriodo objNegocioPeriodo = new NegocioPeriodo();
            List<Periodo> lstPeriodo = new List<Periodo>();

            try
            {
                lstPeriodo = objNegocioPeriodo.NegocioLlenarComboBoxPeriodo(idPeriodo, nombrePeriodo, buscar);
                cbo.ValueMember = "IdPeriodo";
                cbo.DisplayMember = "NombrePeriodo";
                cbo.DataSource = lstPeriodo;

                return lstPeriodo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return lstPeriodo;
            }
            finally
            {
                lstPeriodo = null;
            }
        }
        #endregion

        #region Asignar Datos al seleccionar el Periodo
        private void cboPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPeriodo.SelectedItem is Periodo selectedPeriodo)
            {
                txtIdPeriodo.Text = selectedPeriodo.IdPeriodo.ToString();
                dtFechaInicioPeriodo.Value = selectedPeriodo.FechaInicioPeriodo;
                dtFechaFinPeriodo.Value = selectedPeriodo.FechaFinPeriodo;
            }
            FuncionValidarChanged();
        }
        #endregion

        #region Función Listar Contrato Laboral
        private bool SegundaBusqueda = false;
        public Boolean FuncionBuscarContratoLaboral(DataGridView dgv, Int32 numPagina)
        {
            NegocioContratoLaboral objNegocioContratoLaboral = new NegocioContratoLaboral();
            DataTable dtContratoLaboral = new DataTable();
            String documentoTrabajador;
            Int32 filas = 11;
            DateTime fechaInicial = dtFechaInicio.Value;
            DateTime fechaFinal = dtFechaFin.Value;
            Boolean habilitarFechas = chkHabilitarFechasBusqueda.Checked ? true : false;

            try
            {
                documentoTrabajador = txtBusquedaContratoLaboralTrabajador.Text;

                dtContratoLaboral = objNegocioContratoLaboral.NegocioListarContratoLaboral(habilitarFechas, fechaInicial, fechaFinal, documentoTrabajador, numPagina);

                dgv.Visible = true;
                chkSeleccionarTodosContrato.Visible = true;
                chkSeleccionarTodosContrato.Enabled = true;
                dgv.Rows.Clear();
                Int32 totalResultados = dtContratoLaboral.Rows.Count;



                if (dtContratoLaboral.Rows.Count > 0)
                {
                    Int32 y;
                    if (numPagina == 0)
                    {
                        y = 0;
                    }
                    else
                    {
                        int tabInicio = (numPagina - 1) * filas;
                        y = tabInicio;
                    }

                    foreach (DataRow item in dtContratoLaboral.Rows)
                    {
                        if (!dgv.Columns.Contains("Seleccionar"))
                        {
                            DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
                            chkColumn.Name = "Seleccionar";
                            chkColumn.HeaderText = "Seleccionar";
                            chkColumn.Width = 60;
                            chkColumn.TrueValue = true;
                            chkColumn.FalseValue = false;
                            dgv.Columns.Add(chkColumn);
                        }

                        y++;
                        string nombreCompleto = $"{item["nombreTrabajador"]} {item["apellidoPaternoTrabajador"]} {item["apellidoMaternoTrabajador"]}";
                        string fechaInicio = Convert.ToDateTime(item["fechaInicioContratoLaboral"]).ToString("dd/MM/yyyy");
                        string fechaFin = Convert.ToDateTime(item["fechaFinContratoLaboral"]).ToString("dd/MM/yyyy");

                        dgv.Rows.Add(
                            y,
                            item["IdContratoLaboral"],
                            item["documentoTrabajador"],
                            nombreCompleto,
                            fechaInicio,
                            fechaFin,
                            item["nombreTipoContratoLaboral"],
                            item["salarioBaseTipoContratoLaboral"],
                            item["AsignaciónFamiliarContratoLaboral"],
                            true
                        );
                    }
                }

                if (numPagina == 0 && !SegundaBusqueda)
                {
                    Int32 totalRegistros = Convert.ToInt32(dtContratoLaboral.Rows[0][0]);
                    FuncionesGenerales.CalcularPaginacion(totalRegistros, filas, totalResultados, cboPaginaContratoLaboral, btnTotalPaginasPagos, btnNumFilasPagos, btnTotalRegistrosPagos);
                    SegundaBusqueda = true;
                    btnProcesarContratosLaborales.Enabled = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar contrato laboral: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            finally
            {
                objNegocioContratoLaboral = null;
            }
        }
        #endregion

        #region Seleccionar Todos los Registros de la tabla [dgvContratoLaboral]dgvContratoLaboral
        private void chkSeleccionarTodosContrato_CheckedChanged(object sender, EventArgs e)
        {
            bool estado = chkSeleccionarTodosContrato.Checked;

            foreach (DataGridViewRow row in dgvContratoLaboral.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Seleccionar"];
                    chk.Value = estado;
                }
            }
            btnProcesarContratosLaborales.Enabled = estado;
        }

        #region Evento CellContentClick de la tabla [dgvContratoLaboral] para manejar el click en los checkboxes
        private void dgvContratoLaboral_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvContratoLaboral.Columns["Seleccionar"].Index && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvContratoLaboral.Rows[e.RowIndex].Cells["Seleccionar"];

                chk.Value = !(Convert.ToBoolean(chk.Value));

                btnProcesarContratosLaborales.Enabled = dgvContratoLaboral.Rows.Cast<DataGridViewRow>()
                    .Any(row => !row.IsNewRow && Convert.ToBoolean(row.Cells["Seleccionar"].Value));
                if (Convert.ToBoolean(chk.Value))
                {

                    dgvContratoLaboral.CommitEdit(DataGridViewDataErrorContexts.Commit);

                }
            }
        }
        #endregion

        #endregion

        #region Actualizar Deducciones
        List<Deducciones> listaDeducciones = new List<Deducciones>();
        private void ActualizarDeducciones()
        {
            MetodosPagos metodosPagos = new MetodosPagos();

            List<decimal> salariosSeleccionados = ObtenerSalariosSeleccionados();

            listaDeducciones.Clear();


            if (salariosSeleccionados.Count == 0)
            {
                LimpiarCamposDeducciones();
                return;
            }

            foreach (DataGridViewRow row in dgvContratoLaboral.Rows)
            {
                if (!row.IsNewRow && Convert.ToBoolean(row.Cells["Seleccionar"].Value))
                {
                    if (decimal.TryParse(row.Cells["salarioBaseTipoContratoLaboral"].Value?.ToString(), out decimal salarioBase) &&
                        decimal.TryParse(row.Cells["AsignaciónFamiliarContratoLaboral"].Value?.ToString(), out decimal asignacionFamiliar))
                    {
                        decimal deduccionSalud = metodosPagos.CalcularDeduccionSeguros(new List<decimal> { salarioBase + asignacionFamiliar }, 1);
                        decimal deduccionVida = metodosPagos.CalcularDeduccionSeguros(new List<decimal> { salarioBase + asignacionFamiliar }, 2);
                        decimal deduccionAccidentes = metodosPagos.CalcularDeduccionSeguros(new List<decimal> { salarioBase + asignacionFamiliar }, 3);

                        decimal afp = metodosPagos.CalcularAFP(new List<decimal> { salarioBase + asignacionFamiliar });
                        decimal deduccionImpuestos = metodosPagos.CalcularDeduccionImpuestos(new List<decimal> { salarioBase + asignacionFamiliar });

                        Deducciones deducciones = new Deducciones(deduccionSalud, deduccionVida, deduccionAccidentes, afp, deduccionImpuestos);
                        listaDeducciones.Add(deducciones);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingresa un salario base y asignación familiar válidos en todas las filas seleccionadas.");
                        return;
                    }
                }
            }

            decimal deduccionSaludTotal = listaDeducciones.Sum(d => d.DeduccionSalud);
            decimal deduccionVidaTotal = listaDeducciones.Sum(d => d.DeduccionVida);
            decimal deduccionAccidentesTotal = listaDeducciones.Sum(d => d.DeduccionAccidentes);
            decimal totalDeducciones = deduccionSaludTotal + deduccionVidaTotal + deduccionAccidentesTotal;

            txtSeguroSalud.Text = deduccionSaludTotal.ToString("F2");
            txtSeguroVida.Text = deduccionVidaTotal.ToString("F2");
            txtSeguroAccidentes.Text = deduccionAccidentesTotal.ToString("F2");
            txtDeduccionesSeguros.Text = totalDeducciones.ToString("F2");

            decimal afpTotal = listaDeducciones.Sum(d => d.Afp);
            txtAFP.Text = afpTotal.ToString("F2");

            decimal deduccionImpuestosTotal = listaDeducciones.Sum(d => d.DeduccionImpuestos);
            txtDeducciónImpuestos.Text = deduccionImpuestosTotal.ToString("F2");

            decimal descuentosTotales = metodosPagos.CalcularDescuentosTotales(totalDeducciones, afpTotal, deduccionImpuestosTotal);
            txtDescuentosTotales.Text = descuentosTotales.ToString("F2");
        }
        #endregion

        #region Funcion Culcular Total Pago
        public void FuncionCalcularMontoTotalPago()
        {
            MetodosPagos metodosPagos = new MetodosPagos();
            if (dgvPagos.Rows.Count > 0)
            {
                List<decimal> salariosSeleccionados = new List<decimal>();


                foreach (DataGridViewRow row in dgvPagos.Rows)
                {

                    if (decimal.TryParse(row.Cells["SalarioNeto"].Value?.ToString(), out decimal salarioNeto))
                    {
                        salariosSeleccionados.Add(salarioNeto);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, asegúrese de que todos los salarios netos sean válidos en las filas seleccionadas.");
                        return;
                    }
                }

                decimal totalSeguro = metodosPagos.CalcularMontoTotalPago(salariosSeleccionados);

                txtMontoTotalPago.Text = totalSeguro.ToString("F2");
            }
            else
            {
                txtMontoTotalPago.Text = "0.00";

                MessageBox.Show("No hay registros en el DataGridView para procesar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Obtener Salarios Seleccionados
        private List<decimal> ObtenerSalariosSeleccionados()
        {
            List<decimal> salariosSeleccionados = new List<decimal>();

            foreach (DataGridViewRow row in dgvContratoLaboral.Rows)
            {
                if (!row.IsNewRow && Convert.ToBoolean(row.Cells["Seleccionar"].Value))
                {
                    if (decimal.TryParse(row.Cells["salarioBaseTipoContratoLaboral"].Value?.ToString(), out decimal salarioBase) &&
                        decimal.TryParse(row.Cells["AsignaciónFamiliarContratoLaboral"].Value?.ToString(), out decimal asignacionFamiliar))
                    {
                        salariosSeleccionados.Add(salarioBase + asignacionFamiliar);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingresa un salario base y asignación familiar válidos en todas las filas seleccionadas.");
                        return new List<decimal>();
                    }
                }
            }

            return salariosSeleccionados;
        }
        #endregion

        #region Limpiar Campos de Deducciones
        private void LimpiarCamposDeducciones()
        {
            txtSeguroSalud.Text = "0.00";
            txtSeguroVida.Text = "0.00";
            txtSeguroAccidentes.Text = "0.00";
            txtDeduccionesSeguros.Text = "0.00";
            txtAFP.Text = "0.00";
            txtDeducciónImpuestos.Text = "0.00";
            txtDescuentosTotales.Text = "0.00";
        }
        #endregion

        #region Eventos de Actualización
        private void dgvContratoLaboral_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ActualizarDeducciones();
        }
        #endregion

        #region Funcion Tipos de Busqueda
        public void FuncionTiposBusqueda(Int32 tipoCon)
        {
            dgvPeriodoPagos.Visible = false;
            Boolean bResult;
            if (tipoCon == 1)
            {
                if (pasoLoad)
                {
                    bResult = FuncionBuscarContratoLaboral(dgvContratoLaboral, 0);

                    if (!bResult)
                    {
                        Mbox.Show("Error al realizar busqueda. Comunicar al Administrador del Sistema", "Error de Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else if (tipoCon == 2)
            {
                if (pasoLoad)
                {
                    bResult = FuncionBuscarContratoLaboral(dgvContratoLaboral, 0);

                    if (!bResult)
                    {
                        Mbox.Show("Error al realizar busqueda. Comunicar al Administrador del Sistema", "Error de Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                Int32 numPagina = Convert.ToInt32(cboPaginaContratoLaboral.Text.ToString());
                if (pasoLoad)
                {
                    bResult = FuncionBuscarContratoLaboral(dgvContratoLaboral, numPagina);

                    if (!bResult)
                    {
                        Mbox.Show("Error al realizar busqueda. Comunicar al Administrador del Sistema", "Error de Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

        }

        #endregion

        #region Tipos de Busqueda 
        private void pboxBuscarContratoLaboraPorTrabajador_Click(object sender, EventArgs e)
        {
            FuncionTiposBusqueda(1);
        }

        private void txtBusquedaContratoLaboralTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Key.Enter)
            {
                FuncionTiposBusqueda(2);
            }
        }

        private void cboPaginaContratoLaboral_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuncionTiposBusqueda(3);
        }
        #endregion

        #region Button Procesar Contratos Laborales
        private void btnProcesarContratosLaborales_Click(object sender, EventArgs e)
        {
            if (dgvPagos.Rows.Count > 0)
            {
                dgvPagos.Rows.Clear();
            }

            int numeroPago = 1;
            ActualizarDeducciones();

            foreach (DataGridViewRow row in dgvContratoLaboral.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Seleccionar"].Value))
                {
                    decimal salarioContrato = Convert.ToDecimal(row.Cells["salarioBaseTipoContratoLaboral"].Value);

                    if (salarioContrato <=0)
                    {
                        MessageBox.Show("El salario no debe ser 0; debe ser igual a lo estipulado en el contrato.", "Error de Proceso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        dgvPagos.Visible = false;
                        return;
                    }

                    dgvPagos.Visible = true;
                    dgvPagos.Columns["NumeroPago"].Width = 25;
                    dgvPagos.Columns["documentoTrabajador"].Width = 80;
                    dgvPagos.Columns["nombreTrabajador"].Width = 180;
                    dgvPagos.Columns["fechaPago"].Width = 120;

                    int index = dgvPagos.Rows.Add();

                    dgvPagos.Rows[index].Cells["NumeroPago"].Value = numeroPago;
                    dgvPagos.Rows[index].Cells["IdPago"].Value = Guid.NewGuid();
                    dgvPagos.Rows[index].Cells["IdContratoLaboral"].Value = row.Cells["IdContratoLaboral"].Value;
                    dgvPagos.Rows[index].Cells["documentoTrabajador"].Value = row.Cells["documentoTrabajador"].Value;
                    dgvPagos.Rows[index].Cells["nombreTrabajador"].Value = row.Cells["nombreTrabajador"].Value;
                    dgvPagos.Rows[index].Cells["SalarioContrato"].Value = salarioContrato;
                    dgvPagos.Rows[index].Cells["Bonificacion"].Value = row.Cells["AsignaciónFamiliarContratoLaboral"].Value;

                    decimal bonificacion = Convert.ToDecimal(row.Cells["AsignaciónFamiliarContratoLaboral"].Value);

                    if (numeroPago - 1 < listaDeducciones.Count)
                    {
                        var deducciones = listaDeducciones[numeroPago - 1];
                        dgvPagos.Rows[index].Cells["DescuentoSeguroSalud"].Value = deducciones.DeduccionSalud;
                        dgvPagos.Rows[index].Cells["DescuentoSeguroVida"].Value = deducciones.DeduccionVida;
                        dgvPagos.Rows[index].Cells["DescuentoSeguroAccidentes"].Value = deducciones.DeduccionAccidentes;
                        dgvPagos.Rows[index].Cells["DescuentoAFP"].Value = deducciones.Afp;
                        dgvPagos.Rows[index].Cells["DescuentoDeduccionImpuestos"].Value = deducciones.DeduccionImpuestos;

                        decimal descuentosTotales = deducciones.TotalDeducciones;
                        dgvPagos.Rows[index].Cells["DescuentosTotales"].Value = descuentosTotales;

                        decimal salarioNeto = (salarioContrato + bonificacion) - descuentosTotales;

                        dgvPagos.Rows[index].Cells["SalarioNeto"].Value = salarioNeto.ToString("F2");

                        if (salarioNeto != 0)
                        {
                            dgvPagos.Rows[index].Cells["fechaPago"].Value = DateTime.Now;

                            numeroPago++;
                            lblMonedaMontoTotal.Visible = true;
                            txtMontoTotalPago.Visible = true;
                            dgvContratoLaboral.Enabled = false;
                            chkSeleccionarTodosContrato.Enabled = false;
                            btnLimpiarControlesPagos.Visible = true;
                            FuncionCalcularMontoTotalPago();
                            FuncionesGenerales.ShowAlert("Proceso Exitoso", frmNotificacion.enmType.Info);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay suficientes deducciones para procesar el pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

            if (numeroPago == 1)
            {
                MessageBox.Show("Por favor seleccione un registro para proceder con el pago.", "Error de Proceso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        #endregion

        #region Button Limpiar Controles
        private void btnLimpiarControlesPagos_Click(object sender, EventArgs e)
        {
            FuncionLimpiarControles();
        }
        #endregion

        #region Funcion Limpiar Controles
        public void FuncionLimpiarControles()
        {
            dgvContratoLaboral.Enabled = true;
            chkSeleccionarTodosContrato.Enabled = true;
            btnLimpiarControlesPagos.Visible = false;
            if (dgvPagos.Rows.Count > 0)
            {
                dgvPagos.Rows.Clear();
                dgvPagos.Visible = false;
            }

            chkDeduccionesSeguros.Checked = false;
            chkSeguroSalud.Checked = false;
            chkSeguroVida.Checked = false;
            chkSeguroAccidentes.Checked = false;
            chkDeducciónImpuestos.Checked = false;
            chkAFP.Checked = false;

            cboPeriodo.SelectedIndex = 0;
            txtIdPeriodo.Text = "";
            txtMontoTotalPago.Text = "";
            cboMetodoPago.SelectedIndex = 0;
            btnProcesarContratosLaborales.Enabled = true;
            lblMontoTotal.Visible = false;
            lblMonedaMontoTotal.Visible = false;
            txtMontoTotalPago.Visible = false;

        }
        #endregion

        #region Habilitar fechas de busqueda
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

        #region Función Registrar Pago
        public String funcionRegistrarPago()
        {
            NegocioPago objNegocioPago = new NegocioPago();
            Pago objPago = new Pago();
            String mensajeValidar = "";

            try
            {
                objPago.MontoTotalPago = Convert.ToDecimal(txtMontoTotalPago.Text);
                objPago.FechaPago = DateTime.Now;
                objPago.EstadoPago = true;
                objPago.MetodoPago = cboMetodoPago.Text;
                objPago.IdPeriodo = Convert.ToInt32(txtIdPeriodo.Text);

                StringBuilder xmlDetalles = new StringBuilder();
                xmlDetalles.Append("<Detalles>");

                foreach (DataGridViewRow row in dgvPagos.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        xmlDetalles.Append("<DetallePago>");

                        decimal montoTotal = Convert.ToDecimal(row.Cells["SalarioNeto"].Value);
                        xmlDetalles.AppendFormat("<MontoTotalDetallePago>{0}</MontoTotalDetallePago>", montoTotal);

                        int idContratoLaboral = Convert.ToInt32(row.Cells["IdContratoLaboral"].Value);
                        xmlDetalles.AppendFormat("<IdContratoLaboral>{0}</IdContratoLaboral>", idContratoLaboral);

                        string metodoPago = !string.IsNullOrWhiteSpace(cboMetodoPago.Text) ? cboMetodoPago.Text : "No especificado";
                        xmlDetalles.AppendFormat("<MetodoPago>{0}</MetodoPago>", metodoPago);

                        decimal descuentosTotales = Convert.ToDecimal(row.Cells["DescuentosTotales"].Value);
                        xmlDetalles.AppendFormat("<DescuentosTotales>{0}</DescuentosTotales>", descuentosTotales);

                        decimal descuentoSeguroSalud = Convert.ToDecimal(row.Cells["DescuentoSeguroSalud"].Value);
                        xmlDetalles.AppendFormat("<DescuentoSeguroSalud>{0}</DescuentoSeguroSalud>", descuentoSeguroSalud);

                        decimal descuentoSeguroVida = Convert.ToDecimal(row.Cells["DescuentoSeguroVida"].Value);
                        xmlDetalles.AppendFormat("<DescuentoSeguroVida>{0}</DescuentoSeguroVida>", descuentoSeguroVida);

                        decimal descuentoSeguroAccidentes = Convert.ToDecimal(row.Cells["DescuentoSeguroAccidentes"].Value);
                        xmlDetalles.AppendFormat("<DescuentoSeguroAccidentes>{0}</DescuentoSeguroAccidentes>", descuentoSeguroAccidentes);

                        decimal descuentoAFP = Convert.ToDecimal(row.Cells["DescuentoAFP"].Value);
                        xmlDetalles.AppendFormat("<DescuentoAFP>{0}</DescuentoAFP>", descuentoAFP);

                        decimal descuentoImpuestos = Convert.ToDecimal(row.Cells["DescuentoDeduccionImpuestos"].Value);
                        xmlDetalles.AppendFormat("<DescuentoDeduccionImpuestos>{0}</DescuentoDeduccionImpuestos>", descuentoImpuestos);

                        xmlDetalles.Append("</DetallePago>");
                    }
                }
                xmlDetalles.Append("</Detalles>");

                objPago.XmlDetalles = xmlDetalles.ToString();

                mensajeValidar = objNegocioPago.NegocioRegistrarPago(objPago).Trim();

                if (mensajeValidar == "OK")
                {
                    mensajeValidar = "Pago Registrado";
                }
                else if (mensajeValidar == "El XML proporcionado está vacío o no tiene formato válido.")
                {
                    mensajeValidar = "El XML proporcionado está vacío o no tiene formato válido.";
                }
                else if (mensajeValidar == "Ya existe un pago registrado para el período especificado.")
                {
                    mensajeValidar = "Ya existe un pago registrado para el período especificado.";
                }
                else
                {
                    mensajeValidar = "Error al registrar el Pago.";
                }
            }
            catch (Exception ex)
            {
                mensajeValidar = $"Error: {ex.Message}";
            }

            return mensajeValidar;
        }
        #endregion

        #region Button Registrar Pagos
        private void btnRegistrarPagos_Click(object sender, EventArgs e)
        {
            if (cboMetodoPago.SelectedIndex != 0)
            {
                string pResult = funcionRegistrarPago();

                if (pResult == "Pago Registrado")
                {
                    FuncionesGenerales.ShowAlert(pResult, frmNotificacion.enmType.Info);
                    FuncionLimpiarControles();
                    FuncionBuscarContratoLaboral(dgvContratoLaboral, 0);
                }
                else if (pResult == "El XML proporcionado está vacío o no tiene formato válido.")
                {
                    Mbox.Show("Error: " + pResult, "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (pResult == "Ya existe un pago registrado para el período especificado.")
                {
                    Mbox.Show("Error: " + pResult, "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Mbox.Show("Error al registrar el Pago. Comunicar al Administrador del Sistema", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                Mbox.Show("Por Favor seleccione un Método de pago", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        #endregion

        #region Buton Busqueda Periodo
        private void btnBusquedaPeriodo_Click(object sender, EventArgs e)
        {
            FuncionBuscarPeriodos(dgvPeriodoPagos);
        }
        #endregion

        #region Función Listar Períodos
        public Boolean FuncionBuscarPeriodos(DataGridView dgv)
        {
            dgvContratoLaboral.Visible = false; 
            NegocioPeriodo objNegocioPeriodo = new NegocioPeriodo();
            DataTable dtPeriodo = new DataTable();

            try
            {
                dtPeriodo = objNegocioPeriodo.NegocioBuscarPeriodoPago();

                dgv.Visible = true;
                dgv.Rows.Clear();
                Int32 totalResultados = dtPeriodo.Rows.Count;

                if (totalResultados > 0)
                {
                    int y = 0;

                    foreach (DataRow item in dtPeriodo.Rows)
                    {
                        y++;

                        dgv.Rows.Add(
                            y,
                            item["nombrePeriodo"],
                            item["montoTotalPago"],
                            item["DescuentosTotales"],
                            item["DescuentoDeduccionImpuestos"], 
                            item["DescuentoSeguroAccidentes"], 
                            item["DescuentoSeguroSalud"],
                            item["DescuentoSeguroVida"], 
                            item["DescuentoAFP"], 
                            Convert.ToDateTime(item["fechaPago"]).ToString("dd/MM/yyyy")
                        );
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar períodos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            finally
            {
                objNegocioPeriodo = null;
            }
        }
        #endregion

    }

}
