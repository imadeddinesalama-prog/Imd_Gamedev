using System;
using System.Collections.Generic;

Dictionary<string, object> ATM = new Dictionary<string, object>()
{
    { "account number", 5321588458845444 },
    { "PIN", 545 },
    { "balance", 56000000 }
};

List<string> transactionHistory = new List<string>();

Main();

void Main()
{
    Console.WriteLine("Welcome to GameDev Bank ATM!");
    Console.WriteLine("Please enter your account number: ");
    if (!long.TryParse(Console.ReadLine(), out long accountNumber))
    {
        Console.WriteLine("Invalid account number.");
        return; 
    }

    Console.WriteLine("Please enter your PIN: ");
    if (!int.TryParse(Console.ReadLine(), out int pin))
    {
        Console.WriteLine("Invalid PIN.");
        return; 
    }

    if (accountNumber == (long)ATM["account number"] && pin == (int)ATM["PIN"])
    {
        Console.WriteLine("Authentication successful!");

        while (true)
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("if you want to check your balance, type '1'");
            Console.WriteLine("if you want to withdraw money, type '2'");
            Console.WriteLine("if you want to deposit money, type '3'");
            Console.WriteLine("View transaction history, type '4'");
            Console.WriteLine("To exit and end the session, type '5'");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number from 1 to 5.");
                continue; 
            }

            Console.WriteLine("-----------------");

            switch (choice)
            {
                case 1: 
                    Console.WriteLine($"Your balance is: ${(int)ATM["balance"]}");
                    break;

                case 2: 
                    Console.WriteLine("Enter amount to withdraw: ");
                    if (!int.TryParse(Console.ReadLine(), out int withdrawAmount))
                    {
                        Console.WriteLine("Invalid amount format.");
                    }
                    else if (withdrawAmount <= 0)
                    {
                        Console.WriteLine("Withdrawal amount must be greater than zero.");
                    }
                    else if (withdrawAmount <= (int)ATM["balance"])
                    {
                        ATM["balance"] = (int)ATM["balance"] - withdrawAmount;
                        string entry = $"Withdrawal: -{withdrawAmount} | New balance: {(int)ATM["balance"]} | {DateTime.Now}";
                        transactionHistory.Add(entry);
                        Console.WriteLine($"Please take your cash. New balance is: ${(int)ATM["balance"]}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance!");
                    }
                    break;

                case 3: 
                    Console.WriteLine("Enter amount to deposit: ");
                    if (!int.TryParse(Console.ReadLine(), out int depositAmount))
                    {
                        Console.WriteLine("Invalid amount format.");
                    }
                    else if (depositAmount <= 0)
                    {
                        Console.WriteLine("Deposit amount must be greater than zero.");
                    }
                    else
                    {
                        ATM["balance"] = (int)ATM["balance"] + depositAmount;
                        string depositEntry = $"Deposit: +{depositAmount} | New balance: {(int)ATM["balance"]} | {DateTime.Now}";
                        transactionHistory.Add(depositEntry);
                        Console.WriteLine($"Deposit successful! New balance is: ${(int)ATM["balance"]}");
                    }
                    break;

                case 4: 
                    if (transactionHistory.Count == 0)
                    {
                        Console.WriteLine("No history found.");
                    }
                    else
                    {
                        Console.WriteLine("Transaction History:");
                        foreach (var e in transactionHistory)
                            Console.WriteLine($"- {e}");
                    }
                    break;

                case 5: 
                    Console.WriteLine("Thank you for using GameDev Bank ATM! Goodbye.");
                    return; 

                default:
                    Console.WriteLine("Unknown option. Please choose a valid number (1-5).");
                    break;
            }
        }
    }
    else
    {
        Console.WriteLine("Authentication failed! Please check your account number and PIN.");
        return; 
    }
}
