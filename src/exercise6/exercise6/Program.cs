
using System;
using System.Linq;
using System.Text;
class Program
{
    static void Main()
    {
        while (true)
        {
            if (TryReadInput(out int[] parent, out string s))
            {
                Tree tree = new Tree(parent, s);
                var (len, path) = tree.FindLongestPath();
                Console.WriteLine($"{len}\n{string.Join(", ", path)}");
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

    static bool TryReadInput(out int[] parent, out string s)
    {
        parent = Array.Empty<int>();
        s = "";

        try
        {
            string parentLine = Console.ReadLine();
            s = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(parentLine) || string.IsNullOrWhiteSpace(s))
                return false;

            int[] parsed = parentLine
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.TryParse(x.Trim(), out var v) ? v : int.MinValue)
                .ToArray();

            if (parsed.Length == 0 ||
                parsed.Any(x => x < -1) ||
                s.Length != parsed.Length ||
                parsed[0] != -1)
                return false;

            parent = parsed;
            return true;
        }
        catch
        {
            return false;
        }
    }
}
struct Node
{
    public int Id;
    public char Value;
    public List<int> Children;

    public Node(int id, char value)
    {
        Id = id;
        Value = value;
        Children = new List<int>();
    }
}
class Tree
{
    public Node[] Nodes;
    public int Root;

    public Tree(int[] parent, string s)
    {
        int n = parent.Length;
        Nodes = new Node[n];
        for (int i = 0; i < n; i++)
        {
            Nodes[i] = new Node(i, s[i]);
        }
        Root = 0;
        for (int i = 1; i < n; i++)
        {
            if (parent[i] >= 0 && parent[i] < n)
            {
                Nodes[parent[i]].Children.Add(i);
            }
        }
    }

    public (int length, List<int> path) FindLongestPath()
    {
        List<int> bestPath = new List<int>();

        List<int> Dfs(int node, int parent)
        {
            List<int> best1 = new List<int>();
            List<int> best2 = new List<int>();

            foreach (int child in Nodes[node].Children)
            {
                var down = Dfs(child, node);
                if (Nodes[child].Value == Nodes[node].Value) continue;

                if (down.Count > best1.Count)
                {
                    best2 = best1;
                    best1 = down;
                }
                else if (down.Count > best2.Count)
                {
                    best2 = down;
                }
            }

            var candidate = new List<int>();
            candidate.AddRange(best1);
            candidate.Add(node);
            candidate.AddRange(best2.AsEnumerable().Reverse());

            if (candidate.Count > bestPath.Count)
                bestPath = candidate;

            var result = new List<int> { node };
            result.AddRange(best1);
            return result;
        }

        Dfs(Root, -1);
        return (bestPath.Count, bestPath);
    }
}