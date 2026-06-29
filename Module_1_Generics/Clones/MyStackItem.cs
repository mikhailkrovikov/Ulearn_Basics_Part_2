namespace Clones;

public class MyStackItem<T>
{
    public MyStackItem(T item)
    {
        Item = item;
    }

    public T Item { get; set; }
    public MyStackItem<T> Next { get; set; }
}
