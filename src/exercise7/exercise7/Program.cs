using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        while (true)
        {
            
            if (TryReadProductPrices(out Dictionary<string, double> products))
            {
                double avg = CalculateAverage(products);
                Console.WriteLine($"{avg:0.###}");
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("Couldn't parse a words. Please, try again");
            }
        }
    }

    static bool TryReadProductPrices(out Dictionary<string, double> products)
    {
        products = new Dictionary<string, double>();
        Console.Write("Enter number of products: ");

        string? countInput = Console.ReadLine();
        if (!int.TryParse(countInput, out int n) || n <= 0)
        {
            Console.WriteLine("Couldn't parse a number. Please, try again");
            return false;
        }

        for (int i = 0; i < n; i++)
        {
            Console.Write("Enter product name and price: ");
            string? line = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine("Couldn't parse a words. Please, try again");
                i--;
                continue;
            }

            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                Console.WriteLine("Couldn't parse a words. Please, try again");
                i--;
                continue;
            }

            string name = parts[0];
            if (!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double price))
            {
                Console.WriteLine("Couldn't parse a words. Please, try again");
                i--;
                continue;
            }

            if (price <= 0)
            {
                Console.WriteLine("Incorrect input. price <= 0");
                continue;
            }

            products[name] = price;
        }

        return products.Count > 0;
    }

    static double CalculateAverage(Dictionary<string, double> products)
    {
        double sum = 0;
        foreach (var p in products.Values)
            sum += p;

        return sum / products.Count;
    }
}
