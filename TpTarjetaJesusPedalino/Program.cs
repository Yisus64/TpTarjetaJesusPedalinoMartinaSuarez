using System;
using System.Collections.Generic;

namespace TpSube
{
    public class Program
    {
        // Ejemplo de prueba
        public static void Main()
        {
            TiempoFalso tiempo = new TiempoFalso();
            Console.WriteLine(tiempo.Now().DayOfWeek);
            tiempo.AgregarDias(5);
            Console.WriteLine(tiempo.Now().DayOfWeek);
            tiempo.AgregarDias(1);
            Console.WriteLine(tiempo.Now().DayOfWeek);
        }
    }
}