using System;
using System.Collections.Generic;

namespace TpSube
{
    public class ColectivoInterurbano : Colectivo
    {
        public ColectivoInterurbano(string lineaDelBondi) : base(lineaDelBondi)
        {
            valorPasaje = 2500;
            valorPasajeMedio = valorPasaje / 2;
            valorPasajeCompleto = 0;
            linea = lineaDelBondi;
        }
    }
}