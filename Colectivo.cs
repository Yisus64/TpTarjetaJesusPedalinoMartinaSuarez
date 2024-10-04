using System;
using System.Collections.Generic;

namespace TpSube{
    public class Colectivo
    {
        public int valorPasaje = 940;
        public static int max_negativo = 480;
        public Boleto pagarCon(Tarjeta tarjeta)
        {
            if(tarjeta is FranquiciaCompleta)
            {            
                Boleto boleto = new Boleto();
                boleto.costo= 0;
                return boleto;
            }

            if(tarjeta is MedioBoleto)
            {
                if (tarjeta.saldo < ((valorPasaje / 2) - max_negativo))
                {
                    throw new Exception("Saldo insuficiente");
                }
                else
                {
                    tarjeta.saldo -= (valorPasaje / 2);
                    Boleto boleto = new Boleto();
                    boleto.costo = (valorPasaje / 2);
                    return boleto;
                }
            }

            else
            {
                if (tarjeta.saldo < (valorPasaje - max_negativo)){
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
}