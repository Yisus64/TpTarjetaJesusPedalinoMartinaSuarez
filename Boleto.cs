using System;
using System.Collections.Generic;

namespace TpSube{
    public class Boleto
    {
        public int costo;
        public DateTime fecha = DateTime.Now;
        public int linea_de_colectivo;
        public int saldo_tarjeta;
        public int id_tarjeta;
    }
}