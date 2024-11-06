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
        FranquiciaJubilados jubiladoTarjeta;

        Colectivo colectivo;

        TiempoFalso tiempo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1);
            medioTarjeta = new FranquiciaMedia(2);
            completaTarjeta = new FranquiciaCompleta(3);
            jubiladoTarjeta = new FranquiciaJubilados(4);
            colectivo = new Colectivo("K");
            tiempo = new TiempoFalso();
        }

        [Test]
        public void boletoCompletoTest()
        {
            completaTarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(completaTarjeta, tiempo);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Franquicia Completa"));
            Assert.That(viaje.idTarjeta, Is.EqualTo(completaTarjeta.ID));
            Assert.That(viaje.lineaDeColectivo, Is.EqualTo(colectivo.linea));
            Assert.That(viaje.saldoTarjeta, Is.EqualTo(completaTarjeta.saldo));
        }

        [Test]
        public void boletoMedioTest()
        {
            medioTarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(medioTarjeta, tiempo);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Franquicia Media"));
            Assert.That(viaje.idTarjeta, Is.EqualTo(medioTarjeta.ID));
            Assert.That(viaje.lineaDeColectivo, Is.EqualTo(colectivo.linea));
            Assert.That(viaje.saldoTarjeta, Is.EqualTo(medioTarjeta.saldo));
        }

        [Test]
        public void boletoAsiNomasTest()
        {
            tarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(tarjeta, tiempo);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Sin Franquicia"));
            Assert.That(viaje.idTarjeta, Is.EqualTo(tarjeta.ID));
            Assert.That(viaje.lineaDeColectivo, Is.EqualTo(colectivo.linea));
            Assert.That(viaje.saldoTarjeta, Is.EqualTo(tarjeta.saldo));
        }

        [Test]
        public void boletoJubiladoTest()
        {
            jubiladoTarjeta.recargar(2000);
            Boleto viaje = colectivo.pagarCon(jubiladoTarjeta, tiempo);
            Assert.NotNull(viaje);
            Assert.That(viaje.tipoDeTarjeta, Is.EqualTo("Franquicia Jubilados"));
            Assert.That(viaje.idTarjeta, Is.EqualTo(jubiladoTarjeta.ID));
            Assert.That(viaje.lineaDeColectivo, Is.EqualTo(colectivo.linea));
            Assert.That(viaje.saldoTarjeta, Is.EqualTo(jubiladoTarjeta.saldo));
        }

    }
}
