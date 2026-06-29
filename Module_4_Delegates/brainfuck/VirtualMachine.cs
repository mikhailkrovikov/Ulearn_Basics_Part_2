using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }
        public Dictionary<char, Action<IVirtualMachine>> Commands = new();
        public VirtualMachine(string program, int memorySize)
        {
            Memory = new byte[memorySize];
            Instructions = program;
            MemoryPointer = 0;
            InstructionPointer = 0;
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            Commands[symbol] = execute;
        }

        public void Run()
        {
            while (InstructionPointer >= 0 && InstructionPointer < Instructions.Length)
            {
                var currentCommand = Instructions[InstructionPointer];
                if (Commands.ContainsKey(currentCommand))
                    Commands[currentCommand](this);
                InstructionPointer++;
            }
        }
    }
}