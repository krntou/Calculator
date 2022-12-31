using System;
using System.Diagnostics.Metrics;
using System.Globalization;


namespace Calculator
{
    class Calculator
    {
        public static double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            string newLogEntry;
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    newLogEntry = $"{num1} + {num2} = {result}\n";
                    OperationHistory.Add(newLogEntry);
                    break;
                case "s":
                    result = num1 - num2;
                    newLogEntry = $"{num1} - {num2} = {result}\n";
                    OperationHistory.Add(newLogEntry);
                    break;
                case "m":
                    result = num1 * num2;
                    newLogEntry = $"{num1} x {num2} = {result}\n";
                    OperationHistory.Add(newLogEntry);
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    newLogEntry = $"{num1} / {num2} = {result}\n";
                    OperationHistory.Add(newLogEntry);
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            return result;
        }

        public static List<String> OperationHistory { get; private set; } = new List<string>();

        public static void ClearHistory()
        {
            OperationHistory.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            int timesUsed = 0; // Init var to keep track of times used
            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                string op = Console.ReadLine();

                try
                {
                    result = Calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                timesUsed += 1;
                Console.WriteLine("Operation log:");
                foreach (var entry in Calculator.OperationHistory)
                {
                    Console.WriteLine(entry);
                }
                Console.WriteLine($"Number of calculations performed: {timesUsed}");
                Console.Write("Type 'delete' to reset the operation log, or press any other key to keep it: ");
                if (Console.ReadLine() == "delete")
                {
                    Calculator.ClearHistory();
                    Console.WriteLine("Operation log cleared.");
                }
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            return;
        }
    }
}