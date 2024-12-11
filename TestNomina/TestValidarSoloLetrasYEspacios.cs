using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CapaPresentacion.Utilidades;

namespace TestNomina
{
    [TestClass]
    public class TestValidarSoloLetrasYEspacios
    {
        [TestMethod]
        public void TestValidarSoloLetrasYEspacios_ValidarLetrasYEspacios()
        {
            string texto = "Nombre de usuario";
            char keyChar = 'a';
            string mensajeError;

            bool resultado = MetodoSoloSonLetrasYEspacios.ValidarSoloLetrasYEspacios(texto, keyChar, out mensajeError);

            Assert.IsTrue(resultado);
            Assert.AreEqual(string.Empty, mensajeError);
        }

        [TestMethod]
        public void TestValidarSoloLetrasYEspacios_SimbolosNoPermitidos()
        {
            string texto = "Nombre de usuario";
            char keyChar = '@';
            string mensajeError;

            bool resultado = MetodoSoloSonLetrasYEspacios.ValidarSoloLetrasYEspacios(texto, keyChar, out mensajeError);

            Assert.IsFalse(resultado);
            Assert.AreEqual("Por favor, ingresa solo letras y espacios en este campo.", mensajeError);
        }

        [TestMethod]
        public void TestValidarSoloLetrasYEspacios_EspaciosPermitidos()
        {
            string texto = "Nombre de usuario";
            char keyChar = ' ';
            string mensajeError;

            bool resultado = MetodoSoloSonLetrasYEspacios.ValidarSoloLetrasYEspacios(texto, keyChar, out mensajeError);

            Assert.IsTrue(resultado);
            Assert.AreEqual(string.Empty, mensajeError);
        }

        [TestMethod]
        public void TestValidarSoloLetrasYEspacios_LetraValida()
        {
            string texto = "Nombre de usuario";
            char keyChar = 'N';
            string mensajeError;

            bool resultado = MetodoSoloSonLetrasYEspacios.ValidarSoloLetrasYEspacios(texto, keyChar, out mensajeError);

            Assert.IsTrue(resultado);
            Assert.AreEqual(string.Empty, mensajeError);
        }

        [TestMethod]
        public void TestValidarSoloLetrasYEspacios_CaracterNoValido()
        {
            string texto = "Nombre de usuario";
            char keyChar = '1';
            string mensajeError;

            bool resultado = MetodoSoloSonLetrasYEspacios.ValidarSoloLetrasYEspacios(texto, keyChar, out mensajeError);

            Assert.IsFalse(resultado);
            Assert.AreEqual("Por favor, ingresa solo letras y espacios en este campo.", mensajeError);
        }
    }
}
