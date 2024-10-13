using System;
using System.Collections.Generic;

namespace TpSube{
    public class Tarjeta
    {
        public float saldo = 0;
        public static int id;
        public List<int> cargasPosibles = new List<int> { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
        public void recargar(int recarga)
        {
            if (cargasPosibles.Contains(recarga) && (saldo + recarga) <= 9900){
              saldo += recarga;
            } else if ((saldo + recarga) > 9900) {
                throw new Exception("Excede el limite de saldo");
            }
            else {
                throw new Exception("Monto de carga inválido");
            }
        }
        public float saldoActual(){
            return saldo;
        }
    }
}