using NUnit.Framework;
using TpSube;
using System;

namespace TpTarjetaJesusPedalinoTest
{
    public class TestTarjeta
    {
        Tarjeta tarjeta;
        Colectivo colectivo;
        TiempoFalso tiempo;
        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1);
            colectivo = new Colectivo("K");
            tiempo = new TiempoFalso();
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
        public void saldoRestanteTest()
        {
            tarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000-colectivo.getValorPasaje()));
            Assert.NotNull(viaje);
        }

        [Test]
        public void saldoNegativoTest()
        {
            tarjeta.recargar(2000);
            colectivo.pagarCon(tarjeta, tiempo);
            colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldoActual(), Is.EqualTo(-400));
            var ex = Assert.Throws<Exception>(() => colectivo.pagarCon(tarjeta, tiempo));
            Assert.AreEqual("Saldo insuficiente", ex.Message);
        }

        [Test]
        public void descuentoSaldoAdeudadoTest()
        {
            tarjeta.recargar(2000);
            colectivo.pagarCon(tarjeta, tiempo);
            colectivo.pagarCon(tarjeta, tiempo);
            tarjeta.recargar(2000);
            Assert.That(tarjeta.saldoActual(), Is.EqualTo(-400 + 2000));
        }


        [Test]
        public void saldoNuevoTest()
        {
            tiempo.AgregarMinutos(1000);
            while (tarjeta.saldoActual() < 32000)
            {
                tarjeta.recargar(4000);
            }

            tarjeta.recargar(9000);
            Assert.That(tarjeta.saldo, Is.EqualTo(36000));
            Assert.That(tarjeta.pendiente, Is.EqualTo(5000));
            colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(36000));
            Assert.That(tarjeta.pendiente, Is.EqualTo(3800));
            colectivo.pagarCon(tarjeta, tiempo);
            colectivo.pagarCon(tarjeta, tiempo);
            colectivo.pagarCon(tarjeta, tiempo);
            colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(35000));
            Assert.That(tarjeta.pendiente, Is.EqualTo(0));
        }
    }
}