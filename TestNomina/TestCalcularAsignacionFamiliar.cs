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

        
        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_SalarioExtremadamenteAlto()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 1_000_000m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(100_000m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_DecimalesLargos()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 1234.56789m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(123.46m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_MinimoPositivo()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 0.01m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(0.00m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_MaximoDecimalPrecision()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 9999.9999m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(1000.00m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_ExactitudEnMultiploDiez()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 1230m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(123m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_NumeroGrandeConDecimales()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 1_234_567.89m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(123_456.79m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularAsignacionFamiliar_ValorMuyPequeno()
        {
            var metodo = new MetodoCalcularAsignacionFamiliar();
            var salarioBase = 0.0001m;
            var resultado = metodo.CalcularAsignacionFamiliar(salarioBase);
            Assert.AreEqual(0.00m, resultado);
        }
    }
}
