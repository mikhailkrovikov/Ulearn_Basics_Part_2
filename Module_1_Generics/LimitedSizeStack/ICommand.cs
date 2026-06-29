namespace LimitedSizeStack;

public interface ICommand
{
    void Execute();
    void Undo();
}
