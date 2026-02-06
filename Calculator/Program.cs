using System.Text.RegularExpressions;
using CalculatorLibrary;
using Menu;

class Program
{
    static void Main(string[] args)
    {
        // Declare variables to run the app
        bool endApp = false;
        int cntUsing = 0;
        double ans = 0;
        List<string> history = new List<string>();

        // Display title as the C# console calculator app
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            cntUsing++;
            int menuChoice = Menu.Menu.ShowMenu();

            switch (menuChoice)
            {
                case 1: // New calculation
                        // Declare variables and set to empty
                        // Use Nullable types (with ?) wo match type of System.Console.ReadLine
                    string? numInput1 = "";
                    string? numInput2 = "";
                    double result = 0;

                    // Ask the user to type the first number
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    double cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        if (numInput1.ToLower().Equals("ans"))
                        {
                            cleanNum1 = ans;
                            break;
                        }
                        Console.Write("This is not a valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }

                    // Ask the user to choose an operator
                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.WriteLine("\tsq - Square Root");
                    Console.WriteLine("\tp - Power");
                    Console.WriteLine("\te - 10x");
                    Console.WriteLine("\tt - Trigonmetric functions");
                    Console.Write("Your option? ");

                    string? op = Console.ReadLine();

                    // Validate input is not null, and matches the pattern
                    if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sq|p|e|t]"))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                    }
                    else
                    {
                        double cleanNum2 = 0;
                        int functionChoice = 0;
                        if (op.Equals("t"))
                        {
                            Console.WriteLine("Type a number, then press Enter to choose: ");
                            Console.WriteLine("\t1. Sine");
                            Console.WriteLine("\t2. Cosine");
                            Console.WriteLine("\t3. Tangent");
                            Console.WriteLine("\t4. Cotangent");
                            string? functionInput = Console.ReadLine();
                            while (!int.TryParse(functionInput, out functionChoice) || functionChoice < 1 || functionChoice > 4)
                            {
                                Console.Write("This is not a valid input. Please enter a number from 1 to 3: ");
                                functionInput = Console.ReadLine();
                            }
                        }
                        else if (!op.Equals("sq") && !op.Equals("e"))
                        {
                            // Ask the user to type the second number
                            Console.Write("Type another number, and then press Enter: ");
                            numInput2 = Console.ReadLine();

                            while (!double.TryParse(numInput2, out cleanNum2))
                            {
                                if (numInput2.ToLower().Equals("ans"))
                                {
                                    cleanNum2 = ans;
                                    break;
                                }
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numInput2 = Console.ReadLine();
                            }
                        }
                        try
                        {
                            result = calculator.DoOperation(cleanNum1, op, cleanNum2, functionChoice);
                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                                history.Add(calculator.SetHistoryString(cleanNum1, op, cleanNum2, functionChoice, result));
                                ans = result;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                    Console.WriteLine("------------------------\n");
                    break;
                case 2: // Show history
                    Console.WriteLine("Calculation history:");
                    foreach(string s in history)
                        Console.WriteLine(s);
                    Console.WriteLine();
                    break;
                case 3: // Delete history
                    history.Clear();
                    Console.WriteLine("Calculation history deleted.");
                    break;
                default:
                    break;
            }

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
        }
        Console.WriteLine($"You have used the calculator {cntUsing} times.");
        Console.WriteLine("\n"); // Friendly linespacing.
        calculator.Finish();
        return;
    }
}