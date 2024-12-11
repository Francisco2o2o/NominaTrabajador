using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MetodoCalcularDescuentosTotales
    {
        public decimal CalcularDescuentosTotales(decimal DeduccionesSeguros, decimal AFP, decimal DeducciónImpuestos)
        {
            decimal totalDescuentosTotales = 0m;

            totalDescuentosTotales = DeduccionesSeguros + AFP + DeducciónImpuestos;

            return totalDescuentosTotales;
        }
    }
}
