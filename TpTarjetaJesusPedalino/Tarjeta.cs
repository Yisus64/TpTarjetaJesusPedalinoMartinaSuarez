using System;
using System.Collections.Generic;
using System.Linq;

namespace TpSube{
    public class Tarjeta
    {
        public float saldo;
        public float pendiente;
        public int valorPasaje = 940;
        public static int maxNegativo = 480;
        public static int maxSaldo = 36000;
        public List<int> cargasPosibles = new List<int> { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
        public List<Boleto> boletos = new List<Boleto>();
        public int cantViajesHoy;
        public int ID;
        public Tarjeta(int id)
        {
            this.saldo = 0;
            this.pendiente = 0;
            this.cantViajesHoy = 0;
            this.ID = id;
        }
        public void recargar(int recarga)
        {
            if (cargasPosibles.Contains(recarga) && (saldo + recarga) <= maxSaldo){
              saldo += recarga;
            } else if ((saldo + recarga) > maxSaldo) {
                pendiente = (saldo + recarga) - maxSaldo;
                saldo = maxSaldo;
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

        public Boleto ultimoBoleto() {
            return boletos.Last();
        }

        public void checkPendiente()
        {
            if (pendiente < 0)
            {
                pendiente = 0;
            } else
            {
                pendiente = pendiente;
            }
        }
    }
}