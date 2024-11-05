using System;
using System.Collections.Generic;

namespace TpSube{
    public class Colectivo
    {
        public static int valorPasaje = 940;
        public float saldo_pendiente = 0;

        public Boleto pagarCon(Tarjeta tarjeta)
        {
            if ((tarjeta.saldo) < valorPasaje){
                throw new Exception("Saldo insuficiente");
            } else {
                tarjeta.saldo -= valorPasaje;
                Boleto boleto = new Boleto();
                boleto.costo= valorPasaje;
                return boleto;
            }
        }
    }
}