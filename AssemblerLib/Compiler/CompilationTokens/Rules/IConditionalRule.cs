using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public interface IConditionalRule
    {
        Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken);
    }
}
