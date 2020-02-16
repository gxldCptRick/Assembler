using AssemblerLib.Tokenizer.Tokens;
using System;

namespace AssemblerLib.Compiler.CompilationTokens.BoostedTokens
{
    public class RawNumericToken : HexToken
    {
        public RawNumericToken(int rawValue) : base($"0x{Convert.ToString(rawValue, 16)}")
        {
        }
    }
}
