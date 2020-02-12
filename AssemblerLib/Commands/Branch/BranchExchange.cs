using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.Branch
{
    public class BranchExchange: IOperation
    {
        private Condition _condition;
        private RegisterToken _register;
        public BranchExchange(Condition con, RegisterToken register)
        {
            _condition = con;
            _register = register;
        }
        public string Content => $"BX{(_condition == Condition.AL ? "" : (object)_condition)} {_register}";

        public byte[] Encode()
        {
            var i = 0b0000_0001_0010_1111_1111_1111_0001_0000 | ((int)_condition << 28) | _register;
            var bytes = i.ToByteArray();
            bytes.Reverse();
            return bytes;
        }
    }
}
