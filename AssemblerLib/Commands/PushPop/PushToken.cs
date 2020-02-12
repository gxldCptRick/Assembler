using AssemblerLib.Grammar_Rules.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.PushPop
{
    public class PushToken : IOperation
    {
        public string Content => $"PUSH{(_condition == Condition.AL? "":(object)_condition)} {_source}";
        private Condition _condition;
        private RegisterToken _source;

        public PushToken(Condition con, RegisterToken sauce)
        {
            _condition = con;
            _source = sauce;
        }

        public byte[] Encode()
        {
            var i = 0b0000_0101_0010_1101_0000_0000_0000_0100 | ((int) _condition<< 28) | (_source << 12);
            var bytes = i.ToByteArray();
            bytes.Reverse();
            return bytes;
        }
    }
}
