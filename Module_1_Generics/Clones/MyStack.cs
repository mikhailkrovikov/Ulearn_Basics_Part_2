using System;

namespace Clones;

public class MyStack<T>
{
    public MyStack() { }
    public MyStack(MyStack<T> stack)
    {
        Head = stack.Head;
        count = stack.Count;
    }

    public MyStackItem<T> Head;
    private int count;
    public bool IsEmpty { get { return count == 0; } }
    public int Count { get { return count; } }
    public void Push(T item)
    {
        MyStackItem<T> myStackItem = new MyStackItem<T>(item);
        myStackItem.Next = Head;
        Head = myStackItem;
        count++;
    }

    public T Pop()
    {
        if (IsEmpty)
            throw new InvalidOperationException();
        MyStackItem<T> temp = Head;
        Head = Head.Next;
        count--;
        return temp.Item;
    }

    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException();
        return Head.Item;
    }
}
