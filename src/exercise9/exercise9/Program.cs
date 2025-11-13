using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            if (TryReadMyList(out MyList<int> myList))
            {
                RunMenu(myList);
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
                break;
            }
        }
    }

    static bool TryReadMyList(out MyList<int> myList)
    {
        myList = new MyList<int>();

        Console.Write("Enter number of elements: ");
        string? countInput = Console.ReadLine();
        if (!int.TryParse(countInput, out int n) || n < 0)
        {
            Console.WriteLine("Error: invalid number.");
            return false;
        }

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter element {i + 1}: ");
            string? elemInput = Console.ReadLine();
            if (!int.TryParse(elemInput, out int elem))
            {
                Console.WriteLine("Error: invalid element.");
                return false;
            }
            myList.Add(elem);
        }

        return true;
    }

    static void RunMenu(MyList<int> myList)
    {
        while (true)
        {
            Console.WriteLine("\nChoose action by number:");
            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Remove");
            Console.WriteLine("3 - Count");
            Console.WriteLine("0 - Exit\n");
            Console.Write("Your choice: ");

            string? choiceInput = Console.ReadLine();
            if (!int.TryParse(choiceInput, out int choice))
            {
                Console.WriteLine("Invalid input. Enter a number.\n");
                continue;
            }

            switch (choice)
            {
                case 1:
                    if (TryReadInt("Enter value to add: ", out int toAdd))
                    {
                        myList.Add(toAdd);
                        Console.Write("\nCurrent list: ");
                        PrintList(myList);
                    }
                    break;

                case 2:
                    if (TryReadInt("Enter value to remove: ", out int toRemove))
                    {
                        myList.Remove(toRemove);
                        Console.Write("\nCurrent list: ");
                        PrintList(myList);
                    }
                    break;

                case 3:
                    Console.WriteLine($"\nNumber of elements in list: {myList.Count}");
                    break;

                case 0:
                    Console.WriteLine("Exit.");
                    return;

                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }
    }

    static bool TryReadInt(string prompt, out int value)
    {
        value = 0;
        Console.Write(prompt);
        string? s = Console.ReadLine();
        if (!int.TryParse(s, out value))
        {
            Console.WriteLine("Error: invalid number.");
            return false;
        }
        return true;
    }

    static void PrintList(MyList<int> myList)
    {
        for (int i = 0; i < myList.Count; i++)
        {
            Console.Write(myList[i]);
            if (i < myList.Count - 1) Console.Write(", ");
        }
        Console.WriteLine();
    }
}
