using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Grammar_Rules.Tokens
{
    public enum Condition
    {
        AL = 0b1110,
        EQ = 0b0000,
        NE = 0b0001,
        LT = 0b1011,
        LE = 0b1010,
        GT = 0b1100,
        GE = 0b1010
    }
}
