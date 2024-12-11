using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CapaNegocio;

namespace TestNomina
{
    [TestClass]
    public class TestCalcularAsignacionFamiliar
    {
        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_ValorPositivo()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 2000m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(200m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_ValorCero()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 0m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(0m, resultado);
        }

          
        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_Redondeo()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 1234.56m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(123.46m, resultado);
        }
    }
}
