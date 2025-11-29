using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace TaskManager
{
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public Task(string title, string description)
        {
            Title = title;
            Description = description;
            Status = "Pending";
        }
        public Task()
        {
            Title = "";
            Description = "";
            Status = "Pending";
        }

        public override string ToString()
        {
            return $"Title: {Title}\nDescription: {Description}\nStatus: {Status}";
        }
    }

    class Program
    {
        private static List<Task> tasks = new List<Task>();
        private static string dataFile = "tasks.json";

        static void Main(string[] args)
        {
            LoadTasks();

            bool running = true;

            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ViewTasks();
                        break;
                    case "3":
                        UpdateTaskStatus();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        SearchTasks();
                        break;
                    case "6":
                        running = false;
                        SaveTasks();
                        Console.WriteLine("\nThank you for using Task Manager. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("Welcome to Your Task Manager!");
            Console.WriteLine("=================================\n");
            Console.WriteLine("1. Add a Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Update Task Status");
            Console.WriteLine("4. Delete a Task");
            Console.WriteLine("5. Search Tasks ");
            Console.WriteLine("6. Exit\n");
            Console.Write("Choose an option: ");
        }

        static void AddTask()
        {
            Console.Clear();
            Console.WriteLine("=== Add a New Task ===\n");

            Console.Write("Enter task title: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("\nError: Title cannot be empty.");
                return;
            }

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("\nError: Description cannot be empty.");
                return;
            }

            Task newTask = new Task(title, description);
            tasks.Add(newTask);

            Console.WriteLine("\n✓ Task added successfully!");
            SaveTasks();
        }

        static void ViewTasks()
        {
            Console.Clear();
            Console.WriteLine("=== All Tasks ===\n");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found. Add a task to get started!");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Task #{i + 1}");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine(tasks[i].ToString());
                Console.WriteLine(new string('-', 40));
                Console.WriteLine();
            }
        }

        static void UpdateTaskStatus()
        {
            Console.Clear();
            Console.WriteLine("=== Update Task Status ===\n");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available to update.");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].Title} - [{tasks[i].Status}]");
            }

            Console.Write("\nEnter task number to update: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
            {
                Task selectedTask = tasks[taskNumber - 1];
                Console.WriteLine($"\nCurrent status: {selectedTask.Status}");
                Console.WriteLine("\n1. Pending");
                Console.WriteLine("2. Completed");
                Console.Write("\nChoose new status: ");

                string statusChoice = Console.ReadLine();

                switch (statusChoice)
                {
                    case "1":
                        selectedTask.Status = "Pending";
                        Console.WriteLine("\n✓ Status updated to Pending!");
                        SaveTasks();
                        break;
                    case "2":
                        selectedTask.Status = "Completed";
                        Console.WriteLine("\n✓ Status updated to Completed!");
                        SaveTasks();
                        break;
                    default:
                        Console.WriteLine("\nInvalid status choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nInvalid task number.");
            }
        }

        static void DeleteTask()
        {
            Console.Clear();
            Console.WriteLine("=== Delete a Task ===\n");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available to delete.");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].Title}");
            }

            Console.Write("\nEnter task number to delete: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
            {
                Task selectedTask = tasks[taskNumber - 1];
                Console.WriteLine($"\nAre you sure you want to delete '{selectedTask.Title}'?");
                Console.Write("Type 'yes' to confirm: ");

                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "yes")
                {
                    tasks.RemoveAt(taskNumber - 1);
                    Console.WriteLine("\n✓ Task deleted successfully!");
                    SaveTasks();
                }
                else
                {
                    Console.WriteLine("\nDeletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid task number.");
            }
        }

        static void SearchTasks()
        {
            Console.Clear();
            Console.WriteLine("=== Search Tasks ===\n");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available to search.");
                return;
            }

            Console.Write("Enter search keyword (title or description): ");
            string keyword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("\nSearch keyword cannot be empty.");
                return;
            }

            var results = tasks.Where(t =>
                t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine($"\nNo tasks found matching '{keyword}'.");
                return;
            }

            Console.WriteLine($"\nFound {results.Count} task(s):\n");

            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"Result #{i + 1}");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine(results[i].ToString());
                Console.WriteLine(new string('-', 40));
                Console.WriteLine();
            }
        }

        static void SaveTasks()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(dataFile, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError saving tasks: {ex.Message}");
            }
        }

        static void LoadTasks()
        {
            try
            {
                if (File.Exists(dataFile))
                {
                    string jsonString = File.ReadAllText(dataFile);
                    tasks = JsonSerializer.Deserialize<List<Task>>(jsonString) ?? new List<Task>();
                    Console.WriteLine($"Loaded {tasks.Count} task(s) from file.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError loading tasks: {ex.Message}");
                tasks = new List<Task>();
            }
        }
    }
}