using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bytebank.Titular
{
    public class Cliente
    {
        private string nome;
        public string Nome { 
         get{ return nome ;}
         set 
            { 
                Regex somenteletra = new Regex(@"[a-zA-Z]+.[a-zA-Z]+");
                bool resposta = somenteletra.IsMatch(value);
                if (resposta == true)
                {
                    this.nome = value;
                }
                else
                {
                    Console.WriteLine("Digite apenas letras");
                    return;
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
