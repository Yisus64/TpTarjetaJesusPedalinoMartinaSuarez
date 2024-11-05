using NUnit.Framework;
using TpSube;
using System;

namespace TpTarjetaJesusPedalinoTest
{
    public class TestsDos
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
        public void pagarConYSinSaldo()
        {
            tarjeta.recargar(2000);
            colectivo.pagarCon(tarjeta);
            float saldoPrim = tarjeta.saldoActual();
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - 940));
            colectivo.pagarCon(tarjeta);
            float saldoSeg = tarjeta.saldoActual();
            Assert.That(saldoSeg, Is.LessThan(saldoPrim));
            var ex = Assert.Throws<Exception>(() => colectivo.pagarCon(tarjeta));
            Assert.AreEqual("Saldo insuficiente", ex.Message);
            float saldoTer = tarjeta.saldoActual();
            Assert.That(saldoSeg, Is.EqualTo(saldoTer));
        }
    }
}
