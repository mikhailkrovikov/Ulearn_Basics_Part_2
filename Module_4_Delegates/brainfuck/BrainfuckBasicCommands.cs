using System;
using System.Runtime.CompilerServices;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            vm.RegisterCommand('.', b =>
            {
                write((char)b.Memory[b.MemoryPointer]);
            });

            vm.RegisterCommand('+', b =>
            {
                if (b.Memory[b.MemoryPointer] == 255)
                    b.Memory[b.MemoryPointer] = 0;
                else
                    b.Memory[b.MemoryPointer]++;
            });

            vm.RegisterCommand('-', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.Memory[b.MemoryPointer] = 255;
                else b.Memory[b.MemoryPointer]--;
            });

            vm.RegisterCommand(',', b =>
            {
                b.Memory[b.MemoryPointer] = (byte)read();
            });

            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer == b.Memory.Length - 1)
                    b.MemoryPointer = 0;
                else b.MemoryPointer++;
            });

            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer == 0)
                    b.MemoryPointer = b.Memory.Length - 1;
                else b.MemoryPointer--;
            });
            foreach (var c in validChars)
            {
                vm.RegisterCommand(c, b =>
                {
                    b.Memory[b.MemoryPointer] = (byte)c;
                });
            }
        }
    }
}