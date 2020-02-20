using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    [DebuggerDisplay("Expression: {Value}")]
    public abstract class Expression : ICompilationToken
    {
        public abstract string Content { get; }

        public abstract NumericToken Value { get; }

        public abstract IEnumerable<IToken> Assemble();

        public override string ToString()
        {
            return Content;
        }
    }
}
