using AssemblerLib.Grammar_Rules.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.PushPop
{
    public class PopToken : IOperation
    {
        public string Content => $"POP{(_condition == Condition.AL? "":(object)_condition)} {_source}";

        private Condition _condition;
        private RegisterToken _source;

        public PopToken(Condition cond, RegisterToken sauce)
        {
            _condition = cond;
            _source = sauce;
        }
        public byte[] Encode()
        {
            var i = 0b0000_0100_1001_1101_0000_0000_0000_0100 | ((int)_condition << 28) | (_source << 12);
            var bytes = i.ToByteArray();
            bytes.Reverse();
            return bytes;
        }
    }
}
