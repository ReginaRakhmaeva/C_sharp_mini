using System;
using System.Linq;
using System.Text;
class Program
{
    static void Main()
    {
        while (true)
        {
            if (TryReadList(out ListNode? head))
            {
                ChangeOrderList(head);
                PrintList(head);
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("Couldn't parse a number. Please, try again");
            }
        }
    }

    static bool TryReadList(out ListNode? head)
    {
        Console.Write("Введите числа через запятую: ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            head = null;
            return false;
        }

        string[] parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        ListNode? dummy = new(); 
        ListNode current = dummy;

        foreach (string part in parts)
        {
            if (int.TryParse(part.Trim(), out int value))
            {
                current.next = new ListNode(value);
                current = current.next;
            }
            else
            {
                head = null;
                return false;
            }
        }

        head = dummy.next; 
        return head != null;
    }

    static void ChangeOrderList(ListNode head)
    {
        if ( head.next == null) return;
        
        Stack<ListNode> stack = new();
        ListNode? cur = head;
        while (cur != null)
        {
            stack.Push(cur);
            cur = cur.next;
        }

        ListNode left = head;
        while (stack.Count > 0)
        {
            ListNode right = stack.Pop();

            if (left == right)
            {
                left.next = null;
                break;
            }

            if (left.next == right)
            {
                right.next = null;
                break;
            }

            ListNode? tmp = left.next;
            left.next = right;
            right.next = tmp;

            left = tmp!;
        }
    }
    static void PrintList(ListNode? head)
    {
        var sb = new System.Text.StringBuilder();
        ListNode? cur = head;
        while (cur != null)
        {
            if (sb.Length > 0) sb.Append(", ");
            sb.Append(cur.val);
            cur = cur.next;
        }

        Console.WriteLine(sb.ToString());
    }
}

public class ListNode
{
    public int val;
    public ListNode? next;
    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }
}
