using Npgsql;
using System;

public class Conexao
{
    static void Main()
    {
        string connString = "Server=localhost;Port=5433;Database=NewBank;User Id=postgres;Password=123456;";
        NpgsqlConnection PgAdmin = new NpgsqlConnection(connString);
        PgAdmin.Open();
    }
}

