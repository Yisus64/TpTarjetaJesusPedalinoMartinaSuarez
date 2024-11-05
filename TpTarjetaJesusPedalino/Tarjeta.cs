using System;
using System.Collections.Generic;

namespace TpSube{
    public class Tarjeta
    {
        public float saldo;
        public int valorPasaje = 940;
        public static int maxNegativo = 480;
        public static int maxSaldo = 9900;
        public List<int> cargasPosibles = new List<int> { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
        public int ID;
        public Tarjeta(int id)
        {
            this.saldo = 0;
            this.ID = id;
        }
        public void recargar(int recarga)
        {
            if (cargasPosibles.Contains(recarga) && (saldo + recarga) <= 9900){
              saldo += recarga;
            } else if ((saldo + recarga) > 9900) {
                throw new Exception("Excede el limite de saldo");
            }
            else {
                throw new Exception("Monto de carga invalido");
            }
        }

        public float saldoActual(){
            return saldo;
        }

        public float perdonDivino()
        {
            return maxNegativo;
        }
    }
}