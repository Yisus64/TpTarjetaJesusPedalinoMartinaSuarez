using NUnit.Framework;
using TpSube;
using System;

namespace TpTarjetaJesusPedalinoTest
{
    public class TestsCuatro
    {
        Tarjeta tarjeta;
        FranquiciaMedia medioTarjeta;
        FranquiciaCompleta completaTarjeta;

        Colectivo colectivo;
        ColectivoInterurbano interurbano;

        TiempoFalso tiempo;
        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1);
            medioTarjeta = new FranquiciaMedia(2);
            completaTarjeta = new FranquiciaCompleta(3);
            colectivo = new Colectivo("K");
            interurbano = new ColectivoInterurbano("Expreso");
            tiempo = new TiempoFalso();
        }

        [Test]
        public void normalConDescuentosTest()
        {
            tarjeta.saldo = 36000;
            tarjeta.cantViajesMes = 28;
            Boleto fst = colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(fst.costo, Is.EqualTo(colectivo.getValorPasaje()));
            tarjeta.cantViajesMes = 78;
            Boleto snd = colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(snd.costo, Is.EqualTo(colectivo.getValorPasaje() * 0.8));
            Boleto thr = colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(thr.costo, Is.EqualTo(colectivo.getValorPasaje() * 0.75));
            tiempo.AgregarDias(40);
            Boleto frt = colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(frt.costo, Is.EqualTo(colectivo.getValorPasaje()));
        }

        [Test]
        public void franjaHorariaTest()
        {
            medioTarjeta.recargar(4000);
            completaTarjeta.recargar(4000);
            Boleto fsm = colectivo.pagarCon(medioTarjeta, tiempo);
            Boleto fsc = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.That(fsm.costo, Is.EqualTo(colectivo.getValorPasaje()));
            Assert.That(fsc.costo, Is.EqualTo(colectivo.getValorPasaje()));
            tiempo.AgregarMinutos(500);
            Boleto ssm = colectivo.pagarCon(medioTarjeta, tiempo);
            Boleto ssc = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.That(ssm.costo, Is.EqualTo(colectivo.getValorPasajeMedio()));
            Assert.That(ssc.costo, Is.EqualTo(colectivo.getValorPasajeCompleto()));
            tiempo.AgregarDias(6);
            Boleto tsm = colectivo.pagarCon(medioTarjeta, tiempo);
            Boleto tsc = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.That(tsm.costo, Is.EqualTo(colectivo.getValorPasaje()));
            Assert.That(tsc.costo, Is.EqualTo(colectivo.getValorPasaje()));
        }

        [Test]
        [TestCase(3000)]
        [TestCase(4000)]
        [TestCase(5000)]
        [TestCase(6000)]
        public void interurbanoTest(float saldo)
        {
            tarjeta.saldo = saldo;
            Boleto viaje = interurbano.pagarCon(tarjeta, tiempo);
            Assert.That(viaje.costo, Is.EqualTo(interurbano.getValorPasaje()));
            Assert.That(tarjeta.saldo, Is.EqualTo(saldo - 2500));
        }

        [Test]
        [TestCase(3000)]
        [TestCase(4000)]
        [TestCase(5000)]
        [TestCase(6000)]
        public void interurbanoMedioTest(float saldo)
        {
            tiempo.AgregarMinutos(500);
            medioTarjeta.saldo = saldo;
            Boleto viaje = interurbano.pagarCon(medioTarjeta, tiempo);
            Assert.That(viaje.costo, Is.EqualTo(interurbano.getValorPasajeMedio()));
            Assert.That(medioTarjeta.saldo, Is.EqualTo(saldo - 1250));
        }

        [Test]
        [TestCase(3000)]
        [TestCase(4000)]
        [TestCase(5000)]
        [TestCase(6000)]
        public void interurbanoCompletoTest(float saldo)
        {
            tiempo.AgregarMinutos(500);
            completaTarjeta.saldo = saldo;
            Boleto viaje = interurbano.pagarCon(completaTarjeta, tiempo);
            Assert.That(viaje.costo, Is.EqualTo(interurbano.getValorPasajeCompleto()));
            Assert.That(completaTarjeta.saldo, Is.EqualTo(saldo));
        }
    }
}