using Newbank.Titular;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbank.Contas
{
    public class ContaCorrente
    {
        public static int TotalDeContasCriadas { get; private set; }
       
        public string Conta { get; set; }
        public int numeroAgencia { get; set; }

        private double saldo;
        public Cliente Titular { get; set; }


        public double Saldo
        {
            get { return this.saldo; }
            set { if (value > 0)
                {

                    saldo = value;
                }
                else { return; }
            }
        }

        public void Depositar(double valor)
        {
            Saldo += valor;
        }

        public bool Sacar (double valor)
        {
            if(saldo > valor)
            {
                saldo -= valor;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Transferir (double valor, ContaCorrente destino)
        {
            if (saldo > valor)
            {
                Sacar(valor);
                destino.Depositar(valor);
                return true;
            }
            else
            {
                Console.WriteLine("Saldo insuficiente");
                return false;
            }
        }
        // metodo construtor
        public ContaCorrente(string conta, int numeroAgencia,double saldo, Cliente Titular)
        {

            this.Conta = conta;
            this.numeroAgencia = numeroAgencia;
            this.Titular = Titular;
            this.saldo = saldo;
            TotalDeContasCriadas++;
        }
    }
}
