using System;
using System.Collections.Generic;

namespace TpSube{
    public class Colectivo
    {
        public static int valorPasaje = 940;
        public static int valorPasajeMedio = valorPasaje/2;
        public static int valorPasajeCompleto = 0; // se refiere al valor de una franquicia completa
        public string linea;
        public float saldo_pendiente = 0;

        public Colectivo(string lineaDelBondi)
        {
            linea = lineaDelBondi;
        }
        public Boleto pagarCon(Tarjeta tarjeta, Tiempo tiempo)
        {
            if (tarjeta is FranquiciaCompleta)
            {
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
                    return boleto;
                }
                    
            } else if (tarjeta is FranquiciaMedia)
            {
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
                if ((tarjeta.saldo + tarjeta.perdonDivino()) < valorPasaje)
                {
                    throw new Exception("Saldo insuficiente");
                }
                else
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

        public int getValorPasaje()
        {
            return valorPasaje;
        }
        public int getValorPasajeMedio()
        {
            return valorPasajeMedio;
        }
        public int getValorPasajeCompleto()
        {
            return valorPasajeCompleto;
        }
    }
}