using System;
using System.Collections.Generic;

namespace TpSube{
    public class Boleto
    {
        public string tipo; //el string es temporal
        public int costo;

        public Boleto() {
            this.tipo = "Normal";
            this.costo = 940;
        }
    }
}