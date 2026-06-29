namespace LimitedSizeStack;

public class RemoveItemCommand<TItem> : ICommand
{
    private ListModel<TItem> _Items;
    private int _Index;
    TItem item;
    public RemoveItemCommand(ListModel<TItem> items, int index)
    {
        _Index = index;
        _Items = items;
    }

    public void Execute()
    {
        item = _Items.Items[_Index];
        _Items.Items.RemoveAt(_Index);
    }

    public void Undo()
    {
        _Items.Items.Insert(_Index, item);
    }
}