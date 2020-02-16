using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public interface IConditionalRule
    {
        Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken);
    }
}
