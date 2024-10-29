using System;
using System.Collections.Generic;

namespace TpSube{
    public class Tarjeta
    {
        public float saldo;
        public List<int> cargasPosibles = new List<int> { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 }; 

        public float saldo_pendiente = 0;
        public float max_saldo = 36000;
        public List<Boleto> boletos;
        public int c_boletos_hoy = 0;
        public Tarjeta()
        {
            this.saldo = 0;
        }
        public void recargar(int recarga)
        {
            if (cargasPosibles.Contains(recarga) && (saldo + recarga) <= max_saldo){
              saldo += recarga;
            } else if ((saldo + recarga) > max_saldo) {
                saldo = max_saldo;
                saldo_pendiente += ((saldo + recarga) - max_saldo);
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