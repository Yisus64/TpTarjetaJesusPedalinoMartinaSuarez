using NUnit.Framework;
using TpSube;
using System;

namespace TpTarjetaJesusPedalinoTest
{
    public class TestsCompleto
    {
        FranquiciaCompleta completaTarjeta;

        Colectivo colectivo;

        TiempoFalso tiempo;

        [SetUp]
        public void Setup()
        {
            completaTarjeta = new FranquiciaCompleta(3);
            colectivo = new Colectivo("K");
            tiempo = new TiempoFalso();
        }

        [Test]
        public void soloDosGratisYPosterioresNormalesTest()
        {
            tiempo.AgregarMinutos(1000);
            completaTarjeta.recargar(2000);
            Boleto fst = colectivo.pagarCon(completaTarjeta, tiempo);
            Boleto snd = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.That(fst.costo, Is.EqualTo(0));
            Assert.That(snd.costo, Is.EqualTo(0));
            Boleto thr = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.That(thr.costo, Is.EqualTo(colectivo.getValorPasaje()));
        }
    }
}