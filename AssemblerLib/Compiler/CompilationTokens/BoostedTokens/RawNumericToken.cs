using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.BoostedTokens
{
    public class RawNumericToken : HexToken
    {
        public RawNumericToken(int rawValue) : base($"0x{Convert.ToString(rawValue, 16)}")
        {
        }
    }
}
