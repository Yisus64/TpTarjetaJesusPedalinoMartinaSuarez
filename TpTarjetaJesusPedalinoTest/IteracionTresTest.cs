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

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1);
            medioTarjeta = new FranquiciaMedia(2);
            completaTarjeta = new FranquiciaCompleta(3);
            colectivo = new Colectivo("K");
        }

        [Test]
        public void boletoAsiNomasTest()
        {
            tarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(tarjeta);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Sin Franquicia"));
        }

        [Test]
        public void boletoMedioTest()
        {
            medioTarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(medioTarjeta);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Franquicia Media"));
        }

        [Test]
        public void boletoCompletoTest()
        {
            completaTarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(completaTarjeta);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Franquicia Completa"));
        }
    }
}