using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CapaNegocio;

namespace TestNomina
{
    [TestClass]
    public class TestCalcularDeduccionSeguros
    {
        [TestMethod]
        public void TestMethodCalcularDeduccionSeguros_ListaVacia()
        {
            var metodo = new MetodoCalcularDeduccionSeguros();
            var salarios = new List<decimal>();
            var resultado = metodo.CalcularDeduccionSeguros(salarios, 1);
            Assert.AreEqual(0m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionSeguros_ListaConValoresPositivos_Condicion1()
        {
            var metodo = new MetodoCalcularDeduccionSeguros();
            var salarios = new List<decimal> { 1000m, 2000m, 1500m };
            var resultado = metodo.CalcularDeduccionSeguros(salarios, 1);
            Assert.AreEqual(405m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionSeguros_ListaConValoresPositivos_Condicion2()
        {
            var metodo = new MetodoCalcularDeduccionSeguros();
            var salarios = new List<decimal> { 1000m, 2000m, 1500m };
            var resultado = metodo.CalcularDeduccionSeguros(salarios, 2);
            Assert.AreEqual(45m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionSeguros_ListaConValoresPositivos_Condicion3()
        {
            var metodo = new MetodoCalcularDeduccionSeguros();
            var salarios = new List<decimal> { 1000m, 2000m, 1500m };
            var resultado = metodo.CalcularDeduccionSeguros(salarios, 3);
            Assert.AreEqual(67.5m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionSeguros_ListaConUnSoloElemento()
        {
            var metodo = new MetodoCalcularDeduccionSeguros();
            var salarios = new List<decimal> { 5000m };
            var resultado = metodo.CalcularDeduccionSeguros(salarios, 1);
            Assert.AreEqual(450m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionSeguros_ListaConValoresNegativos()
        {
            var metodo = new MetodoCalcularDeduccionSeguros();
            var salarios = new List<decimal> { -1000m, -2000m };
            var resultado = metodo.CalcularDeduccionSeguros(salarios, 1);
            Assert.AreEqual(-270m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionSeguros_ValoresGrandes()
        {
            var metodo = new MetodoCalcularDeduccionSeguros();
            var salarios = new List<decimal> { 1000000m, 2000000m, 1500000m };
            var resultado = metodo.CalcularDeduccionSeguros(salarios, 1);
            Assert.AreEqual(405000m, resultado);
        }

        [TestMethod]
        public void TestMethodCalcularDeduccionSeguros_ValoresCercanosACero()
        {
            var metodo = new MetodoCalcularDeduccionSeguros();
            var salarios = new List<decimal> { 0.01m, 0.02m, 0.03m };
            var resultado = metodo.CalcularDeduccionSeguros(salarios, 1);
            Assert.AreEqual(0.0054m, resultado, 0.0001m);
        }
    }
}
