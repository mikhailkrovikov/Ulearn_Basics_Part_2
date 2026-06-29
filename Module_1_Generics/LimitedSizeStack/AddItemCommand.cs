namespace LimitedSizeStack;

public class AddItemCommand<TItem> : ICommand
{
    private ListModel<TItem> _Items;
    private TItem _Item;
    public AddItemCommand(ListModel<TItem> items, TItem item)
    {
        _Items = items;
        _Item = item;
    }

    public void Execute()
    {
        _Items.Items.Add(_Item);
    }

    public void Undo()
    {
        _Items.Items.Remove(_Item);
    }
}
