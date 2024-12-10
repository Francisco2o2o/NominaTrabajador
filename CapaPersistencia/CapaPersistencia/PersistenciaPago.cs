using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaConexion;
using System.Xml;

namespace CapaPersistencia
{
    public class PersistenciaPago
    {
        #region Registrar Pago
        public string PersistenciaRegistrarPago(Pago objPago)
        {
            SqlParameter[] pa = new SqlParameter[5];
            ConexionSql objCnx = null;
            try
            {
                pa[0] = new SqlParameter("@FechaPago", SqlDbType.DateTime) { Value = objPago.FechaPago };
                pa[1] = new SqlParameter("@MontoTotalPago", SqlDbType.Decimal) { Value = objPago.MontoTotalPago };
                pa[2] = new SqlParameter("@EstadoPago", SqlDbType.Bit) { Value = objPago.EstadoPago };
                pa[3] = new SqlParameter("@IdPeriodo", SqlDbType.Int) { Value = objPago.IdPeriodo };

                pa[4] = new SqlParameter("@DetallePagoXml", SqlDbType.Xml) { Value = objPago.XmlDetalles };

                objCnx = new ConexionSql("");
                objCnx.EjecutarProcedimiento("sp_RegistrarPago", pa);

                return "OK";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    return ex.Message;
                }
                throw;
            }
            finally
            {
                if (objCnx != null)
                {
                    objCnx.CierraConexion(); 
                }
                objCnx = null; 
            }
        }

    }
    #endregion

}

