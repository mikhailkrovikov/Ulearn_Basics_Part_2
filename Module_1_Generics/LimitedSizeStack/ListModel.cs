using System.Collections.Generic;

namespace LimitedSizeStack;
public class ListModel<TItem>
{
    public List<TItem> Items;

    public LimitedSizeStack<ICommand> Stack;

    public int UndoLimit;

    public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
    {
        Stack = new LimitedSizeStack<ICommand>(undoLimit);
        UndoLimit = undoLimit;
    }

    public ListModel(List<TItem> items, int undoLimit)
    {
        Items = items;
        UndoLimit = undoLimit;
    }

    public void AddItem(TItem item)
    {
        AddItemCommand<TItem> command = new AddItemCommand<TItem>(this, item);
        command.Execute();
        Stack.Push(command);
    }

    public void RemoveItem(int index)
    {
        RemoveItemCommand<TItem> command = new RemoveItemCommand<TItem>(this, index);
        command.Execute();
        Stack.Push(command);
    }

    public bool CanUndo()
    {
        return Stack.Count != 0;
    }

    public void Undo()
    {
        if (CanUndo())
        {
            ICommand command = Stack.Pop();
            command.Undo();
        }
    }
}
