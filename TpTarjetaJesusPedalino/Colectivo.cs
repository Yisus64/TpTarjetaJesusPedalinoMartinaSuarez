using System;
using System.Collections.Generic;

namespace TpSube{
    public class Colectivo
    {
        public static int valorPasaje = 940;
        public static int valorPasajeMedio = valorPasaje/2;
        public static int valorPasajeCompleto = 0;
        public float saldo_pendiente = 0;

        public Boleto pagarCon(Tarjeta tarjeta)
        {
            if (tarjeta is FranquiciaCompleta)
            {
                Boleto boleto = new Boleto();
                boleto.costo = valorPasajeCompleto;
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