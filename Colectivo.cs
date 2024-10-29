using System;
using System.Collections.Generic;

namespace TpSube{
    public class Colectivo
    {
        public int valorPasaje = 940;
        public static int max_negativo = 480;
        public Boleto pagarCon(Tarjeta tarjeta)
        {
            if(tarjeta.saldo_pendiente > 0)
            {
                if((tarjeta.saldo + tarjeta.saldo_pendiente) > tarjeta.max_saldo)
                {
                    tarjeta.saldo_pendiente -= (tarjeta.max_saldo - tarjeta.saldo);
                    tarjeta.saldo = tarjeta.max_saldo;
                }
            }
            if(tarjeta is FranquiciaCompleta)            
            {
                if (DateTime.Now.Date != (tarjeta.boletos[boletos.Count - 1].fecha).Date)
                {
                    tarjeta.c_boletos_hoy = 0;
                }
                if (tarjeta.c_boletos_hoy >= 2)
                {
                    if (tarjeta.saldo < (valorPasaje - max_negativo))
                    {
                        throw new Exception("Saldo insuficiente");
                    }
                    else
                    {
                       if (tarjeta.boletos[boletos.Count - 1].saldo_tarjeta < 0 && tarjeta.saldo > 0)
                        {
                            Console.WriteLine("Abona " + tarjeta.boletos[boletos.Count - 1].saldo_tarjeta);
                        }
                        tarjeta.saldo -= valorPasaje;
                        Boleto boleto = new Boleto();
                        boleto.costo = valorPasaje;
                        boleto.linea_de_colectivo = linea;
                        boleto.saldo_tarjeta = tarjeta.saldo;
                        boleto.id_tarjeta = tarjeta.id;
                        tarjeta.boletos.add(boleto);
                        Console.WriteLine('Saldo: ' + tarjeta.saldo);
                        return boleto;
                    }
                }
                else
                {
                    Boleto boleto = new Boleto();
                    boleto.costo= 0;
                    boleto.linea_de_colectivo = linea;
                    boleto.saldo_tarjeta = tarjeta.saldo;
                    boleto.id_tarjeta = tarjeta.id;
                    tarjeta.boletos.add(boleto);
                    tarjeta.c_boletos_hoy++;
                    Console.WriteLine('Saldo: ' + tarjeta.saldo);
                    return boleto;
                }               
            }

            if(tarjeta is MedioBoleto)
            {
                if (DateTime.Now.Date != (tarjeta.boletos[boletos.Count - 1].fecha).Date)
                {
                    tarjeta.c_boletos_hoy = 0;
                }
                if ((DateTime.Now - (tarjeta.boletos[boletos.Count - 1].fecha)).TotalMinutes < 5 || tarjeta.c_boletos_hoy >= 4)
                {
                    if (tarjeta.saldo < (valorPasaje - max_negativo))
                    {
                        throw new Exception("Saldo insuficiente");
                    }
                    else
                    {
                       if (tarjeta.boletos[boletos.Count - 1].saldo_tarjeta < 0 && tarjeta.saldo > 0)
                        {
                            Console.WriteLine("Abona " + tarjeta.boletos[boletos.Count - 1].saldo_tarjeta);
                        }
                        tarjeta.saldo -= valorPasaje;
                        Boleto boleto = new Boleto();
                        boleto.costo = valorPasaje;
                        boleto.linea_de_colectivo = linea;
                        boleto.saldo_tarjeta = tarjeta.saldo;
                        boleto.id_tarjeta = tarjeta.id;
                        tarjeta.boletos.add(boleto);
                        Console.WriteLine('Saldo: ' + tarjeta.saldo);
                        return boleto;
                    }
                }
                
                else if (tarjeta.saldo < ((valorPasaje / 2) - max_negativo))
                {
                    throw new Exception("Saldo insuficiente");
                }
                else
                {
                    if (tarjeta.boletos[boletos.Count - 1].saldo_tarjeta < 0 && tarjeta.saldo > 0)
                        {
                            Console.WriteLine("Abona " + tarjeta.boletos[boletos.Count - 1].saldo_tarjeta);
                        }
                    tarjeta.saldo -= (valorPasaje / 2);
                    Boleto boleto = new Boleto();
                    boleto.costo = (valorPasaje / 2);
                    boleto.linea_de_colectivo = linea;
                    boleto.saldo_tarjeta = tarjeta.saldo;
                    boleto.id_tarjeta = tarjeta.id;
                    tarjeta.boletos.add(boleto);
                    tarjeta.c_boletos_hoy++;
                    Console.WriteLine('Saldo: ' + tarjeta.saldo);
                    return boleto;
                }
            }

            else
            {
                if (tarjeta.saldo < (valorPasaje - max_negativo)){
                    throw new Exception("Saldo insuficiente");
                } else {
                   if (tarjeta.boletos[boletos.Count - 1].saldo_tarjeta < 0 && tarjeta.saldo > 0)
                        {
                            Console.WriteLine("Abona " + tarjeta.boletos[boletos.Count - 1].saldo_tarjeta);
                        }
                    tarjeta.saldo -= valorPasaje;
                    Boleto boleto = new Boleto();
                    boleto.costo= valorPasaje;
                    boleto.linea_de_colectivo = linea;
                    boleto.saldo_tarjeta = tarjeta.saldo;
                    boleto.id_tarjeta = tarjeta.id;
                    tarjeta.boletos.add(boleto);
                    Console.WriteLine('Saldo: ' + tarjeta.saldo);
                    return boleto;
                }
            }
        }
    }
}