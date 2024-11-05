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
    }
}