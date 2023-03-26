using Newbank.Contas;
using Newbank.Titular;
using Npgsql;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;
// conexão
    string connString = "Server=localhost;Port=5433;Database=NewBank;User Id=postgres;Password=123456;";
    NpgsqlConnection PgAdmin = new NpgsqlConnection(connString);
    PgAdmin.Open();

int opcao;
//Criando um cliente
Cliente ClienteBanco = new Cliente();
Console.WriteLine("Digite o seu nome");
ClienteBanco.Nome = Console.ReadLine();
Console.Clear();

NpgsqlCommand ConsultarSaldoAtual = new NpgsqlCommand(
    "Select saldoatual From movimentobancario " +
    "where idcontacorrente = 1 ORDER BY idmovimentobancario DESC LIMIT 1", PgAdmin);
NpgsqlDataReader r_saldo = ConsultarSaldoAtual.ExecuteReader();
while (r_saldo.Read())
{
    double saldoatual = (double)r_saldo.GetInt64(0);


    ContaCorrente novaContaCorrente = new ContaCorrente("1021-X", 12, saldoatual, ClienteBanco);
    PgAdmin.Close();
    do
    {
        Console.WriteLine("Seja Bem Vindo ao ByteBank Sr(a) " + ClienteBanco.Nome);
        Console.WriteLine("Selecione a opção que desejar");
        Console.WriteLine("1 - Consultar o saldo");
        Console.WriteLine("2 - Realizar deposito");
        Console.WriteLine("3 - Realizar um saque");
        Console.WriteLine("4 - Realizar uma transferencia");
        Console.WriteLine("5 - Visualizar o total de contas cadastradas");
        Console.WriteLine("6 - Consultar numero da conta e agencia");
        Console.WriteLine("0 - Selecione para sair do sistema");
        opcao = Convert.ToInt32(Console.ReadLine());
        switch (opcao)
        {
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
                    double valormovimento;
                    Console.WriteLine("Qual o valor que deseja depositar ?");
                    valormovimento = Convert.ToDouble(Console.ReadLine());
                    novaContaCorrente.Depositar(valormovimento);
                    Console.WriteLine("Deposito realizado com sucesso, seu saldo atual é R$" + novaContaCorrente.Saldo);
                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();

         
                    PgAdmin.Open();
                    // insert na tabela movimento bancario 
                    NpgsqlCommand InserirDeposito = new NpgsqlCommand(
                        "INSERT INTO public.movimentobancario" +
                        "(idcontacorrente, saldoatual, valormovimento,tpmovimento)" +
                        "VALUES ( @idcontacorrente, @saldoatual, @valormovimento,@tpmovimento)", PgAdmin);
                    InserirDeposito.Parameters.AddWithValue("idcontacorrente", 1);
                    InserirDeposito.Parameters.AddWithValue("saldoatual", novaContaCorrente.Saldo);
                    InserirDeposito.Parameters.AddWithValue("valormovimento", valormovimento);
                    InserirDeposito.Parameters.AddWithValue("tpmovimento", "Entrada");
                    NpgsqlDataReader execute = InserirDeposito.ExecuteReader();
                    PgAdmin.Close();
                    break;
                }
            case 3:
                {
                    Console.WriteLine("Qual o valor que desjea sacar ?");
                    double valorsaque = Convert.ToDouble(Console.ReadLine());
                    if (novaContaCorrente.Sacar(valorsaque) == false)
                    {
                        Console.WriteLine("Saldo insuficiente");
                    }
                    else
                    {
                        Console.WriteLine("Saque realizado com sucesso, seu saldo atual é R$" + novaContaCorrente.Saldo);
                        PgAdmin.Open();
                        // insert na tabela movimento bancario 
                        NpgsqlCommand InserirDeposito = new NpgsqlCommand(
                            "INSERT INTO public.movimentobancario" +
                            "(idcontacorrente, saldoatual, valormovimento,tpmovimento)" +
                            "VALUES ( @idcontacorrente, @saldoatual, @valormovimento,@tpmovimento)", PgAdmin);
                        InserirDeposito.Parameters.AddWithValue("idcontacorrente", 1);
                        InserirDeposito.Parameters.AddWithValue("saldoatual", novaContaCorrente.Saldo);
                        InserirDeposito.Parameters.AddWithValue("valormovimento", valorsaque);
                        InserirDeposito.Parameters.AddWithValue("tpmovimento", "Saida");
                        NpgsqlDataReader execute = InserirDeposito.ExecuteReader();
                        PgAdmin.Close();
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

                    ContaCorrente transferenciaContaCorrente = new(nrconta, nragencia, saldoatual, clienteTransferencia);


                    Console.WriteLine("Qual o valor que deseja transferir ?");
                    if (novaContaCorrente.Transferir(Convert.ToDouble(Console.ReadLine()), transferenciaContaCorrente) == true)
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
            case 6:
                {
                    PgAdmin.Open();
                    NpgsqlCommand ConsultarConta = new NpgsqlCommand(
                        "SELECT nrcontacorrente,nragencia,C.nmcliente " +
                        "FROM public.contacorrente CC " +
                        "join cliente C on (CC.idcliente = C.idcliente) where idcontacorrente = 1;", PgAdmin);
                    NpgsqlDataReader reader = ConsultarConta.ExecuteReader();
                    while (reader.Read())
                    {
                        int nrconta = (int)reader.GetInt64(0);
                        int nragencia = (int)reader.GetInt64(1);
                        string nome = reader.GetString(2);
                        Console.WriteLine($"Titular: {nome}Nr_conta: {nrconta}   Nr_agencia{nragencia}");
                    }
                    PgAdmin.Close();
                    reader.Close();
                    break;
                }
        }
    } while (opcao != 0);
    Console.WriteLine("Sistema será finalizado");

}