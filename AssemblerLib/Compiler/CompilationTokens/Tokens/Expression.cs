using AssemblerLib.Tokenizer.Tokens;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    [DebuggerDisplay("Exprssion: {Value}")]
    public abstract class Expression : ICompilationToken
    {
        public abstract string Content { get; }

        public abstract NumericToken Value { get; }

        public override string ToString()
        {
            return Content;
        }
    }
}
