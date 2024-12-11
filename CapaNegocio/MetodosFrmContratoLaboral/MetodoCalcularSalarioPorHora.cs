using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CapaNegocio
{
    public class MetodoCalcularSalarioPorHora
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
}
