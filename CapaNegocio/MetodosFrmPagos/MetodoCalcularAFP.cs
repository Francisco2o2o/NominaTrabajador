using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MetodoCalcularAFP
    {
        public decimal CalcularAFP(List<decimal> salarios)
        {
            decimal TotalAFP = 0m;

            foreach (var salarioBase in salarios)
            {

                TotalAFP += salarioBase * 0.1m;
            }
            return TotalAFP;
        }
    }
}
