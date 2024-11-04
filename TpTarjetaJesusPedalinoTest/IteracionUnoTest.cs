using NUnit.Framework;
using TpSube;
using System;

namespace TpTarjetaJesusPedalinoTest
{
    public class Tests
    {
        Tarjeta tarjeta;
        Colectivo colectivo;
        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta();
            colectivo = new Colectivo();
        }

        [Test]
        [TestCase(2000)]
        [TestCase(3000)]
        [TestCase(4000)]
        [TestCase(5000)]
        [TestCase(6000)]
        [TestCase(7000)]
        [TestCase(8000)]
        [TestCase(9000)]
        public void recargarTest(int c)
        {
            tarjeta.recargar(c);
            Assert.That(tarjeta.saldo, Is.EqualTo(c));

        }
        [Test]
        public void reargaNoValidaTest()
        {
            int montoNoPermitido = 69;
            var ex = Assert.Throws<Exception> (() => tarjeta.recargar(montoNoPermitido));
            Assert.AreEqual("Monto de carga invalido", ex.Message);
        }

        [Test]
        public void recargaMayorTest()
        {
            tarjeta.recargar(9000);
            int montoMayor = 69420;

            var ex = Assert.Throws<Exception>(() => tarjeta.recargar(montoMayor));
            Assert.AreEqual("Excede el limite de saldo", ex.Message);

        }

        [Test]
        public void pagarTest()
        {
            tarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(tarjeta);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000-940));
            Assert.NotNull(viaje);
        }
    }
}