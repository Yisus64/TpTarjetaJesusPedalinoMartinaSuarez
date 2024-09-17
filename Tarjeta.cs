using System;
using System.Collections.Generic;

namespace TpSube{
    public class Tarjeta
    {
        public int saldo;

        public Tarjeta()
        {
            this.saldo = 0;
        }
        public void recargar(recarga, tarjeta)
        {
            if ((tarjeta.saldo + recarga) < 9900){
              this.saldo += recarga;  
            } else {
                throw new Exception("Excede el limite de saldo");
            }
        }
    }
}