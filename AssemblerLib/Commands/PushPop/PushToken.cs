using AssemblerLib.Grammar_Rules.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Commands.PushPop
{
    public class PushToken : IOperation
    {
        public string Content => $"PUSH{(_condition == Condition.AL ? "" : (object)_condition)} {_source}";
        private Condition _condition;
        private RegisterToken _source;

        public PushToken(Condition con, RegisterToken sauce)
        {
            _condition = con;
            _source = sauce;
        }

        public byte[] Encode()
        {
            var i = 0b0000_0101_0010_1101_0000_0000_0000_0100 | ((int)_condition << 28) | (_source << 12);
            var bytes = i.ToByteArray();
            bytes.Reverse();
            return bytes;
        }

        public override bool Equals(object obj)
        {
            return obj is PushToken token &&
                   _condition == token._condition &&
                   EqualityComparer<RegisterToken>.Default.Equals(_source, token._source);
        }

        public override int GetHashCode()
        {
            var hashCode = -1889235114;
            hashCode = hashCode * -1521134295 + _condition.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<RegisterToken>.Default.GetHashCode(_source);
            return hashCode;
        }
    }
}
