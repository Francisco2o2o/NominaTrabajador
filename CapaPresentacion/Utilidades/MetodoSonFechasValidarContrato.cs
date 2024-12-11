using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Utilidades
{
    public class MetodoSonFechasValidarContrato
    {
        public static void EstablecerFechasContrato(out DateTime fechaInicial, out DateTime fechaFinal, int mesesDuracion)
        {
            fechaInicial = DateTime.Now;
            fechaFinal = fechaInicial.AddMonths(mesesDuracion);
        }
    }
}
