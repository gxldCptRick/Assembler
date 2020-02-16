using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public abstract class Term : ICompilationToken
    {
        public abstract string Content { get; }
        public abstract NumericToken Value { get; }
    }
}
