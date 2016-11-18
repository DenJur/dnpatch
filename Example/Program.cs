﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnpatch;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Replaces all instructions with your own body
             */
            Patcher p = new Patcher("Test.exe");
            Instruction[] opcodesConsoleWriteLine = {
                Instruction.Create(OpCodes.Ldstr, "Hello Sir"), // String to print
                Instruction.Create(OpCodes.Call, p.BuildMemberRef("System", "Console", "WriteLine")), // Console.WriteLine call
                Instruction.Create(OpCodes.Ret) // Alaway return smth
            };
            Target target = new Target()
            {
                Namespace = "Test",
                Class = "Program",
                Method = "Print",
                Instructions = opcodesConsoleWriteLine
            };
            p.Patch(target);
            p.Save("Test1.exe");


            /*
             * Replaces the instructions at the given index
             */
            p = new Patcher("Test.exe");
            Instruction[] opCodesManipulateOffset = {
                Instruction.Create(OpCodes.Ldstr, "Place easter egg here 1"),
                Instruction.Create(OpCodes.Ldstr, "Place easter egg here 2")
            };
            int[] indexes = {
                4,
                8
            };
            target = new Target()
            {
                Namespace = "Test",
                Class = "Program",
                Method = "PrintAlot",
                Instructions = opCodesManipulateOffset,
                Indexes = indexes
            };
            p.Patch(target);
            p.Save("Test2.exe");


            /*
             * Replaces the instructions at the given index in a nested class
             */
            p = new Patcher("Test.exe");
            Instruction opCodeManipulateOffsetNestedClass = Instruction.Create(OpCodes.Ldstr, "FooBarCode");
            int index = 0;
            string nestedClass = "Bar";
            target = new Target()
            {
                Namespace = "Test",
                Class = "Foo",
                NestedClass = nestedClass,
                Method = "NestedPrint",
                Instruction = opCodeManipulateOffsetNestedClass,
                Index = index
            };
            p.Patch(target);
            p.Save("Test3.exe");


            /*
             * Replaces the instructions at the given index in a big nested class
             */
            p = new Patcher("Test.exe");
            Instruction opCodeManipulateOffsetNestedClasses = Instruction.Create(OpCodes.Ldstr, "Eat fruits");
            index = 0;
            string[] nestedClasses = {
                "Am",
                "A",
                "Burger"
            };
            target = new Target()
            {
                Namespace = "Test",
                Class = "I",
                NestedClasses = nestedClasses,
                Method = "Eat",
                Instruction = opCodeManipulateOffsetNestedClasses,
                Index = index
            };
            p.Patch(target);
            p.Save(true);
        }
    }
}