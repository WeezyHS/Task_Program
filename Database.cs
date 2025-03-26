using System;
using MySql.Data.MySqlClient;

namespace TodoApp
{
    public class Database
    {
        private MySqlConnection connection;

        public Database()
        {
            string connectionString = "Server=localhost;Database=taskprogram_database;User=root;Password=Wesley@212325";
            connection = new MySqlConnection(connectionString);
        }
        public void TestConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conectado ao banco de dados com sucesso!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro ao conectar: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}