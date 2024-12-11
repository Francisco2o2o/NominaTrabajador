using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CapaNegocio;

namespace TestNomina
{
    [TestClass]
    public class TestCalcularDeduccionImpuestos
    {
        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaVacia()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal>();
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(0m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaConUnSoloElemento()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 5000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(400m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaConCero()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 0m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(0m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaConValoresNegativos()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { -1000m, -2000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(-240m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaMixta()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 1000m, -2000m, 3000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(160m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ValoresAltos()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 100000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(8000m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ValoresMuyBajos()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 1m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(0.08m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_SoloImpuestoBase()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 1000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(80m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaConDecimales()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 1500.50m, 2000.75m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(280.10m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ValoresDuplicados()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 1000m, 1000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(160m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ValoresNulos()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 0m, 0m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(0m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_MultiplesValoresGrandes()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 50000m, 60000m, 70000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(14400m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_SalariosMinimos()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 1025m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(82m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaConCientos()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 300m, 700m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(80m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaConMil()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 1000m, 2000m, 3000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(480m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ListaConTresDecimales()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 1234.567m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(98.77m, resultado, 0.01m); 
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_RangoDeValores()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 100m, 200m, 300m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(48m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionImpuestos_ValoresEnMiles()
        {
            var metodo = new MetodoCalcularDeduccionImpuestos();
            var salarios = new List<decimal> { 10000m, 20000m };
            var resultado = metodo.CalcularDeduccionImpuestos(salarios);
            Assert.AreEqual(2400m, resultado);
        }
    }
}
