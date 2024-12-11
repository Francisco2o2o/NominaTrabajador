using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CapaNegocio;

namespace TestNomina
{
    [TestClass]
    public class TestCalcularMontoTotalPago
    {
        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaVacia()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal>();
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(0m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConValoresPositivos()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 1000m, 2000m, 1500m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(4500m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConUnSoloElemento()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 5000m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(5000m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConValoresNegativos()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { -1000m, -2000m, -1500m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(-4500m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaMixta()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 1000m, -2000m, 1500m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(500m, resultado);
        }
    }
}