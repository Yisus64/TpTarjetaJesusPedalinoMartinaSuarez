using System;
using System.Collections.Generic;

namespace TpSube{
    public class Colectivo
    {
        public static float valorPasaje = 1200;
        public static float valorPasajeMedio = valorPasaje/2;
        public static int valorPasajeCompleto = 0; // se refiere al valor de una franquicia completa
        public string linea;
        public float saldo_pendiente = 0;
        public float tarifaNormal; // se refiere a la tarifa que paga una tarjeta normal dependiendo de cuantos viajes tenga en el mes

        public Colectivo(string lineaDelBondi)
        {
            linea = lineaDelBondi;
        }
        public Boleto pagarCon(Tarjeta tarjeta, Tiempo tiempo)
        {
            if (tarjeta is FranquiciaCompleta)
            {
                if (tiempo.Now().DayOfWeek == DayOfWeek.Saturday || tiempo.Now().DayOfWeek == DayOfWeek.Sunday || tiempo.Now().Hour < 6 || tiempo.Now().Hour > 22)
                {
                    tarjeta.saldo -= valorPasaje;
                    Boleto boleto = new Boleto();
                    boleto.costo = valorPasaje;
                    boleto.fechaUltimoViaje = tiempo.Now();
                    boleto.lineaDeColectivo = linea;
                    boleto.tipoDeTarjeta = "Franquicia Media";
                    boleto.saldoTarjeta = tarjeta.saldo;
                    boleto.idTarjeta = tarjeta.ID;
                    tarjeta.boletos.Add(boleto);
                    tarjeta.cantViajesHoy++;
                    tarjeta.cantViajesMes++;
                    if (tarjeta.pendiente > 0)
                    {
                        if (tarjeta.pendiente >= boleto.costo)
                        {
                            tarjeta.saldo += boleto.costo;
                            tarjeta.pendiente -= boleto.costo;
                            tarjeta.checkPendiente();
                        }
                        else
                        {
                            tarjeta.saldo += tarjeta.pendiente;
                            tarjeta.pendiente = 0;
                        }

                    }
                    return boleto;
                }
                if (tarjeta.boletos.Count != 0)
                {
                    if (tarjeta.ultimoBoleto().fechaUltimoViaje.DayOfYear != tiempo.Now().DayOfYear)
                    {
                        tarjeta.cantViajesHoy = 0;
                    }
                    if(tarjeta.cantViajesHoy < 2)
                    {
                        Boleto boleto = new Boleto();
                        boleto.costo = valorPasajeCompleto;
                        boleto.fechaUltimoViaje = tiempo.Now();
                        boleto.lineaDeColectivo = linea;
                        boleto.tipoDeTarjeta = "Franquicia Completa";
                        boleto.saldoTarjeta = tarjeta.saldo;
                        boleto.idTarjeta = tarjeta.ID;
                        tarjeta.boletos.Add(boleto);
                        tarjeta.cantViajesHoy++;
                        tarjeta.cantViajesMes++;
                        return boleto;
                    } else
                    {
                        tarjeta.saldo -= valorPasaje;
                        Boleto boleto = new Boleto();
                        boleto.costo = valorPasaje;
                        boleto.fechaUltimoViaje = tiempo.Now();
                        boleto.lineaDeColectivo = linea;
                        boleto.tipoDeTarjeta = "Franquicia Completa";
                        boleto.saldoTarjeta = tarjeta.saldo;
                        boleto.idTarjeta = tarjeta.ID;
                        tarjeta.boletos.Add(boleto);
                        tarjeta.cantViajesHoy++;
                        tarjeta.cantViajesMes++;
                        if (tarjeta.pendiente > 0)
                        {
                            if (tarjeta.pendiente >= boleto.costo)
                            {
                                tarjeta.saldo += boleto.costo;
                                tarjeta.pendiente -= boleto.costo;
                                tarjeta.checkPendiente();
                            }
                            else
                            {
                                tarjeta.saldo += tarjeta.pendiente;
                                tarjeta.pendiente = 0;
                            }

                        }
                        return boleto;
                    }
                } else
                {
                    Boleto boleto = new Boleto();
                    boleto.costo = valorPasajeCompleto;
                    boleto.fechaUltimoViaje = tiempo.Now();
                    boleto.lineaDeColectivo = linea;
                    boleto.tipoDeTarjeta = "Franquicia Completa";
                    boleto.saldoTarjeta = tarjeta.saldo;
                    boleto.idTarjeta = tarjeta.ID;
                    tarjeta.boletos.Add(boleto);
                    tarjeta.cantViajesHoy++;
                    tarjeta.cantViajesMes++;
                    return boleto;
                }
                    
            } else if (tarjeta is FranquiciaMedia)
            {
                if (tiempo.Now().DayOfWeek == DayOfWeek.Saturday || tiempo.Now().DayOfWeek == DayOfWeek.Sunday || tiempo.Now().Hour < 6 || tiempo.Now().Hour > 22)
                {
                    tarjeta.saldo -= valorPasaje;
                    Boleto boleto = new Boleto();
                    boleto.costo = valorPasaje;
                    boleto.fechaUltimoViaje = tiempo.Now();
                    boleto.lineaDeColectivo = linea;
                    boleto.tipoDeTarjeta = "Franquicia Media";
                    boleto.saldoTarjeta = tarjeta.saldo;
                    boleto.idTarjeta = tarjeta.ID;
                    tarjeta.boletos.Add(boleto);
                    tarjeta.cantViajesHoy++;
                    tarjeta.cantViajesMes++;
                    if (tarjeta.pendiente > 0)
                    {
                        if (tarjeta.pendiente >= boleto.costo)
                        {
                            tarjeta.saldo += boleto.costo;
                            tarjeta.pendiente -= boleto.costo;
                            tarjeta.checkPendiente();
                        }
                        else
                        {
                            tarjeta.saldo += tarjeta.pendiente;
                            tarjeta.pendiente = 0;
                        }

                    }
                    return boleto;
                }
                if (tarjeta.boletos.Count == 0)
                {
                    tarjeta.saldo -= valorPasajeMedio;
                    Boleto boleto = new Boleto();
                    boleto.costo = valorPasajeMedio;
                    boleto.fechaUltimoViaje = tiempo.Now();
                    boleto.lineaDeColectivo = linea;
                    boleto.tipoDeTarjeta = "Franquicia Media";
                    boleto.saldoTarjeta = tarjeta.saldo;
                    boleto.idTarjeta = tarjeta.ID;
                    tarjeta.boletos.Add(boleto);
                    tarjeta.cantViajesHoy++;
                    tarjeta.cantViajesMes++;
                    if (tarjeta.pendiente > 0)
                    {
                        if (tarjeta.pendiente >= boleto.costo)
                        {
                            tarjeta.saldo += boleto.costo;
                            tarjeta.pendiente -= boleto.costo;
                            tarjeta.checkPendiente();
                        }
                        else
                        {
                            tarjeta.saldo += tarjeta.pendiente;
                            tarjeta.pendiente = 0;
                        }

                    }
                    return boleto;
                }
                else
                {
                    if (tarjeta.ultimoBoleto().fechaUltimoViaje.DayOfYear != tiempo.Now().DayOfYear)
                    {
                        tarjeta.cantViajesHoy = 0;
                    }
                    if (tarjeta.cantViajesHoy < 4 && tarjeta.cantViajesHoy >= 0)
                    {
                        if ((tarjeta.saldo + tarjeta.perdonDivino()) < valorPasajeMedio)
                        {
                            throw new Exception("Saldo insuficiente");
                        }
                        if (tarjeta.ultimoBoleto().fechaUltimoViaje.Hour == tiempo.Now().Hour && tarjeta.ultimoBoleto().fechaUltimoViaje.DayOfYear == tiempo.Now().DayOfYear)
                        {
                            if ((tiempo.Now().Minute - tarjeta.ultimoBoleto().fechaUltimoViaje.Minute) < 5)
                            {
                                throw new Exception("No pasaron 5 minutos");
                            }
                            else //medio 5 min dif
                            {
                                tarjeta.saldo -= valorPasajeMedio;
                                Boleto boleto = new Boleto();
                                boleto.costo = valorPasajeMedio;
                                boleto.fechaUltimoViaje = tiempo.Now();
                                boleto.lineaDeColectivo = linea;
                                boleto.tipoDeTarjeta = "Franquicia Media";
                                boleto.saldoTarjeta = tarjeta.saldo;
                                boleto.idTarjeta = tarjeta.ID;
                                tarjeta.boletos.Add(boleto);
                                tarjeta.cantViajesHoy++;
                                tarjeta.cantViajesMes++;
                                if (tarjeta.pendiente > 0)
                                {
                                    if (tarjeta.pendiente >= boleto.costo)
                                    {
                                        tarjeta.saldo += boleto.costo;
                                        tarjeta.pendiente -= boleto.costo;
                                        tarjeta.checkPendiente();
                                    } else
                                    {
                                        tarjeta.saldo += tarjeta.pendiente;
                                        tarjeta.pendiente = 0;
                                    } 
                                    
                                }
                                return boleto;
                            }
                        }
                        else //medio distinta hrs
                        {
                            tarjeta.saldo -= valorPasajeMedio;
                            Boleto boleto = new Boleto();
                            boleto.costo = valorPasajeMedio;
                            boleto.fechaUltimoViaje = tiempo.Now();
                            boleto.lineaDeColectivo = linea;
                            boleto.tipoDeTarjeta = "Franquicia Media";
                            boleto.saldoTarjeta = tarjeta.saldo;
                            boleto.idTarjeta = tarjeta.ID;
                            tarjeta.boletos.Add(boleto);
                            tarjeta.cantViajesHoy++;
                            tarjeta.cantViajesMes++;
                            if (tarjeta.pendiente > 0)
                            {
                                if (tarjeta.pendiente >= boleto.costo)
                                {
                                    tarjeta.saldo += boleto.costo;
                                    tarjeta.pendiente -= boleto.costo;
                                    tarjeta.checkPendiente();
                                }
                                else
                                {
                                    tarjeta.saldo += tarjeta.pendiente;
                                    tarjeta.pendiente = 0;
                                }

                            }
                            return boleto;
                        }
                    }
                    else //mas de 4 viajes
                    {
                        tarjeta.saldo -= valorPasaje;
                        Boleto boleto = new Boleto();
                        boleto.costo = valorPasaje;
                        boleto.fechaUltimoViaje = tiempo.Now();
                        boleto.lineaDeColectivo = linea;
                        boleto.tipoDeTarjeta = "Franquicia Media";
                        boleto.saldoTarjeta = tarjeta.saldo;
                        boleto.idTarjeta = tarjeta.ID;
                        tarjeta.boletos.Add(boleto);
                        tarjeta.cantViajesHoy++;
                        tarjeta.cantViajesMes++;
                        if (tarjeta.pendiente > 0)
                        {
                            if (tarjeta.pendiente >= boleto.costo)
                            {
                                tarjeta.saldo += boleto.costo;
                                tarjeta.pendiente -= boleto.costo;
                                tarjeta.checkPendiente();
                            }
                            else
                            {
                                tarjeta.saldo += tarjeta.pendiente;
                                tarjeta.pendiente = 0;
                            }

                        }
                        return boleto;
                    }
                  }
              } else //pasaje normal
                {
                if (tarjeta.boletos.Count == 0)
                {
                    tarjeta.saldo -= valorPasaje;
                    Boleto boleto = new Boleto();
                    boleto.costo = valorPasaje;
                    boleto.fechaUltimoViaje = tiempo.Now();
                    boleto.lineaDeColectivo = linea;
                    boleto.tipoDeTarjeta = "Sin Franquicia";
                    boleto.saldoTarjeta = tarjeta.saldo;
                    boleto.idTarjeta = tarjeta.ID;
                    tarjeta.boletos.Add(boleto);
                    tarjeta.cantViajesHoy++;
                    tarjeta.cantViajesMes++;
                    if (tarjeta.pendiente > 0)
                    {
                        if (tarjeta.pendiente >= boleto.costo)
                        {
                            tarjeta.saldo += boleto.costo;
                            tarjeta.pendiente -= boleto.costo;
                            tarjeta.checkPendiente();
                        }
                        else
                        {
                            tarjeta.saldo += tarjeta.pendiente;
                            tarjeta.pendiente = 0;
                        }
                    }
                    return boleto;
                }
                if(tarjeta.ultimoBoleto().fechaUltimoViaje.Month != tiempo.Now().Month)
                {
                    tarjeta.cantViajesMes = 0;
                }

                if (tarjeta.cantViajesMes < 29)
                {
                    tarifaNormal = valorPasaje;
                } else if (tarjeta.cantViajesMes >= 29 && tarjeta.cantViajesMes < 79)
                {
                    tarifaNormal = (float)(valorPasaje * 0.8);
                } else if (tarjeta.cantViajesMes >= 79)
                {
                    tarifaNormal = (float)(valorPasaje * 0.75);
                }


                if ((tarjeta.saldo + tarjeta.perdonDivino()) < tarifaNormal)
                {
                    throw new Exception("Saldo insuficiente");
                }
                else
                {
                    tarjeta.saldo -= tarifaNormal;
                    Boleto boleto = new Boleto();
                    boleto.costo = tarifaNormal;
                    boleto.fechaUltimoViaje = tiempo.Now();
                    boleto.lineaDeColectivo = linea;
                    boleto.tipoDeTarjeta = "Sin Franquicia";
                    boleto.saldoTarjeta = tarjeta.saldo;
                    boleto.idTarjeta = tarjeta.ID;
                    tarjeta.boletos.Add(boleto);
                    tarjeta.cantViajesHoy++;
                    tarjeta.cantViajesMes++;
                    if (tarjeta.pendiente > 0)
                    {
                        if (tarjeta.pendiente >= boleto.costo)
                        {
                            tarjeta.saldo += boleto.costo;
                            tarjeta.pendiente -= boleto.costo;
                            tarjeta.checkPendiente();
                        }
                        else
                        {
                            tarjeta.saldo += tarjeta.pendiente;
                            tarjeta.pendiente = 0;
                        }

                    }
                    return boleto;
                }
            }   
        }

        public float getValorPasaje()
        {
            return valorPasaje;
        }
        public float getValorPasajeMedio()
        {
            return valorPasajeMedio;
        }
        public int getValorPasajeCompleto()
        {
            return valorPasajeCompleto;
        }
    }
}