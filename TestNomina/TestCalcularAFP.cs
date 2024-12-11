using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CapaNegocio; // Asegúrate de que el espacio de nombres sea el correcto

namespace TestNomina
{
    [TestClass]
    public class TestCalcularAFP
    {
        [TestMethod]
        public void TestMethodCalcularAFP_ListaVacia()
        {
            var metodo = new MetodoCalcularAFP();
            var salarios = new List<decimal>();

            var resultado = metodo.CalcularAFP(salarios);

            Assert.AreEqual(0m, resultado, "El cálculo de AFP para una lista vacía debe ser 0.");
        }

        [TestMethod]
        public void TestMethodCalcularAFP_ListaConValoresPositivos()
        {
            var metodo = new MetodoCalcularAFP();
            var salarios = new List<decimal> { 1000m, 2000m, 1500m };

            
            var resultado = metodo.CalcularAFP(salarios);

            Assert.AreEqual(450m, resultado, "El cálculo de AFP para los salarios proporcionados no es correcto.");
        }

        [TestMethod]
        public void TestMethodCalcularAFP_ListaConUnSoloElemento()
        {
            var metodo = new MetodoCalcularAFP();
            var salarios = new List<decimal> { 5000m };

            var resultado = metodo.CalcularAFP(salarios);

            Assert.AreEqual(500m, resultado, "El cálculo de AFP para un solo salario no es correcto.");
        }

        [TestMethod]
        public void TestMethodCalcularAFP_ListaConCero()
        {
            var metodo = new MetodoCalcularAFP();
            var salarios = new List<decimal> { 0m };

            var resultado = metodo.CalcularAFP(salarios);

            Assert.AreEqual(0m, resultado, "El cálculo de AFP para un salario de cero debe ser 0.");
        }
    }
}
