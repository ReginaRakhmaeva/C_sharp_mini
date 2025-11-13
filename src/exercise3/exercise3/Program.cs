using System;
using System.Globalization;
using System.Linq;

class Program
{
    static void Main()
    {
        while (true)
        {
            if (TryReadMatrixArray(out int[] deck))
            {
                ArrangeCard(deck);
                Console.WriteLine(string.Join(",", deck));
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

    static bool TryReadMatrixArray(out int[] deck)
    {
        Console.Write("Введите числа через запятую: ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            deck = Array.Empty<int>();
            return false;
        }

        string[] parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        deck = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i].Trim(), out deck[i]))
            {
                deck = Array.Empty<int>();
                return false;
            }
        }

        if (deck.Length == 0 || deck.Distinct().Count() != deck.Length)
        {
            deck = Array.Empty<int>();
            return false;
        }

        return true;
    }

    public class CircularQueue<T>
    {
        private T[] _buffer;
        private int _head;
        private int _tail;
        private int _count;

        public CircularQueue(int capacity = 4)
        {
            if (capacity < 1) capacity = 4;
            _buffer = new T[capacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public void Enqueue(T item)
        {
            if (_count == _buffer.Length) Grow();
            _buffer[_tail] = item;
            _tail = (_tail + 1) % _buffer.Length;
            _count++;
        }

        public T Dequeue()
        {
            if (IsEmpty) throw new InvalidOperationException("Queue is empty.");
            T value = _buffer[_head]!;
            _buffer[_head] = default!;
            _head = (_head + 1) % _buffer.Length;
            _count--;
            return value;
        }

        public T[] ToArray()
        {
            T[] arr = new T[_count];
            if (_count == 0) return arr;
            if (_head < _tail)
            {
                Array.Copy(_buffer, _head, arr, 0, _count);
            }
            else
            {
                int firstLen = _buffer.Length - _head;
                Array.Copy(_buffer, _head, arr, 0, firstLen);
                Array.Copy(_buffer, 0, arr, firstLen, _tail);
            }
            return arr;
        }

        private void Grow()
        {
            int newCapacity = _buffer.Length * 2;
            T[] newBuffer = new T[newCapacity];
            if (_count > 0)
            {
                if (_head < _tail)
                {
                    Array.Copy(_buffer, _head, newBuffer, 0, _count);
                }
                else
                {
                    int firstLen = _buffer.Length - _head;
                    Array.Copy(_buffer, _head, newBuffer, 0, firstLen);
                    Array.Copy(_buffer, 0, newBuffer, firstLen, _tail);
                }
            }
            _buffer = newBuffer;
            _head = 0;
            _tail = _count % _buffer.Length;
        }
    }

    static void ArrangeCard(int[] deck)
    {
        int n = deck.Length;
        if (n == 0) return;

        Array.Sort(deck);

        var q = new CircularQueue<int>(Math.Max(4, n));

        for (int idx = deck.Length - 1; idx >= 0; idx--)
        {
            if (q.Count > 0)
            {
                int rotateTimes = q.Count - 1;
                for (int r = 0; r < rotateTimes; r++)
                {
                    q.Enqueue(q.Dequeue());
                }

                int last = q.Dequeue();

                q.Enqueue(last);
                for (int r = 0; r < rotateTimes; r++)
                {
                    q.Enqueue(q.Dequeue());
                }
            }

            int prevCount = q.Count;
            q.Enqueue(deck[idx]); 
            for (int r = 0; r < prevCount; r++)
            {
                q.Enqueue(q.Dequeue());
            }
        }

        int[] result = q.ToArray();

        for (int i = 0; i < n; i++)
        {
            deck[i] = result[i];
        }
    }
}
