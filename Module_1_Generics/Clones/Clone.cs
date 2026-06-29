namespace Clones;

public partial class CloneVersionSystem
{
    public class Clone
    {
        private MyStack<string> _CancelledProgram = new MyStack<string>();
        private MyStack<string> _LearnedProgram = new MyStack<string>();
        public Clone(MyStack<string> learned, MyStack<string> cancelled)
        {
            _LearnedProgram = new MyStack<string>(learned);
            _CancelledProgram = new MyStack<string>(cancelled);
        }

        public Clone() { }
        public void LearnCommand(string command)
        {
            _LearnedProgram.Push(command);
        }

        public void RollbackCommand()
        {
            string lastCommand = _LearnedProgram.Pop();
            _CancelledProgram.Push(lastCommand);
        }

        public void RelearnCommand()
        {
            if (_CancelledProgram.Count > 0)
                _LearnedProgram.Push(_CancelledProgram.Pop());
        }

        public Clone CloneCommand()
        {
            return new Clone(_LearnedProgram, _CancelledProgram);
        }

        public string CheckCommand()
        {
            if (_LearnedProgram.Count > 0)
                return _LearnedProgram.Peek();
            else return "basic";
        }
    }
}