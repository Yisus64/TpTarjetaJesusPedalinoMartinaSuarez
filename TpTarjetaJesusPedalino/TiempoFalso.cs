using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpSube
{
    public class TiempoFalso : Tiempo
    {
        private DateTime tiempo;

        public TiempoFalso()
        {
            //CON VOS ES 4 DE NOVIEMBRE CADA MEDIA HORA ATRASARE LAS HORAS HORAS HORAS 4/11/2024(Lunes) 00:00:00
            tiempo = new DateTime(2024, 11, 4);
        }

        public override DateTime Now()
        {
            return tiempo;
        }

        public void AgregarDias(int cantidad)
        {
            tiempo = tiempo.AddDays(cantidad);
        }

        public void AgregarMinutos(int cantidad)
        {
            tiempo = tiempo.AddMinutes(cantidad);
        }
    }
}
