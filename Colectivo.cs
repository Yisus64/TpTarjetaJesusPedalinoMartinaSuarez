using System;
using System.Collections.Generic;

namespace TpSube{
    public class colectivo
    {
        public Boleto pagarCon(tarjeta)
        {
            if (tarjeta.saldo < Boleto.costo){
                throw new Exception("Saldo insuficiente");
            } else {
                tarjeta.saldo -= valorPasaje;
                return new Boleto;
            }
        }
    }
}