using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{

    [DebuggerDisplay("T: {Content}")]
    public abstract class Term : ICompilationToken
    {
        public abstract string Content { get; }
        public abstract NumericToken Value { get; }

        public abstract IEnumerable<IToken> Assemble();
    }
}
