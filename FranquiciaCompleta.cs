using System;
using System.Collections.Generic;

namespace TpSube{
    public class FranquiciaCompleta : Tarjeta{
        public override Boleto pagar()
        {            
                Boleto boleto = new Boleto();
                boleto.costo= 0;
                return boleto;
        }
    }
}