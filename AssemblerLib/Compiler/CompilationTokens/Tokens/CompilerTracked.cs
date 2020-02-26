using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public abstract class CompilerTracked : ICompilationToken
    {
        public abstract string Content { get; }
        public abstract IEnumerable<IToken> ResolveValue();
        public abstract IEnumerable<IToken> WriteOut();
    }
}
