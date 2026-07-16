using System.Collections.Generic;
using System.Linq;

namespace rocket_bot;

public class Channel<T> where T : class
{
    private readonly List<T> _list = new();

    /// <summary>
    /// Возвращает элемент по индексу или null, если такого элемента нет.
    /// При присвоении удаляет все элементы после.
    /// Если индекс в точности равен размеру коллекции, работает как Append.
    /// </summary>
    public T this[int index]
    {
        get
        {
            lock (_list)
            {
                if (Count <= index) return null;
                T value = _list[index];
                return value;
            }
        }
        set
        {
            lock (_list)
            {
                for (int i = _list.Count - 1; i >= index; i--)
                    _list.Remove(LastItem());
                AppendIfLastItemIsUnchanged(value, LastItem());
            }
        }
    }

    /// <summary>
    /// Возвращает последний элемент или null, если такого элемента нет
    /// </summary>
    public T? LastItem()
    {
        lock (_list)
        {
            if (_list.Count == 0) return null;
            var last = _list[Count - 1];
            if (last == null) return null;
            else return last;
        }
    }

    /// <summary>
    /// Добавляет item в конец только если lastItem является последним элементом
    /// </summary>
    public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
    {
        lock (_list)
        {
            if (_list.Count == 0)
                _list.Add(item);
            else
                if (_list[Count - 1].Equals(knownLastItem))
                    _list.Add(item);
        }
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции
    /// </summary>
    public int Count
    {
        get
        {
            lock (_list)
            {
                return _list.Count;
            }
        }
    }
}