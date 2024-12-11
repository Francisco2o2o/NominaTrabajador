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

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConCeros()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 0m, 0m, 0m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(0m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConValoresGrandes()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 1_000_000m, 2_000_000m, 3_000_000m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(6_000_000m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConValoresDecimales()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 1234.56m, 789.10m, 456.78m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(2480.44m, resultado);
        }

        
        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConValorMaximo()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { decimal.MaxValue };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(decimal.MaxValue, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConUnElementoNegativo()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { -999.99m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(-999.99m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConNumerosMuyPequeños()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 0.0001m, 0.0002m, 0.0003m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(0.0006m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConNumerosMuyGrandes()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 9_999_999_999m, 1_000_000_001m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(11_000_000_000m, resultado);
        }

        

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConValoresAltamenteRedondeados()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 100.456789m, 200.987654m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(301.44m, Math.Round(resultado, 2));
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaMixtaConValoresPequeños()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 0.01m, -0.01m, 1000m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(1000m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConCasiMaximos()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { decimal.MaxValue - 1m, 1m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(decimal.MaxValue, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaCasiVaciaConCero()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 0m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(0m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConValoresAleatorios()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 345m, 678.90m, 123.45m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(1147.35m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularMontoTotalPago_ListaConNumerosRepetidos()
        {
            var metodo = new MetodoCalcularMontoTotalPago();
            var salarios = new List<decimal> { 500m, 500m, 500m, 500m };
            var resultado = metodo.CalcularMontoTotalPago(salarios);
            Assert.AreEqual(2000m, resultado);
        }
    }
}
