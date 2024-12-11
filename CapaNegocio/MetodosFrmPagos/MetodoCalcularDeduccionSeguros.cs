using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaNegocio
{
    public class MetodoCalcularDeduccionSeguros
    {
        public decimal CalcularDeduccionSeguros(List<decimal> salarios, int tipoCondicion)
        {
            decimal totalSeguroSalud = 0m;

            foreach (var salarioBase in salarios)
            {
                if (tipoCondicion == 1)
                {
                    totalSeguroSalud += salarioBase * 0.09m;
                }
                else if (tipoCondicion == 2)
                {
                    totalSeguroSalud += salarioBase * 0.01m;
                }
                else
                {
                    totalSeguroSalud += salarioBase * 0.015m;
                }
            }
            return totalSeguroSalud;
        }
      
    }
}
