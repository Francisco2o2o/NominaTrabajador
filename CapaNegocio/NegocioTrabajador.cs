using CapaEntidad;
using CapaPersistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NegocioTrabajador
    {

        #region Registrar Trabajador
        public string NegocioRegistraTrabajador(Trabajador objTrabajador)
        {
            PersistenciaTrabajador objPersistenciaTrabajador = new PersistenciaTrabajador();
            return objPersistenciaTrabajador.PersistenciaRegistrarTrabajador(objTrabajador);
        }
        #endregion

        #region Actualizar Trabajador
        public String NegocioActualizarTrabajador(Trabajador objTrabajador)
        {

            PersistenciaTrabajador objPersistenciaTrabajador = new PersistenciaTrabajador();
            return objPersistenciaTrabajador.PersistenciaActualizarTabajador(objTrabajador);


        }
        #endregion

        #region Buscar Trabajador
        public DataTable NegocioBuscarTrabajador(Boolean habilitarFechas, DateTime fechaInicial, DateTime fechaFinal, String documentoTrabajador, Int32 numPagina)
        {
            PersistenciaTrabajador objPersistenciaTrabajador = new PersistenciaTrabajador();
            return objPersistenciaTrabajador.PersistenciaBuscarTrabajador(habilitarFechas, fechaInicial, fechaFinal, documentoTrabajador, numPagina);
        }
        #endregion

        #region Eliminar Trabajador
        public string NegocioEliminarTrabajador(Int32 IdTrabajador, Int32 DocumentoTrabajador)
        {
            PersistenciaTrabajador objPersistenciaTrabajador = new PersistenciaTrabajador();
            return objPersistenciaTrabajador.PersistenciaEliminarTrabajador(IdTrabajador, DocumentoTrabajador);

        }
        #endregion

        #region Llenar ComboBox Trabajador
        public List<Trabajador> NegocioLlenarComboBoxTrabajador(Int32 idTrabajador, String nombreTrabajador, Boolean buscar)
        {
            PersistenciaTrabajador objPersistenciaTrabajador = new PersistenciaTrabajador();
            try
            {
                return objPersistenciaTrabajador.PersistenciaLlenarComboBoxTrabajador(idTrabajador, nombreTrabajador, buscar);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region  Verificar Contrato Activo
        public DataTable NegocioVerificarContratoLaboralActivo(Int32 IdTrabajador)
        {
            PersistenciaTrabajador objPersistenciaTrabajador = new PersistenciaTrabajador();
            try
            {
                return objPersistenciaTrabajador.PersistenciaVerificarContratoLaboralActivo(IdTrabajador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
