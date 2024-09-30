using System;
using System.Collections.Generic;

namespace TpSube
{
    public class MedioBoleto : Tarjeta
    {
        public override Boleto pagar()
        {
            if (saldo < ((valorPasaje / 2) - max_negativo))
            {
                throw new Exception("Saldo insuficiente");
            }
            else
            {
                saldo -= (valorPasaje / 2);
                Boleto boleto = new Boleto();
                boleto.costo = (valorPasaje / 2);
                return boleto;
            }
        }
    }
}