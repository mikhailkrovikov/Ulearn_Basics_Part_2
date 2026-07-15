using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var bracketPairs = new Dictionary<int, int>();
			var openBrackets = new Stack<int>();

			for (var i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[')
					openBrackets.Push(i);
				else if (vm.Instructions[i] == ']')
				{
					var open = openBrackets.Pop();
					bracketPairs[open] = i;
					bracketPairs[i] = open;
				}
			}

			vm.RegisterCommand('[', b =>
			{
				if (b.Memory[b.MemoryPointer] == 0)
					b.InstructionPointer = bracketPairs[b.InstructionPointer];
			});

			vm.RegisterCommand(']', b =>
			{
				if (b.Memory[b.MemoryPointer] != 0)
					b.InstructionPointer = bracketPairs[b.InstructionPointer];
			});
		}
	}
}
