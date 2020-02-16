using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Commands.MoveTopBottom
{
    public class MoveTopToken : IOperation
    {
        public string Content => $"MOVT{(_condition == Condition.AL ? "" : (object)_condition)} {_destination}, {_value}";
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
            var i = 0b0000_0011_0100_0000_0000_0000_0000_0000;
            i |= ((int)_condition) << 28;
            var shortenedValue = (short)_value;
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

        public override bool Equals(object obj)
        {
            return obj is MoveTopToken token &&
                   _condition == token._condition &&
                   EqualityComparer<RegisterToken>.Default.Equals(_destination, token._destination) &&
                   EqualityComparer<NumericToken>.Default.Equals(_value, token._value);
        }

        public override int GetHashCode()
        {
            var hashCode = -9044964;
            hashCode = hashCode * -1521134295 + _condition.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<RegisterToken>.Default.GetHashCode(_destination);
            hashCode = hashCode * -1521134295 + EqualityComparer<NumericToken>.Default.GetHashCode(_value);
            return hashCode;
        }
    }
}
