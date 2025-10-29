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

            if (TryReadMatrixFromFile(path, out int[,] SystemLinearEquations))
            {
                double[] solution = SolveGaussian(SystemLinearEquations);

                if (solution != null)
                {
                    for (int i = 0; i < solution.Length; i++)
                    {
                        Console.WriteLine($"x{i + 1} = {solution[i]:0.###} ");
                    }
                }
                else
                {
                    Console.WriteLine("The system of linear algebraic equations has no solutions");
                } 
                break;
            }
            else
            {
                Console.WriteLine("Couldn't parse a number. Please, try again");
            }
        }

       
    }
    static bool TryReadMatrixFromFile(string filePath, out int[,] SystemLinearEquations)
    {
        string[] numbersLine = File.ReadAllLines(filePath);
        int rows = numbersLine.Length;

        string[] firstLine = numbersLine[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int cols = firstLine.Length;

        SystemLinearEquations = new int[rows, cols];

        for (int i = 0; i < rows; i++) {
            string[] elements = numbersLine[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < cols; j++) {
                if (!int.TryParse(elements[j], out int value))
                {
                    return false;
                }
                else SystemLinearEquations[i, j] = value;
            }
        }

        return true;

    }

    static double[] SolveGaussian(int[,] SystemLinearEquations) {
        int numberRows = SystemLinearEquations.GetLength(0);
        int numberColumns = SystemLinearEquations.GetLength(1);
        double[,] matrixResult = new double[numberRows, numberColumns];
        
        for (int i = 0; i < numberRows; i++)
        {
            for (int j = 0; j < numberColumns; j++)
            {
                matrixResult[i, j] = SystemLinearEquations[i, j];
            }
        }
        for (int i = 0; i < numberRows; i++)
        {
            int maxRow = i;
            for (int k = i + 1; k < numberRows; k++)
            {
                if (Math.Abs(matrixResult[k, i]) > Math.Abs(matrixResult[maxRow, i]))
                {
                    maxRow = k;
                }
            }
            if (maxRow != i)
            {
                for (int j = 0; j < numberColumns; j++)
                {
                    (matrixResult[i, j], matrixResult[maxRow, j]) = (matrixResult[maxRow, j], matrixResult[i, j]);
                }
            }
            if (Math.Abs(matrixResult[i, i]) < 1e-10)
            {
                return null;
            }
            for (int k = i + 1; k < numberRows; k++)
            {
                double factor = matrixResult[k, i] / matrixResult[i, i];
                for (int j = i; j < numberColumns; j++)
                {
                    matrixResult[k, j] -= factor * matrixResult[i, j];
                }
            }
        }
        
        double[] solution = new double[numberRows];
        for (int i = numberRows - 1; i >= 0; i--)
        {
            solution[i] = matrixResult[i, numberColumns - 1];
            for (int j = i + 1; j < numberRows; j++)
            {
                solution[i] -= matrixResult[i, j] * solution[j];
            }
            solution[i] /= matrixResult[i, i];
        }
        return solution;
    }
}