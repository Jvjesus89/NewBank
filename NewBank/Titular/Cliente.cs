using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Newbank.Titular
{
    public class Cliente
    {
        private string nome;
        public string Nome { 
         get{ return nome ;}
         set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z]+$"))
                {
                    nome = value;
                }
                else
                {
                    Console.WriteLine("A entrada deve conter apenas letras.");
                    throw new ArgumentException("A entrada deve conter apenas letras.");
                }
            }
        }
        private string cpf;
            public string Cpf
            {
                get { return this.cpf; }
                set { this.cpf = value; }
            }

        public string Profissao { get; set; }
    }
}
