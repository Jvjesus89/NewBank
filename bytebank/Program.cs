using bytebank.Contas;
using bytebank.Titular;
using System.Reflection.Metadata.Ecma335;

int opcao;

Cliente ClienteBanco = new Cliente();
Console.WriteLine("Digite o seu nome");
ClienteBanco.Nome = Console.ReadLine();
Console.Clear();
ClienteBanco.Cpf = "161381";
ClienteBanco.Profissao = "Suporte";

ContaCorrente novaContaCorrente = new ContaCorrente("1021-X", 12, ClienteBanco);

do
{
    Console.WriteLine("Seja Bem Vindo ao ByteBank Sr(a) "+ ClienteBanco.Nome);
    Console.WriteLine("Selecione a opção que desejar");
    Console.WriteLine("1 - Consultar o saldo");
    Console.WriteLine("2 - Realizar deposito");
    Console.WriteLine("3 - Realizar um saque");
    Console.WriteLine("4 - Realizar uma transferencia");
    Console.WriteLine("5 - Visualizar o total de contas cadastradas");
    Console.WriteLine("0 - Selecione para sair do sistema");
    opcao = Convert.ToInt32(Console.ReadLine());   
    switch(opcao){
        case 0:
            { }
            break;
        case 1:
            {
                Console.WriteLine("Olá " + ClienteBanco.Nome + " seu saldo é R$" + novaContaCorrente.Saldo);
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        case 2:
            {
                Console.WriteLine("Qual o valor que deseja depositar ?");
                novaContaCorrente.Depositar(Convert.ToDouble(Console.ReadLine()));
                Console.WriteLine("Deposito realizado com sucesso, seu saldo atual é R$" + novaContaCorrente.Saldo);
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        case 3:
            {
                Console.WriteLine("Qual o valor que desjea sacar ?");
                if (novaContaCorrente.Sacar(Convert.ToDouble(Console.ReadLine())) == false)
                {
                    Console.WriteLine("Saldo insuficiente");
                }
                else
                {
                    Console.WriteLine("Saque realizado com sucesso, seu saldo atual é R$" + novaContaCorrente.Saldo);
                }
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        case 4:
            {
                string nrconta;
                int nragencia;
                string titularconta;

                Console.WriteLine("Digita o numero da conta que deseja realizar o deposito");
                nrconta = Console.ReadLine();
                Console.WriteLine("digite o numero da agencia");
                nragencia = Convert.ToInt16(Console.ReadLine());
               
                
                Console.WriteLine("Digite o nome do titular");
                titularconta = Console.ReadLine();

                Cliente clienteTransferencia = new Cliente();
                titularconta = clienteTransferencia.Nome;

                ContaCorrente transferenciaContaCorrente = new(nrconta, nragencia, clienteTransferencia);
                

                Console.WriteLine("Qual o valor que deseja transferir ?");
                if (novaContaCorrente.Transferir(Convert.ToDouble(Console.ReadLine()),transferenciaContaCorrente) == true)
                {
                    Console.WriteLine("Transferencia realizada com sucesso, saldo atual é R$" + novaContaCorrente.Saldo);
                    Console.WriteLine("Saldo da conta destino é R$" + transferenciaContaCorrente.Saldo);
                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                }
                break;
            }
        case 5:
            Console.WriteLine(ContaCorrente.TotalDeContasCriadas);
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            break;
    }
} while (opcao != 0);
Console.WriteLine("Sistema será finalizado"); 