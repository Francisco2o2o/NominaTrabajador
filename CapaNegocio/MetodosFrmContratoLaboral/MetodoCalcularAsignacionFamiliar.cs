using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CapaNegocio
{
    public class MetodoCalcularAsignacionFamiliar
    {
        public decimal CalcularAsignacionFamiliar(decimal SalarioBase)
        {
            if (SalarioBase < 0)
            {
                MessageBox.Show("El salario base debe ser mayor que cero.", "Error de Entrada");
            }

            decimal MontoAsignacionFamiliar = SalarioBase * 0.1m;
            return Math.Round(MontoAsignacionFamiliar, 2);
        }
    }
}
