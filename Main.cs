using System;
using System.Collections.Generic;

namespace TpSube{
    public class Program
    {
        public static void Main()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.recargar(2500);
            Colectivo colectivo = new Colectivo();
            Console.WriteLine(tarjeta.saldoActual());
            colectivo.pagarCon(tarjeta);
            Console.WriteLine(tarjeta.saldoActual());
        }
    }
}