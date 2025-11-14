using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("Enter path to input file: ");
            string path = Console.ReadLine() ?? string.Empty;

            if (!File.Exists(path))
            {
                Console.WriteLine("Input error. File isn't exist");
                continue;
            }

            if (TryReadInputFromFile(path, out string[] lines, out string word))
            {
                int countWords = CountingNumberWords(lines, word);
                PrintInConsol(lines, countWords, word);
                PrintInFile(countWords);
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
                break;
            }
            
        }


    }
    static bool TryReadInputFromFile(string filePath, out string[] lines, out string word)
    {
        string[] alllines = File.ReadAllLines(filePath);
        string firstLine = alllines[0];
        word = null;
        if (!int.TryParse(firstLine, out int linesCount))
        {
            Console.WriteLine("Input error. First line is not a number.");
            lines = new string[0];
            return false;
        }

        if (linesCount <= 0)
        {
            Console.WriteLine("Input error. linesCount <= 0");
            lines = new string[0];
            return false;
        }

        int numberLinesFile = alllines.Length;
        if (linesCount  > numberLinesFile - 2)
        {
            Console.WriteLine("Input error. Insufficient number of elements");
            lines = new string[0];
            return false;
        }

        lines = new string[linesCount];
        for (int i = 0; i < linesCount; i++)
        {
            lines[i] = alllines[i + 1];
        }
        word = alllines[numberLinesFile - 1];
        return true;

    }
    static int CountingNumberWords(string[] lines, string word) {
        int total = 0;

        for (int i = 0; i < lines.Length; i++)  
        {
            string line = lines[i];
            string current = ""; 

            for (int j = 0; j < line.Length; j++)  
            {
                char c = line[j];

                if (char.IsLetter(c)) current += c; 
                else
                {
                    if (current == word) total++;
                    current = ""; 
                }
            }

            if (current == word) total++;
        }
        return total;
    }
    static void PrintInConsol(string[] lines, int countWords, string word) {
        foreach (string line in lines) { 
            Console.WriteLine(line);
        }
        Console.WriteLine(word);
        Console.Write(countWords);
    }
    static void PrintInFile(int countWords)
    {
        File.WriteAllText("result.txt", $"{countWords}");
    }
}