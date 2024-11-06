using NUnit.Framework;
using TpSube;
using System;

namespace TpTarjetaJesusPedalinoTest
{
    public class TestsJubilados
    {
        Tarjeta tarjeta;
        FranquiciaMedia medioTarjeta;
        FranquiciaCompleta completaTarjeta;
        FranquiciaJubilados jubiladoTarjeta;

        Colectivo colectivo;

        TiempoFalso tiempo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1);
            medioTarjeta = new FranquiciaMedia(2);
            completaTarjeta = new FranquiciaCompleta(3);
            jubiladoTarjeta = new FranquiciaJubilados(5);
            colectivo = new Colectivo("K");
            tiempo = new TiempoFalso();
        }

        [Test]
        public void jubiladoSiempreGratisSemanaDe6a22Test()
        {
            tiempo.AgregarMinutos(1000);
            jubiladoTarjeta.recargar(2000);
            Boleto viaje1 = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.That(viaje1.costo, Is.EqualTo(0));
            Boleto viaje2 = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.That(viaje2.costo, Is.EqualTo(0));
            Boleto viaje3 = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.That(viaje3.costo, Is.EqualTo(0));
            Boleto viaje4 = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.That(viaje4.costo, Is.EqualTo(0));
            Assert.That(jubiladoTarjeta.saldo, Is.EqualTo(2000));
        }

        [Test]
        public void jubiladoFindeTest()
        {
            tiempo.AgregarMinutos(1000);
            jubiladoTarjeta.recargar(2000);
            Boleto viaje1 = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.That(viaje1.costo, Is.EqualTo(0));
            tiempo.AgregarDias(6);
            Boleto viaje2 = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.That(viaje2.costo, Is.EqualTo(1200));
        }

        [Test]
        public void jubiladoNocheTest()
        {
            jubiladoTarjeta.recargar(2000);
            Boleto viaje1 = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.That(viaje1.costo, Is.EqualTo(1200));
            tiempo.AgregarMinutos(1000);
            Boleto viaje2 = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.That(viaje2.costo, Is.EqualTo(0));
        }
    }
}