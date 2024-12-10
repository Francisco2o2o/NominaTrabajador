using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CapaNegocio
{
    public class MetodosContratoLaboral
    {
        public class CalcularSalarioContratoLaboral
        {
            public bool CalcularSalarioPorHora(decimal salarioTotal, int horasTotales, out decimal salarioPorHora)
            {
                salarioPorHora = 0.00m;

                if (salarioTotal <= 0 || horasTotales <= 0)
                {
                    MessageBox.Show("Por favor, ingrese un salario y horas válidas.", "Error");
                    return false;
                }
                salarioPorHora = salarioTotal / horasTotales;
                return true;
            }

        }

        public class CalcularMontoAsignacionFamiliar
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
}
