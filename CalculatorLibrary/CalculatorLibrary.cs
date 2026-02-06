using Newtonsoft.Json;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public string SetHistoryString(double num1, string op, double num2, int function, double result)
        {
            switch (op)
            {
                case "a":
                    return $"{num1} + {num2} = {result}";
                case "s":
                    return $"{num1} - {num2} = {result}";
                case "m":
                    return $"{num1} * {num2} = {result}";
                case "d":
                    return $"{num1} / {num2} = {result}";
                case "sq":
                    return $"Sqrt({num1}) = {result}";
                case "p":
                    return $"{num1} ^ {num2} = {result}";
                case "e":
                    return $"10 ^ {num1} = {result}";
                case "t":
                    switch (function)
                    {
                        case 1:
                            return $"Sin({num1})= {result}";
                        case 2:
                            return $"Cos({num1})= {result}";
                        case 3:
                            return $"Tan({num1})= {result}";
                        case 4:
                            return $"Cot({num1})= {result}";
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            // Default return
            return "";
        }

        public double DoOperation(double num1, string op, double num2 = 0, int function = 0)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                    }
                    break;
                case "sq":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "e":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10x");
                    break;
                case "t":
                    switch (function)
                    {
                        case 1:
                            result = MathF.Sin((float)num1);
                            writer.WriteValue("Sine");
                            break;
                        case 2:
                            result = MathF.Cos((float)num1);
                            writer.WriteValue("Cosine");
                            break;
                        case 3:
                            result = MathF.Tan((float)num1);
                            writer.WriteValue("Tangent");
                            break;
                        case 4:
                            result = 1/MathF.Tan((float)num1);
                            writer.WriteValue("Cotangent");
                            break;
                        default:
                            break;
                    }
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
    }
}