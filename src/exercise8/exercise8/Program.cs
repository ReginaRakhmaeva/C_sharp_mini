using exercise8;
using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        while (true)
        {
            if (TryReadStudent(out List<Student> students, out int groupSearch))
            {
                PrintStudentSameGroup(students, groupSearch);
                break;
            }
        }
    }

    static bool TryReadStudent(out List<Student> students, out int groupSearch)
    {
        students = new List<Student>();
        groupSearch = -2;
        Console.Write("Enter number of students: ");

        string? countInput = Console.ReadLine();
        if (!int.TryParse(countInput, out int n) || n <= 0)
        {
            InputHelpers.PrintError(InputHelpers.ErrParseNumber);
            return false;
        }

        for (int i = 0; i < n; i++)
        {
            Console.Write("Enter student name and group: ");
            string? line = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
            {
                InputHelpers.PrintError(InputHelpers.ErrParseWords);
                continue;
            }

            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                InputHelpers.PrintError(InputHelpers.ErrParseWords);
                continue;
            }

            string name = parts[0];
            if (!int.TryParse(parts[1], out int group))
            {
                InputHelpers.PrintError(InputHelpers.ErrParseWords);
                continue;
            }

            if (group <= 0)
            {
                InputHelpers.PrintError(InputHelpers.ErrGroupNonPositive);
                continue;
            }

            students.Add(new Student(name, group));
        }

        Console.Write("Enter the number of the student group you want to find: ");
        string? groupInput = Console.ReadLine();

        if (!int.TryParse(groupInput, out int groupInputForSearch) || n <= 0)
        {
            InputHelpers.PrintError(InputHelpers.ErrParseNumber);
            return false;
        }

        groupSearch = groupInputForSearch;

        return students.Count > 0;
    }

    static void PrintStudentSameGroup(List<Student> students, int groupSearch)
    {
        var sameGroup = students
        .Where(s => s.group == groupSearch)
        .Select(s => s.name)
        .ToList();

        if (sameGroup.Count == 0)
            Console.WriteLine("There are no students from such a group");
        else
            Console.WriteLine(string.Join(", ", sameGroup));
    }
}
static class InputHelpers
{
    public const string ErrParseNumber = "Couldn't parse a number. Please, try again";
    public const string ErrParseWords = "Couldn't parse words. Please, try again";
    public const string ErrGroupNonPositive = "Incorrect input. Group <= 0";
    public static void PrintError(string message)
    {
        Console.WriteLine(message);
    }
}