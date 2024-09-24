using System;
using System.Collections.Generic;

namespace TpSube{
    public class Tarjeta
    {
        public float saldo;

        public Tarjeta()
        {
            this.saldo = 9900;
        }
        public void recargar(int recarga)
        {
            if ((saldo + recarga) < 9900){
              saldo += recarga;  
            } else {
                throw new Exception("Excede el limite de saldo");
            }
        }

        public float saldoActual(){
            return saldo;
        }
    }
}