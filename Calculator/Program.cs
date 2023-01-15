using System;
using System.Diagnostics.Metrics;
using System.Globalization;
//if you ever come to look at this again, you need to check if the fix the use previous results feature so it works on single ops too works

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
                case "p":
                    result = Math.Pow(num1, num2);
                    newLogEntry = $"{num1} ^ {num2} = {result}\n";
                    OperationHistory.Add(newLogEntry);
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            return result;
        }
        public static double DoOperationSingle(double num1, string op)
        {
            double result = double.NaN;
            string newLogEntry;
            switch (op)
            {
                case "r":
                    result = Math.Sqrt(num1);
                    newLogEntry = $"Root of {num1} = {result}\n";
                    OperationHistory.Add(newLogEntry);
                    break;
                case "mt":
                    result = num1 * 10;
                    newLogEntry = $"{num1} * 10 = {result}";
                    OperationHistory.Add(newLogEntry);
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    newLogEntry = $"sin {num1} = {result}";
                    OperationHistory.Add(newLogEntry);
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    newLogEntry = $"cos {num1} = {result}";
                    OperationHistory.Add(newLogEntry);
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    newLogEntry = $"tan {num1} = {result}";
                    OperationHistory.Add(newLogEntry);
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
                double cleanNum1 = 0;
                string numInput2 = "";
                double result = 0;
                bool usePrevResult = false;

                // Ask the user if they want to use a previous result as the first number.
                if (Calculator.OperationHistory.Count > 0)
                {
                    Console.Write("Press 'y' to use a previous result as the first number, or else press any key to type a new number: ");
                    if (Console.ReadLine() == "y")
                    {
                        usePrevResult = true;
                        List<Double> pastResults = new List<Double>();
                        int countLoop = 1;
                        foreach (var entry in Calculator.OperationHistory)
                        {
                            String[] splitEntry = entry.Split(" ");
                            string pastResult = splitEntry.ElementAt(splitEntry.Count() - 1);
                            Console.WriteLine($"{countLoop}: {pastResult}");
                            double pastResultDBL = double.Parse(pastResult);
                            pastResults.Add(pastResultDBL);
                            countLoop++;
                        }
                        Console.Write("Choose a result to use as the first number (type the number before the colon): ");
                        int chosenIndex = Convert.ToInt32(Console.ReadLine());
                        cleanNum1 = pastResults.ElementAt(chosenIndex - 1);
                        Console.WriteLine($"The first number set to: {cleanNum1}");

                    }
                    else // fix this else so that asking for the first number is not repeated
                    {
                        usePrevResult = false;
                        // Ask the user to type the first number.
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        cleanNum1 = 0;
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput1 = Console.ReadLine();
                        }
                    }
                }
                // Ask the user if they want to perform an operation with one number 
                Console.WriteLine("\tr - Square root of");
                Console.WriteLine("\tmt - Multiply by 10");
                Console.WriteLine("\tsin - Sin");
                Console.WriteLine("\tcos - Cos");
                Console.WriteLine("\ttan - Tan");
                Console.WriteLine("Press 'y' to perform any of the single number operations above.");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Choose an operation from the above list: ");
                    string op = Console.ReadLine();
                    if (usePrevResult == true)
                    {
                        try
                        {
                            result = Calculator.DoOperationSingle(cleanNum1, op);
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
                    }
                    else
                    {
                        // Ask the user to type the first number.
                        Console.Write("Type a number to perform the operation on, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        cleanNum1 = 0;
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput1 = Console.ReadLine();
                        }
                        try
                        {
                            result = Calculator.DoOperationSingle(cleanNum1, op);
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
                    }
                }
                else
                {
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    cleanNum1 = 0;
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
                    Console.WriteLine("\tp - Power of");
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