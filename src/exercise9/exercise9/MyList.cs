using System;

public class MyList<T>
{
    private T[] _items;
    private int _count;

    public MyList(int initialCapacity = 4)
    {
        if (initialCapacity < 1) initialCapacity = 4;
        _items = new T[initialCapacity];
        _count = 0;
    }

    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            Resize(_items.Length * 2);
        }

        _items[_count++] = item;
    }

    public int Count => _count;

    public void Remove(T item)
    {
        int idx = IndexOf(item);
        if (idx == -1) return;

        for (int i = idx; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        _items[_count - 1] = default!;
        _count--;

        if (_count > 0 && _count <= _items.Length / 4)
        {
            Resize(Math.Max(4, _items.Length / 2));
        }
    }

    private int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < _count; i++)
        {
            if (comparer.Equals(_items[i], item)) return i;
        }
        return -1;
    }

    private void Resize(int newSize)
    {
        var newArr = new T[newSize];
        Array.Copy(_items, 0, newArr, 0, _count);
        _items = newArr;
    }
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count) throw new ArgumentOutOfRangeException(nameof(index));
            return _items[index];
        }
    }
}
