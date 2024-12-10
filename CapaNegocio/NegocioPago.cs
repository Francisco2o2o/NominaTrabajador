using CapaEntidad;
using CapaPersistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NegocioPago
    {
        #region Registrar Pago
        public String NegocioRegistrarPago(Pago objPago)
        {
            PersistenciaPago objPersistenciaPago = new PersistenciaPago();
            return objPersistenciaPago.PersistenciaRegistrarPago(objPago);
        }
        #endregion
    }
}
