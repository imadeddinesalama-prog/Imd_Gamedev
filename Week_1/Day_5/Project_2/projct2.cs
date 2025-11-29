using System;

namespace PersonalExpenseTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpenseManager manager = new ExpenseManager();
            bool running = true;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("║  PERSONAL EXPENSE TRACKER CONSOLE APP     ║");
            Console.ResetColor();

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTransactionMenu(manager);
                        break;
                    case "2":
                        manager.ViewAllTransactions();
                        break;
                    case "3":
                        UpdateTransactionMenu(manager);
                        break;
                    case "4":
                        DeleteTransactionMenu(manager);
                        break;
                    case "5":
                        manager.ViewSummary();
                        break;
                    case "6":
                        manager.SaveTransactions();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✓ Transactions saved successfully!");
                        Console.ResetColor();
                        break;
                    case "7":
                        FilterMenu(manager);
                        break;
                    case "8":
                        SortMenu(manager);
                        break;
                    case "9":
                        manager.ExportToCSV();
                        break;
                    case "10":
                        manager.ExportSummaryReport();
                        break;
                    case "0":
                        running = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThank you for using Expense Tracker. Goodbye!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n" + new string('─', 50));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("MAIN MENU");
            Console.ResetColor();
            Console.WriteLine(new string('─', 50));
            Console.WriteLine("1.  Add Transaction");
            Console.WriteLine("2.  View All Transactions");
            Console.WriteLine("3.  Update Transaction");
            Console.WriteLine("4.  Delete Transaction");
            Console.WriteLine("5.  View Summary / Analysis");
            Console.WriteLine("6.  Save Transactions");
            Console.WriteLine("7.  Filter Transactions");
            Console.WriteLine("8.  Sort Transactions");
            Console.WriteLine("9.  Export to CSV");
            Console.WriteLine("10. Export Summary Report");
            Console.WriteLine("0.  Exit");
            Console.WriteLine(new string('─', 50));
            Console.Write("Enter your choice: ");
        }

        static void AddTransactionMenu(ExpenseManager manager)
        {
            Console.WriteLine("\n--- ADD TRANSACTION ---");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Amount (positive for income, negative for expense): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid amount. Transaction cancelled.");
                Console.ResetColor();
                return;
            }

            Console.Write("Category: ");
            string category = Console.ReadLine();

            Console.Write("Date (yyyy-mm-dd) [Leave blank for today]: ");
            string dateStr = Console.ReadLine();
            DateTime date = string.IsNullOrWhiteSpace(dateStr) ? DateTime.Now : DateTime.Parse(dateStr);

            manager.AddTransaction(title, amount, category, date);
        }

        static void UpdateTransactionMenu(ExpenseManager manager)
        {
            Console.WriteLine("\n--- UPDATE TRANSACTION ---");
            Console.Write("Enter transaction ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                manager.UpdateTransaction(id);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID.");
                Console.ResetColor();
            }
        }

        static void DeleteTransactionMenu(ExpenseManager manager)
        {
            Console.WriteLine("\n--- DELETE TRANSACTION ---");
            Console.Write("Enter transaction ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                manager.DeleteTransaction(id);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID.");
                Console.ResetColor();
            }
        }

        static void FilterMenu(ExpenseManager manager)
        {
            Console.WriteLine("\n--- FILTER TRANSACTIONS ---");
            Console.WriteLine("1. Filter by Date Range");
            Console.WriteLine("2. Filter by Category");
            Console.Write("Choose filter type: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Start date (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                    {
                        Console.Write("End date (yyyy-mm-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                        {
                            manager.FilterByDateRange(startDate, endDate);
                        }
                    }
                    break;
                case "2":
                    Console.Write("Category: ");
                    string category = Console.ReadLine();
                    manager.FilterByCategory(category);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        static void SortMenu(ExpenseManager manager)
        {
            Console.WriteLine("\n--- SORT TRANSACTIONS ---");
            Console.WriteLine("1. Sort by Amount");
            Console.WriteLine("2. Sort by Date");
            Console.Write("Choose sort option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.SortTransactions("amount");
                    break;
                case "2":
                    manager.SortTransactions("date");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}