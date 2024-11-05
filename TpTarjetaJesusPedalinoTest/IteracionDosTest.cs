using NUnit.Framework;
using TpSube;
using System;

namespace TpTarjetaJesusPedalinoTest
{
    public class TestsDos
    {
        Tarjeta tarjeta;
        FranquiciaMedia medioTarjeta;
        FranquiciaCompleta completaTarjeta;

        Colectivo colectivo;
        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1);
            medioTarjeta = new FranquiciaMedia(2);
            completaTarjeta = new FranquiciaCompleta(3);
            colectivo = new Colectivo();
        }

        [Test]
        public void pagarConYSinSaldoTest()
        {
            tarjeta.recargar(2000);
            colectivo.pagarCon(tarjeta);
            float saldoPrim = tarjeta.saldoActual();
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - colectivo.getValorPasaje()));
            colectivo.pagarCon(tarjeta);
            float saldoSeg = tarjeta.saldoActual();
            Assert.That(saldoSeg, Is.LessThan(saldoPrim));
            var ex = Assert.Throws<Exception>(() => colectivo.pagarCon(tarjeta));
            Assert.AreEqual("Saldo insuficiente", ex.Message);
            float saldoTer = tarjeta.saldoActual();
            Assert.That(saldoSeg, Is.EqualTo(saldoTer));
        }

        [Test]
        public void saldoNegativoTest()
        {
            tarjeta.saldo = 460;
            colectivo.pagarCon(tarjeta);
            Assert.That(tarjeta.saldoActual(), Is.EqualTo(-480));
            var ex = Assert.Throws<Exception>(() => colectivo.pagarCon(tarjeta));
            Assert.AreEqual("Saldo insuficiente", ex.Message);
        }

        [Test]
        public void descuentoSaldoAdeudadoTest()
        {
            tarjeta.saldo = -480;
            tarjeta.recargar(2000);
            Assert.That(tarjeta.saldoActual(), Is.EqualTo(-480 + 2000));
        }

        [Test]
        public void completaSiemprePagaTest()
        {
            completaTarjeta.recargar(2000);
            Boleto viaje1 = colectivo.pagarCon(completaTarjeta);
            Assert.That(viaje1.costo, Is.EqualTo(0));
            Boleto viaje2 = colectivo.pagarCon(completaTarjeta);
            Assert.That(viaje2.costo, Is.EqualTo(0));
            Boleto viaje3 = colectivo.pagarCon(completaTarjeta);
            Assert.That(viaje3.costo, Is.EqualTo(0));
            Assert.That(completaTarjeta.saldo, Is.EqualTo(2000 - colectivo.getValorPasajeCompleto()));
        }

        [Test]

        public void medioBoletoPagoTest()
        {
            medioTarjeta.recargar(2000);
            float saldoEsperado = medioTarjeta.saldo - colectivo.getValorPasajeMedio();
            Boleto viaje = colectivo.pagarCon(medioTarjeta);
            Assert.That(viaje.costo, Is.EqualTo(colectivo.getValorPasajeMedio()));
            Assert.That(medioTarjeta.saldo, Is.EqualTo(saldoEsperado));
        }
    }
}
