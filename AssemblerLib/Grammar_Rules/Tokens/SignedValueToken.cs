using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Grammar_Rules.Tokens
{
    public class SignedValueToken : IToken
    {
        private readonly NumericToken _nestedToken;

        public string Content => $"{Value}";
        public int Value => -_nestedToken;

        public SignedValueToken(NumericToken nestedToken)
        {
            _nestedToken = nestedToken;
        }
        public static implicit operator int(SignedValueToken svt) => svt.Value;

        public override string ToString()
        {
            return Content;
        }
    }
}
