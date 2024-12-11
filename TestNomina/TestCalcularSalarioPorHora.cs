using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CapaNegocio;

namespace TestNomina
{
    [TestClass]
    public class TestCalcularSalarioPorHora
    {
        [TestMethod]
        public void TestMethodCalcularSalarioPorHora_Valido()
        {
            var metodo = new MetodoCalcularSalarioPorHora();
            decimal salarioTotal = 1600.00m;
            int horasTotales = 160;
            decimal salarioPorHora;

            bool resultado = metodo.CalcularSalarioPorHora(salarioTotal, horasTotales, out salarioPorHora);

            Assert.IsTrue(resultado);
            Assert.AreEqual(10.00m, salarioPorHora);
        }

        [TestMethod]
        public void TestMethodCalcularSalarioPorHora_SalarioInvalido()
        {
            var metodo = new MetodoCalcularSalarioPorHora();
            decimal salarioTotal = -1600.00m;
            int horasTotales = 160;
            decimal salarioPorHora;

            bool resultado = metodo.CalcularSalarioPorHora(salarioTotal, horasTotales, out salarioPorHora);

            Assert.IsFalse(resultado);
            Assert.AreEqual(0.00m, salarioPorHora);
        }

        [TestMethod]
        public void TestMethodCalcularSalarioPorHora_HorasInvalidas()
        {
            var metodo = new MetodoCalcularSalarioPorHora();
            decimal salarioTotal = 1600.00m;
            int horasTotales = -160;
            decimal salarioPorHora;

            bool resultado = metodo.CalcularSalarioPorHora(salarioTotal, horasTotales, out salarioPorHora);

            Assert.IsFalse(resultado);
            Assert.AreEqual(0.00m, salarioPorHora);
        }

        [TestMethod]
        public void TestMethodCalcularSalarioPorHora_SalarioYHorasInvalidas()
        {
            var metodo = new MetodoCalcularSalarioPorHora();
            decimal salarioTotal = -1600.00m;
            int horasTotales = -160;
            decimal salarioPorHora;

            bool resultado = metodo.CalcularSalarioPorHora(salarioTotal, horasTotales, out salarioPorHora);

            Assert.IsFalse(resultado);
            Assert.AreEqual(0.00m, salarioPorHora);
        }
    }
}
