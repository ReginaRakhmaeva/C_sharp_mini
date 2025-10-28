using System.Globalization;
using System.IO;
class Program
{
    static void Main()
    {
        Console.Write("Enter the path to the file: ");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Input error. File isn't exist");
            return;
        }

        int[,] matrix = ReadMatrixFromFile(filePath);
        if (matrix == null) return;

        double[] solution = SolveGaussian(matrix);

        if (solution != null)
        {
            for (int i = 0; i < solution.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {solution[i]} ");
            }
        }
        else
        {
            Console.WriteLine("The system of linear algebraic equations has no solutions");
        } 
    }
    static int[,] ReadMatrixFromFile(string filePath)
    {
        string[] numbers_in_line = File.ReadAllLines(filePath);
        int rows = numbers_in_line.Length;

        string[] firstLine = numbers_in_line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int cols = firstLine.Length;

        int[,] matrix = new int[rows, cols];

        for (int i = 0; i < rows; i++) {
            string[] elements = numbers_in_line[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < cols; j++) {
                if (!int.TryParse(elements[j], NumberStyles.Any, CultureInfo.InvariantCulture, out int value))
                {
                    Console.WriteLine("Couldn't parse a number. Please, try again");
                }
                else matrix[i, j] = value;
            }
        }

        return matrix;
    }
    static double[] SolveGaussian(int[,] matrix) {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);
        double[,] matrix_result = new double[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                matrix_result[i, j] = matrix[i, j];
            }
        }
        for (int i = 0; i < n; i++) {
            int maxRow = i;
            for (int k = i + 1; k < n; k++)
            {
                if (Math.Abs(matrix_result[k, i]) > Math.Abs(matrix_result[maxRow, i]))
                {
                    maxRow = k;
                }
            }
            if (maxRow != i)
            {
                for (int j = 0; j < m;j++)
                {
                    double temp = matrix_result[i, j];
                    matrix_result[i, j] = matrix_result[maxRow, j];
                    matrix_result[maxRow, j] = temp;
                }
            }
            if (Math.Abs(matrix_result[i, i]) < 1e-10)
            {
                return null; 
            }
            for (int k = i + 1; k < n; k++)
            {
                double factor = matrix_result[k, i] / matrix_result[i, i];
                for (int j = i; j < m; j++)
                {
                    matrix_result[k, j] -= factor * matrix_result[i, j];
                }
            }
        }
        double[] solution = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            solution[i] = matrix_result[i, m - 1];
            for (int j = i + 1; j < n; j++)
            {
                solution[i] -= matrix_result[i, j] * solution[j];
            }
            solution[i] /= matrix_result[i, i];
        }

        return solution;

    }
}