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
        LS = 0b1001,
        HI = 0b1000,
        VC = 0b0111,
        VS = 0b0110,
        LE = 0b1101,
        GT = 0b1100,
        GE = 0b1010
    }
}
