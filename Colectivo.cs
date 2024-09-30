using System;
using System.Collections.Generic;

namespace TpSube{
    public class Colectivo
    {
        public void pagarCon(Tarjeta tarjeta)
        {
            tarjeta.pagar();
        }
    }
}