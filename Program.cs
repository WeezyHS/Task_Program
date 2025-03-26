using System;
using MySql.Data.MySqlClient;
using TodoApp.Services;

namespace TodoApp
{
    class Program
    {
        static TaskManager taskManager = new TaskManager();

        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=taskprogram_database;User=root;Password=Wesley@212325";
            ShowMenu(connectionString);
        }

        static void ShowMenu(string connectionString)
        {
            while (true)
            {
                Console.Clear();
                PrintTitle("Task Manager", ConsoleColor.Cyan);
                PrintOptions();

                string option = Console.ReadLine();
                HandleMenuOption(option, connectionString);
            }
        }
        static void PrintTitle(string title, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        static void PrintOptions()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n1 - Add Task");
            Console.WriteLine("2 - List Tasks");
            Console.WriteLine("3 - Update Task");
            Console.WriteLine("4 - Delete Task");
            Console.WriteLine("0 - Exit");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nChoose an Option: ");
            Console.ResetColor();
        }

        static void HandleMenuOption(string option, string connectionString)
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=taskprogram_database;User=root;Password=Wesley@212325");
            connection.Open();
            switch (option)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nEnter the task title: ");
                    Console.ResetColor();

                    string title = Console.ReadLine();
                    taskManager.AddTask(title, "Pending");
                    break;
                case "2":
                    taskManager.ListTasks();
                    Console.ReadKey();
                    break;
                case "3":
                    taskManager.UpdateTask();
                    break;
                case "4":
                    taskManager.DeleteTask(connectionString);
                    Console.ReadKey();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    PrintError("\nInvalid option. Please, try again!");
                    Console.ReadKey();
                    break;
            }
        }
        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}