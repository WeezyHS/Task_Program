using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TaskManager
    {
        private string connectionString = "Server=localhost;Database=taskprogram_database;User=root;Password=Wesley@212325";
        public void AddTask(string title, string IsCompleted)
        {
            Random random = new Random();
            int id;
            bool idExists;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                do{
                    id = random.Next(0, 100); //Gera os IDs entre 0 à 99
                    string checkQuery = "SELECT COUNT(*) FROM tasks WHERE Id = @Id";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Id", id);
                        idExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                    }
                } while (idExists);

                string query = "INSERT INTO tasks (Id, Title, IsCompleted) VALUES (@Id, @Title, @IsCompleted)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@IsCompleted", IsCompleted);
                    command.ExecuteNonQuery();
                    ResultMessage("\nTask added successfully!");
                }
            }
        }
        public void ListTasks()
        {
            ResultMessage("\n====Task List====");
            Console.WriteLine();
            string query = "SELECT * FROM tasks";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //ESTRUTURA (COLUNAS) DO BANCO DE DADOS, COM OS TIPOS DE DADOS PRESENTES NELE
                                int id = reader.GetInt32("Id");
                                string title = reader.GetString("Title");
                                string IsCompleted = reader.GetString("IsCompleted");
                                ResultMessage($"{id}: {title} - {IsCompleted}");
                            }
                        }
                        else
                        {
                            MessageError("\nThe list is empty");
                        }
                    }
                }
            }
        }
        public void UpdateTask()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n===== Update task =====");
            ListTasks();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nEnter the ID of the task you want to update: ");
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                string query = "SELECT * FROM tasks WHERE Id = @Id";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nWhat do you want to update?");
                                Console.WriteLine("\n1 - Title");
                                Console.WriteLine("2 - Mark as completed/Pending");
                                Console.WriteLine("3 - Return");
                                Console.Write("\nChoose an option: ");
                                Console.ResetColor();

                                string option = Console.ReadLine();
                                switch (option)
                                {
                                    case "1":
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nEnter new title: ");
                                    Console.ResetColor();
                                    string newTitle = Console.ReadLine();

                                    reader.Close(); //FECHA O "DataReader" ANTES DE EXECUTAR A ATUALIZAÇÃO DAS "tasks"

                                    string updateTitleQuery = "UPDATE tasks SET Title = @Title WHERE Id = @Id";
                                    using (MySqlCommand updateTitleCommand = new MySqlCommand(updateTitleQuery, connection))
                                    {
                                        updateTitleCommand.Parameters.AddWithValue("@Title", newTitle);
                                        updateTitleCommand.Parameters.AddWithValue("@Id", id);
                                        updateTitleCommand.ExecuteNonQuery();
                                        ResultMessage("\nTask updated successfully!");
                                    }
                                        break;
                                    case "2":
                                        String currentIsCompleted = reader.GetString("IsCompleted");
                                        bool currentIsCompletedBool = currentIsCompleted.Equals("Completed", StringComparison.OrdinalIgnoreCase);
                                        reader.Close(); //FECHA O "DataReader" ANTES DE EXECUTAR A ATUALIZAÇÃO DAS "tasks"

                                        string updateStatusQuery = "UPDATE tasks SET IsCompleted = @IsCompleted WHERE Id = @Id";
                                        using (MySqlCommand updateStatusCommand = new MySqlCommand(updateStatusQuery, connection))
                                        {
                                            updateStatusCommand.Parameters.AddWithValue("@IsCompleted", currentIsCompletedBool ? "Pending" : "Completed");
                                            updateStatusCommand.Parameters.AddWithValue("@Id", id);
                                            updateStatusCommand.ExecuteNonQuery();
                                            ResultMessage($"\nStatus updated for: {(!currentIsCompletedBool ? "Completed" : "Pending")}");
                                        }
                                        break;
                                    case "3":
                                        reader.Close();
                                        return;
                                    default:
                                        MessageError("\nInvalid option!");
                                        break;
                                    }
                                }
                                else
                                {
                                    MessageError("\nTask not found!");
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageError("Invalid ID!");
            }
        }
        public void DeleteTask(string connectionString)
        {
            try
            {
                ListTasks();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n1 - To delete a task: ");
                Console.WriteLine("2 - To delete all tasks");
                Console.Write("\nChoose an option: ");
                Console.ResetColor();

                string option = Console.ReadLine();
                Console.ResetColor();

                switch(option)
                {
                    case "1":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nEnter the ID for task you want to delete: ");
                        Console.ResetColor();
                        if (int.TryParse(Console.ReadLine(), out int taskId))
                        {
                            string query1 = "DELETE FROM tasks WHERE Id = @Id";
                                using (MySqlConnection connection = new MySqlConnection(connectionString))
                                {
                                    using (MySqlCommand command = new MySqlCommand(query1, connection))
                                    {

                                    connection.Open();
                                    command.Parameters.AddWithValue("@Id", taskId);
                                    int rowsAffected = command.ExecuteNonQuery();
                                    if (rowsAffected > 0) //Verifica se pelo menos uma linha foi excluída com sucesso
                                    {
                                        ResultMessage("\nTask deleted successfully!");
                                    }
                                    else
                                    {
                                        MessageError("\nTask not found!");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageError("\nInvalid ID format!");
                        }
                        break;
                    case "2":
                        string query2 = "DELETE FROM tasks";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            using (MySqlCommand command = new MySqlCommand(query2, connection))
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                                ResultMessage("\nAll tasks deleted successfully!");
                            }
                        }
                        break;
                    default:
                        MessageError("\nInvalid option!");
                        break;
                    }
            }
            catch (MySqlException ex)
            {
                MessageError("\nError deleting task: " + ex.Message);
            }
        }

        static void ResultMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        static void MessageError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}