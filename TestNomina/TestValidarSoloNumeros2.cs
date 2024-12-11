using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CapaPresentacion.Utilidades;

namespace TestNomina
{
    [TestClass]
    public class TestValidarSoloNumeros2
    {
        [TestMethod]
        public void TestValidarSoloNumeros2_ValidarDocumentoValido()
        {
            string texto = "1234567";
            int tipoCon = 1;
            char keyChar = '8';
            string mensajeError;

            bool resultado = MetodoSoloSonNumeros2.ValidarSoloNumeros2(texto, tipoCon, keyChar, out mensajeError);

            Assert.IsTrue(resultado);
            Assert.AreEqual(string.Empty, mensajeError);
        }

        [TestMethod]
        public void TestValidarSoloNumeros2_DocumentoInvalido()
        {
            string texto = "12345678";
            int tipoCon = 1;
            char keyChar = '9';
            string mensajeError;

            bool resultado = MetodoSoloSonNumeros2.ValidarSoloNumeros2(texto, tipoCon, keyChar, out mensajeError);

            Assert.IsFalse(resultado);
            Assert.AreEqual("El documento debe tener exactamente 8 caracteres.", mensajeError);
        }

        [TestMethod]
        public void TestValidarSoloNumeros2_ValidarTelefonoValido()
        {
            string texto = "12345678";
            int tipoCon = 2;
            char keyChar = '9';
            string mensajeError;

            bool resultado = MetodoSoloSonNumeros2.ValidarSoloNumeros2(texto, tipoCon, keyChar, out mensajeError);

            Assert.IsTrue(resultado);
            Assert.AreEqual(string.Empty, mensajeError);
        }

        [TestMethod]
        public void TestValidarSoloNumeros2_TelefonoInvalido()
        {
            string texto = "123456789";
            int tipoCon = 2;
            char keyChar = '0';
            string mensajeError;

            bool resultado = MetodoSoloSonNumeros2.ValidarSoloNumeros2(texto, tipoCon, keyChar, out mensajeError);

            Assert.IsFalse(resultado);
            Assert.AreEqual("El Telefono debe tener exactamente 9 caracteres.", mensajeError);
        }

        [TestMethod]
        public void TestValidarSoloNumeros2_ValidarSoloNumeros()
        {
            string texto = "12345";
            int tipoCon = 1;
            char keyChar = 'a';
            string mensajeError;

            bool resultado = MetodoSoloSonNumeros2.ValidarSoloNumeros2(texto, tipoCon, keyChar, out mensajeError);

            Assert.IsFalse(resultado);
            Assert.AreEqual("Por favor, ingresa solo números en el campo.", mensajeError);
        }
    }
}
