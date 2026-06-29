using System;
using System.Collections;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
    LinkedList<T> limitedSizeStack = new LinkedList<T>();
    public int UndoLimit { get; set; }
    public LimitedSizeStack(int undoLimit)
    {
        UndoLimit = undoLimit;
    }

    public void Push(T item)
    {
        limitedSizeStack.AddLast(item);
        if (Count > UndoLimit)
            limitedSizeStack.RemoveFirst();
    }

    public T Pop()
    {
        var a = limitedSizeStack.Last.Value;
        limitedSizeStack.RemoveLast();
        return a;
    }

    public int Count => limitedSizeStack.Count;
}