using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.MoveTopBottom
{
    public class MoveTopToken : IOperation
    {
        public string Content => $"MOVT{(_condition == Condition.AL ? (object)"" : (object)_condition)} {_destination}, {_value}";
        public Condition _condition;
        public RegisterToken _destination;
        public NumericToken _value;

        public MoveTopToken(Condition condition, RegisterToken destination, NumericToken value)
        {
            _condition = condition;
            _destination = destination;
            _value = value;
        }

        public byte[] Encode()
        {
            int i = 0b0000_0011_0100_0000_0000_0000_0000_0000;
            i |= ((int)_condition) << 28;
            short shortenedValue = (short)_value;
            i |= ((shortenedValue >> 12) & 0xF) << 16;
            i |= _destination << 12;
            i |= (shortenedValue & 0b0000_1111_1111_1111) << 0;
            var bytes = i.ToByteArray();
            bytes.Reverse();
            return bytes;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
