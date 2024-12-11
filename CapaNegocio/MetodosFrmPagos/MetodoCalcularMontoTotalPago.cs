using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MetodoCalcularMontoTotalPago
    {
        public decimal CalcularMontoTotalPago(List<decimal> salarios)
        {
            decimal TotalDeduccionImpuestos = 0m;

            foreach (var salarioBase in salarios)
            {
                TotalDeduccionImpuestos += salarioBase;
            }
            return TotalDeduccionImpuestos;
        }
    }
}
