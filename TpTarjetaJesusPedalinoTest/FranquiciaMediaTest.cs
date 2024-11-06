using NUnit.Framework;
using TpSube;
using System;

namespace TpTarjetaJesusPedalinoTest
{
    public class TestsMedio
    {
        Tarjeta tarjeta;
        FranquiciaMedia medioTarjeta;
        FranquiciaCompleta completaTarjeta;

        Colectivo colectivo;

        TiempoFalso tiempo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1);
            medioTarjeta = new FranquiciaMedia(2);
            completaTarjeta = new FranquiciaCompleta(3);
            colectivo = new Colectivo("K");
            tiempo = new TiempoFalso();
        }

        [Test]

        public void medioBoletoPagoTest()
        {
            tiempo.AgregarMinutos(1000);
            medioTarjeta.recargar(2000);
            float saldoEsperado = medioTarjeta.saldo - colectivo.getValorPasajeMedio();
            Boleto viaje = colectivo.pagarCon(medioTarjeta, tiempo);
            Assert.That(viaje.costo, Is.EqualTo(colectivo.getValorPasajeMedio()));
            Assert.That(medioTarjeta.saldo, Is.EqualTo(saldoEsperado));
        }

        [Test]
        public void cincoMinsMedioTest()
        {
            tiempo.AgregarMinutos(1000);
            medioTarjeta.recargar(4000);
            Boleto primero = colectivo.pagarCon(medioTarjeta, tiempo);
            var ex1 = Assert.Throws<Exception>(() => colectivo.pagarCon(medioTarjeta, tiempo));
            Assert.AreEqual("No pasaron 5 minutos", ex1.Message);
            tiempo.AgregarMinutos(2);
            var ex2 = Assert.Throws<Exception>(() => colectivo.pagarCon(medioTarjeta, tiempo));
            Assert.AreEqual("No pasaron 5 minutos", ex2.Message);
            tiempo.AgregarMinutos(3);
            Boleto segundo = colectivo.pagarCon(medioTarjeta, tiempo);
            Assert.That((segundo.fechaUltimoViaje - primero.fechaUltimoViaje).Minutes, Is.EqualTo(5));
        }

        [Test]
        public void masDeCuatroViajesNoTest()
        {
            tiempo.AgregarMinutos(1000);
            medioTarjeta.recargar(4000);
            Boleto fst = colectivo.pagarCon(medioTarjeta, tiempo);
            tiempo.AgregarMinutos(5);
            Boleto snd = colectivo.pagarCon(medioTarjeta, tiempo);
            tiempo.AgregarMinutos(5);
            Boleto thr = colectivo.pagarCon(medioTarjeta, tiempo);
            tiempo.AgregarMinutos(5);
            Boleto frh = colectivo.pagarCon(medioTarjeta, tiempo);
            tiempo.AgregarMinutos(5);
            Assert.That(medioTarjeta.saldo, Is.EqualTo(4000 - colectivo.getValorPasajeMedio() * 4));
            Assert.That(medioTarjeta.cantViajesHoy, Is.EqualTo(4));
            Boleto fth = colectivo.pagarCon(medioTarjeta, tiempo);
            Assert.That(fth.costo, Is.EqualTo(colectivo.getValorPasaje()));
            tiempo.AgregarDias(1);
            Boleto sth = colectivo.pagarCon(medioTarjeta, tiempo);
            Assert.That(sth.costo, Is.EqualTo(colectivo.getValorPasajeMedio()));
        }
    }
}