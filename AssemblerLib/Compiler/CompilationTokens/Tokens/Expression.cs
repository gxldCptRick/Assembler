using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
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
