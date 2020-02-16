using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Compiler.CompilationTokens
{
    public interface IStatement : ICompilationToken
    {
        IEnumerable<IToken> AssemblyCommand();
    }
}
