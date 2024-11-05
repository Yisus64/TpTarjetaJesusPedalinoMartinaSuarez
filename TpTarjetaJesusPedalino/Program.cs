using System;
using System.Collections.Generic;

namespace TpSube
{
    public class Program
    {
        // Ejemplo de prueba
        public static void Main()
        {
            Tarjeta tarjeta = new Tarjeta(6);
            tarjeta.recargar(5000);
            Colectivo colectivo = new Colectivo();
            Console.WriteLine(tarjeta.saldoActual());
            colectivo.pagarCon(tarjeta);
            Console.WriteLine(tarjeta.saldoActual());
        }
    }
}