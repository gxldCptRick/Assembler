using AssemblerLib.Grammar_Rules.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.Multiply
{
    public class MultiplicationToken : IOperation
    {
        public string Content => $"MUL {Destination}, {FirstOperand}, {SecondOperand}";

        private RegisterToken Destination { get; }
        private RegisterToken FirstOperand { get; }
        private RegisterToken SecondOperand { get; }
        private Condition ConditionCode { get; }

        public MultiplicationToken(Condition condition, RegisterToken destination, RegisterToken secondOperand, RegisterToken firstOperand)
        {
            Destination = destination;
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            ConditionCode = condition;
        }
        public byte[] Encode()
        {
            var i = 0 | ((int)ConditionCode << 28) | (1 << 20) | (Destination << 16) | (SecondOperand << 8) | (0b1001 << 4) |  ( FirstOperand << 0);
            var bytes = i.ToByteArray();
            bytes.Reverse();
            return bytes;
        }
    }
}
