using NUnit.Framework;
using TpSube;
using System;
using System.Linq;
using System.Collections.Generic;
namespace TpTarjetaJesusPedalinoTest
{
    public class TestsTres
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
        public void boletoAsiNomasTest()
        {
            tarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(tarjeta, tiempo);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Sin Franquicia"));
        }

        [Test]
        public void boletoMedioTest()
        {
            medioTarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(medioTarjeta, tiempo);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Franquicia Media"));
        }

        [Test]
        public void boletoCompletoTest()
        {
            completaTarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Franquicia Completa"));
        }

        /*[Test]
        public void tiempoTest()
        {
            DateTime primero = tiempo.Now();
            tiempo.AgregarMinutos(5);
            DateTime segundo = tiempo.Now();
            Assert.That(segundo.Minute, Is.GreaterThan(primero.Minute));

        }*/

        [Test]
        public void cincoMinsMedioTest()
        {
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
            medioTarjeta.recargar(4000);
            Boleto fst = colectivo.pagarCon(medioTarjeta, tiempo);
            tiempo.AgregarMinutos(5);
            Boleto snd = colectivo.pagarCon(medioTarjeta, tiempo);
            tiempo.AgregarMinutos(5);
            Boleto thr = colectivo.pagarCon(medioTarjeta, tiempo);
            tiempo.AgregarMinutos(5);
            Boleto frh = colectivo.pagarCon(medioTarjeta, tiempo);
            tiempo.AgregarMinutos(5);
            Assert.That(medioTarjeta.saldo, Is.EqualTo(2120));
            Assert.That(medioTarjeta.cantViajesHoy, Is.EqualTo(4));
            Boleto fth = colectivo.pagarCon(medioTarjeta, tiempo);
            Assert.That(fth.costo, Is.EqualTo(940));
            tiempo.AgregarDias(1);
            Boleto sth = colectivo.pagarCon(medioTarjeta, tiempo);
            Assert.That(sth.costo, Is.EqualTo(470));
        }

        [Test]
        public void soloDosGratisYPosterioresNormalesTest()
        {
            completaTarjeta.recargar(2000);
            Boleto fst = colectivo.pagarCon(completaTarjeta, tiempo);
            Boleto snd = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.That(fst.costo, Is.EqualTo(0));
            Assert.That(snd.costo, Is.EqualTo(0));
            Boleto thr = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.That(thr.costo, Is.EqualTo(940));
        }

        [Test]
        public void saldoNuevoTest()
        {
            tarjeta.saldo = 35000;
            tarjeta.recargar(2000);
            Assert.That(tarjeta.saldo, Is.EqualTo(36000));
            Assert.That(tarjeta.pendiente, Is.EqualTo(1000));
            colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(36000));
            Assert.That(tarjeta.pendiente, Is.EqualTo(60));
            colectivo.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(35120));
            Assert.That(tarjeta.pendiente, Is.EqualTo(0));
        }
    }
}