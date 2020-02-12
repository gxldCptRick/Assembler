using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens
{
    public interface IStatement: ICompilationToken
    {
        IEnumerable<IToken> AssemblyCommand();
    }
}
