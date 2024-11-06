﻿using NUnit.Framework;
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
        public void pagarConYSinSaldoTest()
        {
            tarjeta.recargar(2000);
            colectivo.pagarCon(tarjeta, tiempo);
            float saldoPrim = tarjeta.saldoActual();
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - colectivo.getValorPasaje()));
            colectivo.pagarCon(tarjeta, tiempo);
            float saldoSeg = tarjeta.saldoActual();
            Assert.That(saldoSeg, Is.LessThan(saldoPrim));
            var ex = Assert.Throws<Exception>(() => colectivo.pagarCon(tarjeta, tiempo));
            Assert.AreEqual("Saldo insuficiente", ex.Message);
            float saldoTer = tarjeta.saldoActual();
            Assert.That(saldoSeg, Is.EqualTo(saldoTer));
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
            Assert.That(ssc.costo, Is.EqualTo(colectivo.getValorPasajeCompletoYJubilados()));
            tiempo.AgregarDias(6);
            Boleto tsm = colectivo.pagarCon(medioTarjeta, tiempo);
            Boleto tsc = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.That(tsm.costo, Is.EqualTo(colectivo.getValorPasaje()));
            Assert.That(tsc.costo, Is.EqualTo(colectivo.getValorPasaje()));
        }
    }
}