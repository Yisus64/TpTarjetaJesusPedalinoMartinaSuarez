using System;
using System.Collections.Generic;

namespace TpSube{
    public class Colectivo
    {
        public static int valorPasaje = 940;
        public static int valorPasajeMedio = valorPasaje/2;
        public static int valorPasajeCompleto = 0; // se refiere al valor de una franquicia completa
        public string linea;
        public float saldo_pendiente = 0;

        public Colectivo(string lineaDelBondi)
        {
            linea = lineaDelBondi;
        }
        public Boleto pagarCon(Tarjeta tarjeta)
        {
            if (tarjeta is FranquiciaCompleta)
            {
                Boleto boleto = new Boleto();
                boleto.costo = valorPasajeCompleto;
                boleto.fechaUltimoViaje = DateTime.Now;
                boleto.lineaDeColectivo = linea;
                boleto.tipoDeTarjeta = "Franquicia Completa";
                boleto.saldoTarjeta = tarjeta.saldo;
                boleto.idTarjeta = tarjeta.ID;
                tarjeta.boletos.Add(boleto);
                return boleto;
            } else if (tarjeta is FranquiciaMedia)
            {
                if ((tarjeta.saldo + tarjeta.perdonDivino()) < valorPasajeMedio)
                {
                    throw new Exception("Saldo insuficiente");
                }
                else
                {
                    tarjeta.saldo -= valorPasajeMedio;
                    Boleto boleto = new Boleto();
                    boleto.costo = valorPasajeMedio;
                    boleto.fechaUltimoViaje = DateTime.Now;
                    boleto.lineaDeColectivo = linea;
                    boleto.tipoDeTarjeta = "Franquicia Media";
                    boleto.saldoTarjeta = tarjeta.saldo;
                    boleto.idTarjeta = tarjeta.ID;
                    tarjeta.boletos.Add(boleto);
                    return boleto;
                }
            } else
            {
                if ((tarjeta.saldo + tarjeta.perdonDivino()) < valorPasaje)
                {
                    throw new Exception("Saldo insuficiente");
                }
                else
                {
                    tarjeta.saldo -= valorPasaje;
                    Boleto boleto = new Boleto();
                    boleto.costo = valorPasaje;
                    boleto.fechaUltimoViaje = DateTime.Now;
                    boleto.lineaDeColectivo = linea;
                    boleto.tipoDeTarjeta = "Sin Franquicia";
                    boleto.saldoTarjeta = tarjeta.saldo;
                    boleto.idTarjeta = tarjeta.ID;
                    tarjeta.boletos.Add(boleto);
                    return boleto;
                }
            }   
        }

        public int getValorPasaje()
        {
            return valorPasaje;
        }
        public int getValorPasajeMedio()
        {
            return valorPasajeMedio;
        }
        public int getValorPasajeCompleto()
        {
            return valorPasajeCompleto;
        }
    }
}