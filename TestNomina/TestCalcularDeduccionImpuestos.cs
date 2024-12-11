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
    }
}
