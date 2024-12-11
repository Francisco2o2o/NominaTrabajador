using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CapaPresentacion.Utilidades;

namespace TestNomina
{
    [TestClass]
    public class TestEstablecerFechasContrato
    {
        [TestMethod]
        public void TestEstablecerFechasContrato_CorrectaDuracion()
        {
            int mesesDuracion = 12;
            DateTime fechaInicial, fechaFinal;

            MetodoSonFechasValidarContrato.EstablecerFechasContrato(out fechaInicial, out fechaFinal, mesesDuracion);

            DateTime expectedFechaFinal = fechaInicial.AddMonths(mesesDuracion);

            Assert.AreEqual(expectedFechaFinal, fechaFinal);
            Assert.AreEqual(DateTime.Now.Date, fechaInicial.Date);
        }

        [TestMethod]
        public void TestEstablecerFechasContrato_CeroMesesDuracion()
        {
            int mesesDuracion = 0;
            DateTime fechaInicial, fechaFinal;

            MetodoSonFechasValidarContrato.EstablecerFechasContrato(out fechaInicial, out fechaFinal, mesesDuracion);

            Assert.AreEqual(DateTime.Now.Date, fechaInicial.Date);
            Assert.AreEqual(DateTime.Now.Date, fechaFinal.Date);
        }

     
        [TestMethod]
        public void TestEstablecerFechasContrato_MesesDuracionExtremos()
        {
            int mesesDuracion = 1200; 
            DateTime fechaInicial, fechaFinal;

            MetodoSonFechasValidarContrato.EstablecerFechasContrato(out fechaInicial, out fechaFinal, mesesDuracion);

            DateTime expectedFechaFinal = fechaInicial.AddMonths(mesesDuracion);

            Assert.AreEqual(expectedFechaFinal, fechaFinal);
            Assert.AreEqual(DateTime.Now.Date, fechaInicial.Date);
        }
    }
}
